using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Creator
{
	class Program
	{
		public static async Task Main(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			var appSettings = config.GetSection("AppSettings").Get<AppSettings>();
		}
	}
}
