using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.AccountHolders.Queries;

public class GetAccountHolderByIdQuery : IRequest<ResponseWrapper<AccountHolderResponse>>
{
    public int Id { get; set; }
}
public class GetAccountHolderByIdQueryHandler(IUnitOfWork<int> unitOfWork) : IRequestHandler<GetAccountHolderByIdQuery, ResponseWrapper<AccountHolderResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;

    public async Task<ResponseWrapper<AccountHolderResponse>> Handle(GetAccountHolderByIdQuery request, CancellationToken cancellationToken)
    {
        var accountHolderInDb = await _unitOfWork.ReadRepositoryFor<AccountHolder>()
            .GetByIdAsync(request.Id);

        if(accountHolderInDb is not null)
        {
            return new ResponseWrapper<AccountHolderResponse>().Success(accountHolderInDb.Adapt<AccountHolderResponse>());
        }
        return new ResponseWrapper<AccountHolderResponse>().Failed("AccountHolder Does Not Exists.");
    }
}
