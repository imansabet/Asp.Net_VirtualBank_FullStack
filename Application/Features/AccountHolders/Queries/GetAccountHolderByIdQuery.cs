using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.AccountHolders.Queries;

public class GetAccountHolderByIdQuery : IRequest<ResponseWrapper<AccountHolderResponses>>
{
    public int Id { get; set; }
}
public class GetAccountHolderByIdQueryHandler : IRequestHandler<GetAccountHolderByIdQuery, ResponseWrapper<AccountHolderResponses>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAccountHolderByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseWrapper<AccountHolderResponses>> Handle(GetAccountHolderByIdQuery request, CancellationToken cancellationToken)
    {
        var accountHolderInDb = await _unitOfWork.ReadRepositoryFor<AccountHolder>()
            .GetByIdAsync(request.Id);

        if(accountHolderInDb is not null)
        {
            return new ResponseWrapper<AccountHolderResponses>().Success(accountHolderInDb.Adapt<AccountHolderResponses>());
        }
        return new ResponseWrapper<AccountHolderResponses>().Failed("AccountHolder Does Not Exists.");
    }
}
