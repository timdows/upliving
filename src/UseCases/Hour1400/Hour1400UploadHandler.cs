using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Upliving.Models;

namespace Upliving.UseCases.Hour1400
{
	public class Hour1400UploadHandler : IRequestHandler<Hour1400UploadRequest, Hour1400UploadResponse>
	{
		private readonly AppSettings _appSettings;

		public Hour1400UploadHandler(IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
		}

		public async Task<Hour1400UploadResponse> Handle(Hour1400UploadRequest request, CancellationToken cancellationToken)
		{
			var validator = new Hour1400UploadRequestValidator();
			var result = validator.Validate(request);

			if (!result.IsValid)
			{
				throw new Exception(result.ToString());
			}

			if (!string.Equals(request.Secret, _appSettings.Hour1400UploadSecret))
			{
				throw new Exception("Incorrect upload secret");
			}

			var saveDir = Path.Combine(Directory.GetCurrentDirectory(), _appSettings.Hour1400Path);
			if (!Directory.Exists(saveDir))
			{
				Directory.CreateDirectory(saveDir);
			}

			// full path to file in temp location
			var filePath = Path.Combine(saveDir, request.FileName);

			if (request.Bytes.Length > 0)
			{
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await stream.WriteAsync(request.Bytes);
				}
			}

			return new Hour1400UploadResponse();
		}
	}
}
