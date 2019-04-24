using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace Upliving.Helpers
{
	public static class ImageHelpers
	{

		public static void CreateThumbnail(string intputFileName)
		{
			var outputFilename = $"{Path.GetFileNameWithoutExtension(intputFileName)}_thumb.jpg";
			if (File.Exists(outputFilename))
			{
				return;
			}

			using (var image = Image.Load(intputFileName))
			{
				var height = 200;
				decimal widthRatio = (decimal)image.Height / (decimal)height;
				int width = (int)Math.Round(image.Width / widthRatio, 0);

				image.Mutate(x => x.Resize(width, height));

				
				image.Save($"{Path.GetDirectoryName(intputFileName)}/{outputFilename}");
			}
		}
	}
}
