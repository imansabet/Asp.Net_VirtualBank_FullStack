using Application.Repositories;
using Common.Requests;
using Common.Wrapper;
using Domain;
using MediatR;

namespace Application.Features.Accounts.Commands;

public class CreateTransactionCommand : IRequest<ResponseWrapper<int>>
{
    public TransactionRequest Transaction { get; set; }
}
public class CreateTransactionCommandHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<CreateTransactionCommand, ResponseWrapper<int>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;

    public async Task<ResponseWrapper<int>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        //Find account
        var accountInDb = await _unitOfWork.ReadRepositoryFor<Account>().GetByIdAsync(request.Transaction.AccountId);
        if(accountInDb is not null)
        {
            //know the transaction
            //validate 
            if(request.Transaction.Type == Common.Enums.TransactionType.Withdrawal)
            {
                if (request.Transaction.Amount > accountInDb.Balance)
                {
                    return new ResponseWrapper<int>().Failed(message: "Balance is not Enough.");

                }
                //create transaction
                var transaction = new Transaction()
                {
                    AccountId = accountInDb.Id,
                    Amount = request.Transaction.Amount,
                    Type = Common.Enums.TransactionType.Withdrawal,
                    Date = DateTime.Now
                };
                //update account balance
                accountInDb.Balance -= request.Transaction.Amount;

                await _unitOfWork.WriteRepositoryFor<Transaction>().AddAsync(transaction);
                await _unitOfWork.WriteRepositoryFor<Account>().UpdateAsync(accountInDb);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new ResponseWrapper<int>().Success(data: transaction.Id, message: "Withdrawal Done Succesfully.");

            }
            else if(request.Transaction.Type == Common.Enums.TransactionType.Deposit)
            {
                //create transaction
                var transaction = new Transaction()
                {
                    AccountId = accountInDb.Id,
                    Amount = request.Transaction.Amount,
                    Type = Common.Enums.TransactionType.Deposit,
                    Date = DateTime.Now
                };
                //update account balance
                accountInDb.Balance += request.Transaction.Amount;

                await _unitOfWork.WriteRepositoryFor<Transaction>().AddAsync(transaction);
                await _unitOfWork.WriteRepositoryFor<Account>().UpdateAsync(accountInDb);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new ResponseWrapper<int>().Success(data: transaction.Id, message: "Depoist Done Succesfully.");
            }

        }
        return new ResponseWrapper<int>().Failed(message: "Account Does not Exists.");
    }
}
