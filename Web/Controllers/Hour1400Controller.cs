using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Upliving.Helpers;
using Upliving.Models;
using Upliving.UseCases.Hour1400;

namespace Upliving.Controllers
{
	[Route("api/[controller]/[action]")]
	public class Hour1400Controller : Controller
	{
		private readonly IMediator _mediator;
		private readonly AppSettings _appSettings;

		public Hour1400Controller(IMediator mediator, IOptions<AppSettings> appSettings)
		{
			_mediator = mediator;
			_appSettings = appSettings.Value;
		}

		[HttpPost]
		[ProducesResponseType(typeof(Hour1400UploadResponse), 200)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> Hour1400Upload([FromBody] Hour1400UploadRequest request)
		{
			var result = await _mediator.Send(request);
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(typeof(GetThumbnailsResponse), 200)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetThumbnails([FromBody] GetThumbnailsRequest request)
		{
			var result = await _mediator.Send(request);
			return Ok(result);
		}

		[HttpGet]
		public IActionResult GetImage(string fileName)
		{
			var fullPath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), _appSettings.Hour1400Path), fileName);

			if (System.IO.File.Exists(fullPath))
			{
				return new PhysicalFileResult(fullPath, "image/jpg");
			}

			return NotFound();
		}

		[HttpGet]
		public IActionResult CreateThumbnails()
		{
			var images = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), _appSettings.Hour1400Path))
				.Where(item => !item.Contains("thumb") && item.EndsWith(".jpg"))
				.OrderBy(item => item)
				.ToList();

			foreach (var image in images)
			{
				ImageHelpers.CreateThumbnail(image);
			}

			return Ok();
		}
	}
}
