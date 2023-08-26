using System;
namespace SVoting.Shared.Models;

public class CreateACategoryResponse : BaseResponse
{
	public CreateACategoryResponse() : base()
	{
	}

	public CategoryDto? CategoryDto { get; set; }
}

