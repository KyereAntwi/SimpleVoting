using AutoMapper;
using SVoting.Application.Features.Categories.Commands.CreateACategory;
using SVoting.Application.Features.PollingSpaces.Commands.CreateAPollingSpace;
using SVoting.Application.Features.Polls.Commands.CreateAPoll;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<PollingSpace, PollingSpaceDto>().ReverseMap();
		CreateMap<Poll, PollDto>().ReverseMap();
		CreateMap<PollingCategory, CategoryDto>().ReverseMap();
		CreateMap<Nominee, NomineeDto>().ReverseMap();

		CreateMap<CreateAPollingSpaceCommand, PollingSpace>();
		CreateMap<CreateAPollCommand, Poll>();
	}
}

