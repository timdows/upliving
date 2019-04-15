using MediatR;

namespace Upliving.UseCases.Hour1400
{
	public class Hour1400UploadRequest: IRequest<Hour1400UploadResponse>
	{
		public byte[] Bytes { get; set; }
		public string FileName { get; set; }
		public string Secret { get; set; }
	}
}
