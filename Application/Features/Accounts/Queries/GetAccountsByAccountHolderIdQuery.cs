using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountsByAccountHolderIdQuery : IRequest<ResponseWrapper<List<AccountResponse>>>
{
    public int AccountHolderId { get; set; }
}

public class GetAccountsByAccountHolderIdHandler : IRequestHandler<GetAccountsByAccountHolderIdQuery, ResponseWrapper<List<AccountResponse>>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAccountsByAccountHolderIdHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseWrapper<List<AccountResponse>>> Handle(GetAccountsByAccountHolderIdQuery request, CancellationToken cancellationToken)
    {
        var accounts =  _unitOfWork.ReadRepositoryFor<Account>()
            .Entities
            .Where(account => account.AccountHolderId == request.AccountHolderId)
            .ToList();
        
        if (accounts.Count> 0)
        {
            return await Task.FromResult(new ResponseWrapper<List<AccountResponse>>().Success(data: accounts.Adapt<List<AccountResponse>>()));
        }
        return await Task.FromResult(new ResponseWrapper<List<AccountResponse>>().Failed(message: "No Accounts Were Found."));

    }
}
