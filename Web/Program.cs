using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace Upliving
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseKestrel(options =>
				{
					options.Listen(IPAddress.Loopback, 5005);
				})
				.UseStartup<Startup>();
	}
}
