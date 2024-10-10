using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.AccountHolders.Queries;

public class GetAccountHoldersQuery : IRequest<ResponseWrapper<List<AccountHolderResponses>>>{}

public class GetAccountHoldersQueryHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<GetAccountHoldersQuery, ResponseWrapper<List<AccountHolderResponses>>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;

    public async Task<ResponseWrapper<List<AccountHolderResponses>>> Handle(GetAccountHoldersQuery request, CancellationToken cancellationToken)
    {
        var accountHoldersInDb = await _unitOfWork.ReadRepositoryFor<AccountHolder>()
            .GetAllAsync();

        return new ResponseWrapper<List<AccountHolderResponses>>()
            .Success(accountHoldersInDb.Adapt<List<AccountHolderResponses>>());
    }


}
 