using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountByAccountNumberQuery : IRequest<ResponseWrapper<AccountResponse>>
{
    public string AccountNumber { get; set; }
}
public class GetAccountByAccountNumberQueryhandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<GetAccountByAccountNumberQuery, ResponseWrapper<AccountResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;

    public async Task<ResponseWrapper<AccountResponse>> Handle(GetAccountByAccountNumberQuery request, CancellationToken cancellationToken)
    {
        var accountInDb = _unitOfWork.ReadRepositoryFor<Account>()
            .Entities
            .Where(account => account.AccountNumber == request.AccountNumber)
            .FirstOrDefault();
        if (accountInDb is not null)
        {
            return await Task.FromResult(new ResponseWrapper<AccountResponse>().Success(data: accountInDb.Adapt<AccountResponse>())); 
        }
        return await Task.FromResult(new ResponseWrapper<AccountResponse>().Failed("Account Does Not Exists."));
    }
}