using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountsQuery : IRequest<ResponseWrapper<List<AccountResponse>>>
{}
public class GetAccountsQueryHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<GetAccountsQuery, ResponseWrapper<List<AccountResponse>>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;

    public async Task<ResponseWrapper<List<AccountResponse>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accountsInDb = await _unitOfWork.ReadRepositoryFor<Account>().GetAllAsync();
        if(accountsInDb.Count > 0)
        {
            return new ResponseWrapper<List<AccountResponse>>().Success(data: accountsInDb.Adapt<List<AccountResponse>>());
        }
        return new ResponseWrapper<List<AccountResponse>>().Failed(message:"No Accounts Were Found.");

    }
}
