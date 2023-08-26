using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.PollingSpaces.Commands.DisableSpace;

public class DisableSpaceCommand : IRequest<DisableSpacResponse>
{
	public Guid PollingSpaceId { get; set; }
}

public class DisableSpaceCommandHandler : IRequestHandler<DisableSpaceCommand, DisableSpacResponse>
{
    private readonly IPollingSpaceRepository _pollingSpaceRepository;

    public DisableSpaceCommandHandler(IPollingSpaceRepository pollingSpaceRepository)
    {
        _pollingSpaceRepository = pollingSpaceRepository;
    }

    public async Task<DisableSpacResponse> Handle(DisableSpaceCommand request, CancellationToken cancellationToken)
    {
        var response = new DisableSpacResponse();

        var validator = new DisableSpaceCommandValidator(_pollingSpaceRepository);
        var validationErrors = await validator.ValidateAsync(request);

        if (validationErrors.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();
            foreach (var error in validationErrors.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        if (response.Success)
        {
            await _pollingSpaceRepository.TogglePollingSpaceDisability(request.PollingSpaceId);
            response.PollingSpaceId = request.PollingSpaceId;
        }

        return response;
    }
}

