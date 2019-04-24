using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Upliving.Models;

namespace Upliving.UseCases.Hour1400
{
	public class GetThumbnailsHandler : IRequestHandler<GetThumbnailsRequest, GetThumbnailsResponse>
	{
		private readonly AppSettings _appSettings;

		public GetThumbnailsHandler(IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
		}

		public Task<GetThumbnailsResponse> Handle(GetThumbnailsRequest request, CancellationToken cancellationToken)
		{
			var validator = new GetThumbnailsRequestValidator();
			var result = validator.Validate(request);

			if (!result.IsValid)
			{
				throw new Exception(result.ToString());
			}

			var images = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), _appSettings.Hour1400Path))
				.Select(item => Hour1400File.CreateImageFileDetails(item))
				.Where(item => item.FileName.EndsWith("_thumb.jpg"))
				.OrderByDescending(item => item.DateTaken)
				.ToList();

			return Task.FromResult(new GetThumbnailsResponse
			{
				Hour1400Files = images
			});
		}
	}
}
