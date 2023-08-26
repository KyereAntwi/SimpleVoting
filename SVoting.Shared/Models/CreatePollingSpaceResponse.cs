using System;
namespace SVoting.Shared.Models;

public class CreatePollingSpaceResponse : BaseResponse
{
	public CreatePollingSpaceResponse() : base()
	{
	}

	public PollingSpaceDto PollingSpace { get; set; } = default!;
}

