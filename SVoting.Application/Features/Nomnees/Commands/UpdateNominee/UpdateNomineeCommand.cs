using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Nomnees.Commands.UpdateNominee
{
    public class UpdateNomineeCommand : IRequest<DeleteNomineeResponse>
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public byte[]? Photograph { get; set; }
        public string? PhotoMime { get; set; }
    }

    public class UpdateNomineeCommandHandler : IRequestHandler<UpdateNomineeCommand, DeleteNomineeResponse>
    {
        private readonly IAsyncRepository<Nominee> _asyncRepository;

        public UpdateNomineeCommandHandler(IAsyncRepository<Nominee> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }

        public async Task<DeleteNomineeResponse> Handle(UpdateNomineeCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteNomineeResponse();

            var validator = new UpdateNomineeCommandValidator(_asyncRepository);
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
                var nominee = await _asyncRepository.GetByIdAsync(response.NomineeId);

                nominee!.Fullname = request.Fullname;

                if(request.Photograph is not null)
                {
                    nominee.Photograph = request.Photograph;
                    nominee.Photomime = request.PhotoMime;
                }

                await _asyncRepository.UpdateAsync(nominee);

                response.NomineeId = nominee.Id;
            }

            return response;
        }
    }
}
