using Application.Repositories;
using Common.Requests;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.AccountHolders.Command;

public class CreateAccountHolderCommand : IRequest<ResponseWrapper<int>>
{
    public CreateAccountHolder  CreateAccountHolder { get; set; }
}
public class CreateAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<CreateAccountHolderCommand, ResponseWrapper<int>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;

    public async Task<ResponseWrapper<int>> Handle(CreateAccountHolderCommand request, CancellationToken cancellationToken)
    {
        var accountHolder = request.CreateAccountHolder.Adapt<AccountHolder>();

        await _unitOfWork.WriteRepositoryFor<AccountHolder>().AddAsync(accountHolder);
        await _unitOfWork.CommitAsuync(cancellationToken);

        return new ResponseWrapper<int>().Success(accountHolder.Id, "Account Holder Created Successfully");
    }
}
