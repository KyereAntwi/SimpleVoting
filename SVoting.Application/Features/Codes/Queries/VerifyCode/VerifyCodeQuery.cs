using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Application.Exceptions;
using SVoting.Domain.Entities;

namespace SVoting.Application.Features.Codes.Queries.VerifyCode
{
    public record VerifyCodeQuery(string code) : IRequest;

    public class VerifyCodeQueryHandler : IRequestHandler<VerifyCodeQuery>
    {
        private readonly ICodesRepository _codesRepository;

        public VerifyCodeQueryHandler(ICodesRepository codesRepository) 
        { 
            _codesRepository = codesRepository;
        }

        public async Task<Unit> Handle(VerifyCodeQuery request, CancellationToken cancellationToken)
        {
            var code = await _codesRepository.GetByIdentifier(request.code);

            if (code == null) 
            {
                throw new NotFoundException("Code does not exist", nameof(Code));
            }

            return Unit.Value;
        }
    }
}
