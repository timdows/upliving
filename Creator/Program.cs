using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Creator.Commands;
using Microsoft.Extensions.Configuration;

namespace Creator
{
	public class Program
	{
		const string FinishedPathsLogFile = "finishedPaths.log";

		public static async Task Main(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			var appSettings = config.GetSection("AppSettings").Get<AppSettings>();

			while (true)
			{
				try
				{
					Console.WriteLine($"Running commands, start at {DateTime.Now.ToShortTimeString()}");
					await Task.WhenAll(
						Run(appSettings),
						Task.Delay(60 * 60 * 1000));
					Console.WriteLine($"Finished run at {DateTime.Now.ToShortTimeString()}");
				}
				catch (Exception excep)
				{
					Console.Error.WriteLine(excep.Message);
					await Task.Delay(60 * 60 * 1000);
				}
			}
		}

		public static async Task Run(AppSettings appSettings)
		{
			//var get1400HourFileCommand = new Get1400HourFileCommand(appSettings);
			//var everyHourFileCommand = new GetEveryHourFileCommand(appSettings);

			var directories = Directory.EnumerateDirectories(appSettings.SourceImageLocation).OrderBy(item => item);
			foreach (var directory in directories)
			{
				var date = Path.GetFileName(directory);
				if (date.Equals(DateTime.Today.ToString("yyyy-MM-dd"), StringComparison.CurrentCultureIgnoreCase))
				{
					continue;
				}

				var sourceDirectory = Path.Combine(appSettings.SourceImageLocation, date);
				//var destinationDirectory = Path.Combine(appSettings.LocalImageLocation, date);

				if (await IsPathInFinishedFile(sourceDirectory))
				{
					Console.WriteLine($"Skipping copy files and resize for directory {sourceDirectory}");
					continue;
				}

				Console.WriteLine($"Working with directory {sourceDirectory}");

				//GetFilesAndSaveResizedCommand.GetFilesAndSaveResized(sourceDirectory, destinationDirectory);
				await Get1400HourFileCommand.Execute(appSettings, directory);
				//await everyHourFileCommand.GetEveryHourFile(sourceDirectory);
				//await CreateTimelapseMP4Command.CreateTimelapseMP4(appSettings, destinationDirectory, date);

				await AddPathToFinishedFile(sourceDirectory);
			}
		}

		private static void SortImagePerMinute(AppSettings appSettings)
		{
			var allImageFileDetails = new List<ImageFileDetails>();

			var directories = Directory.GetDirectories(appSettings.SourceImageLocation);
			foreach (var directory in directories)
			{
				Console.Write($"Working dir {directory}");
				var allFiles = Directory.GetFiles(directory, "*.jpg", SearchOption.TopDirectoryOnly);
				Console.WriteLine($" {allFiles.Length} files");

				var i = 0;
				foreach (var file in allFiles)
				{
					var imageFileDetails = ImageFileDetails.CreateImageFileDetails(file);
					allImageFileDetails.Add(imageFileDetails);
					Console.Write($"\r{i++} of {allFiles.Length}");
				}

				Console.WriteLine();
			}

			allImageFileDetails = allImageFileDetails.Where(item => item != null).ToList();

			var groupImageFileDetails = allImageFileDetails.GroupBy(item => item.DateTimeTaken.Date);

			foreach (var groupImageFileDetail in groupImageFileDetails)
			{
				var destinationDirectory = Path.Combine(appSettings.MonthImageLocation, groupImageFileDetail.Key.ToString("yyyy-MM-dd"));
				Directory.CreateDirectory(destinationDirectory);

				//var perMinute = groupImageFileDetail.GroupBy(item => item.DateTimeTaken.)
				var perMinuteGroups = groupImageFileDetail.Select(x => new
				{
					Element = x,
					Day = x.DateTimeTaken.Date,
					Hour = x.DateTimeTaken.AddMilliseconds(-1).Hour,
					Minute = x.DateTimeTaken.AddMilliseconds(-1).Minute,
					Second = x.DateTimeTaken.AddMilliseconds(-1).Second
				})
				 .GroupBy(x => new { x.Day, x.Hour, x.Minute });
				foreach (var perMinuteGroup in perMinuteGroups)
				{
					var imageFileDetail = perMinuteGroup
						.OrderByDescending(item => item.Second)
						.First()
						.Element;
					try
					{
						File.Copy(imageFileDetail.Path, Path.Combine(destinationDirectory, imageFileDetail.SaveDateTimeFileName));
					}
					catch (Exception excep)
					{
						var a = excep.Message;
					}
				}

				// Remove the source stuff
				var allFiles = groupImageFileDetail.ToList();
				foreach (var file in allFiles)
				{
					File.Delete(file.Path);
				}
			}
		}

		private static async Task<bool> IsPathInFinishedFile(string path)
		{
			if (!File.Exists(FinishedPathsLogFile))
			{
				return false;
			}

			var lines = await File.ReadAllLinesAsync(FinishedPathsLogFile);
			return lines.Contains(path);
		}

		private static async Task AddPathToFinishedFile(string path)
		{
			await File.AppendAllTextAsync(FinishedPathsLogFile, $"{path}\r\n");
		}
	}
}
