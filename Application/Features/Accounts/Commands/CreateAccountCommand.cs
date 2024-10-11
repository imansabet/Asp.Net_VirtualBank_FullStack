using Application.Repositories;
using Common.Requests;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Commands;

public class CreateAccountCommand : IRequest<ResponseWrapper<int>>
{
    public CreateAccountRequest CreateAccount { get; set; }
}


public class CreateAccountCommandHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<CreateAccountCommand,ResponseWrapper<int>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;

    public async Task<ResponseWrapper<int>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        //map incoming to Domain Account entity
        var account = request.CreateAccount.Adapt<Account>();

        //Generate Account Number -> yyMMddHHmmSS
        account.AccountNumber = AccountNumberGenerator.GenerateAccountNumber();

        //Active Account
        account.IsActive = true;

        //Cretae Account
        await _unitOfWork.WriteRepositoryFor<Account>().AddAsync(account);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(data:account.Id , "Account Created Successfully");
    }
}
