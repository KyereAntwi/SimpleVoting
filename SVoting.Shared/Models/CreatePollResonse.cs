using System;
namespace SVoting.Shared.Models;

public class CreatePollResonse : BaseResponse
{
	public CreatePollResonse() : base()
	{
	}

	public PollDto? PollDto { get; set; }
}

