using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Nomnees.Commands.CreateANominee;

public class CreateANomineeCommand : IRequest<CreateANomineeResponse>
{
    public string Fullname { get; set; } = string.Empty;
    public byte[]? Photograph { get; set; }
    public string? PhotoMime { get; set; }

    public string UserName { get; set; } = string.Empty;
}

public class CreateANomineeCommandHandler : IRequestHandler<CreateANomineeCommand, CreateANomineeResponse>
{
    private readonly IAsyncRepository<Nominee> _asyncRepository;
    private readonly IMapper _mapper;

    public CreateANomineeCommandHandler(IAsyncRepository<Nominee> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<CreateANomineeResponse> Handle(CreateANomineeCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateANomineeResponse();

        var validator = new CreateANomineeCommandValidator();
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
            var newNominee = new Nominee() { Fullname = request.Fullname };

            if (request.Photograph != null)
            {
                newNominee.Photograph = request.Photograph;
                newNominee.Photomime = request.PhotoMime;
                newNominee.UserName = request.UserName;
            }

            var nominee = await _asyncRepository.AddAsync(newNominee);

            response.NomineeDto = _mapper.Map<NomineeDto>(nominee);
        }

        return response;
    }
}
