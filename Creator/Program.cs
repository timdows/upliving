﻿using System;
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
