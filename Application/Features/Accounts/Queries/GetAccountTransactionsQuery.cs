using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;
using System.Transactions;

namespace Application.Features.Accounts.Queries;

public class GetAccountTransactionsQuery : IRequest<ResponseWrapper<List<TransactionResponse>>>
{
    public int AccountId { get; set; }
}


public class GetAccountTransactionsQueryhandler : IRequestHandler<GetAccountTransactionsQuery, ResponseWrapper<List<TransactionResponse>>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAccountTransactionsQueryhandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseWrapper<List<TransactionResponse>>> Handle(GetAccountTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactionsInDb =  _unitOfWork.ReadRepositoryFor<Domain.Transaction>()
            .Entities
            .Where(transaction => transaction.AccountId == request.AccountId)
            .ToList();

        if(transactionsInDb.Count > 0)
        {
            return new ResponseWrapper<List<TransactionResponse>>().Success(data: transactionsInDb.Adapt<List<TransactionResponse>>());

        }
        return new ResponseWrapper<List<TransactionResponse>>().Failed(message: "No Transactions found For This Account.");
    }
}
