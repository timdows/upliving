using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Creator.Commands
{
	public static class Get1400HourFileCommand
	{
		public static async Task Execute(AppSettings appSettings, string workingDirectory)
		{
			var files = Directory.GetFiles(workingDirectory, "*.jpg", SearchOption.AllDirectories)
				.Select(item => ImageFileDetails.CreateImageFileDetails(item))
				.Where(item => item != null)
				.ToList();

			var fileAfter = files
				.OrderBy(item => item.DateTimeTaken)
				.FirstOrDefault(item => item.DateTimeTaken.Hour >= 14);

			var fileBefore = files
				.OrderByDescending(item => item.DateTimeTaken)
				.FirstOrDefault(item => item.DateTimeTaken.Hour < 14);

			ImageFileDetails saveFile = null;
			if ((fileAfter?.DateTimeTaken.Hour ?? 23) - 14 <= 14 - (fileBefore?.DateTimeTaken.Hour ?? 0))
			{
				saveFile = fileAfter;
			}

			if ((fileAfter?.DateTimeTaken.Hour ?? 23) - 14 >= 14 - (fileBefore?.DateTimeTaken.Hour ?? 0))
			{
				saveFile = fileBefore;
			}

			if (saveFile == null && fileBefore != null)
			{
				saveFile = fileBefore;
			}

			if (saveFile == null)
			{
				return;
			}

			Console.WriteLine($"Hour1400 file: {saveFile.Path}");

			try
			{
				using (var client = new Services.UplivingAPI { BaseUri = new Uri(appSettings.ServiceApiLocation) })
				{
					var fileName = $"{saveFile.DateTimeTaken.ToString("yyyy-MM-ddTHHmmss")}.jpg";

					var uploadResponse = await client.Hour1400UploadWithHttpMessagesAsync(new Services.Models.Hour1400UploadRequest
					{
						Secret = appSettings.Hour1400UploadSecret,
						Bytes = await File.ReadAllBytesAsync(saveFile.Path),
						FileName = fileName
					});
				}
			}
			catch (Exception excep)
			{
				Console.Error.WriteLine(excep.Message);
			}
		}
	}
}
