using Application.Repositories;
using Common.Wrapper;
using Domain;
using MediatR;

namespace Application.Features.AccountHolders.Command;

public class DeleteAccountHolderCommand : IRequest<ResponseWrapper<int>>
{
    public int Id { get; set; }
}

public class DeleteAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<DeleteAccountHolderCommand, ResponseWrapper<int>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;

    public async Task<ResponseWrapper<int>> Handle(DeleteAccountHolderCommand request, CancellationToken cancellationToken)
    {
        var accountHolderInDb = await _unitOfWork.ReadRepositoryFor<AccountHolder>().GetByIdAsync(request.Id);
        if(accountHolderInDb is not null)
        {
            await _unitOfWork.WriteRepositoryFor<AccountHolder>().DeleteAsync(accountHolderInDb);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new ResponseWrapper<int>().Success(accountHolderInDb.Id, "Account Holder Deleted Successfully.");
        }
        return new ResponseWrapper<int>().Failed("Account Holder Does Not Exists.");
    }
}
