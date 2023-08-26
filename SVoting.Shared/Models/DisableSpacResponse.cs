using System;
namespace SVoting.Shared.Models
{
	public class DisableSpacResponse : BaseResponse
	{
		public DisableSpacResponse() : base()
		{
		}

		public Guid PollingSpaceId { get; set; }
	}
}

