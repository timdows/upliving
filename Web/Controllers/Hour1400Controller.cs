using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Upliving.UseCases.Hour1400;

namespace Upliving.Controllers
{
	[Route("api/[controller]/[action]")]
	public class Hour1400Controller : Controller
	{
		private readonly IMediator _mediator;

		public Hour1400Controller(IMediator mediator)
		{
			_mediator = mediator;
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
	}
}
