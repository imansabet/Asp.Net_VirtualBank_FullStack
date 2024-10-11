using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountByIdQuery : IRequest<ResponseWrapper<AccountResponse>>
{
    public int Id { get; set; }
}
public class GetAccountByIdQueryHanlder : IRequestHandler<GetAccountByIdQuery, ResponseWrapper<AccountResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAccountByIdQueryHanlder(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseWrapper<AccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var accountInDb = await _unitOfWork.ReadRepositoryFor<Account>().GetByIdAsync(request.Id);
        if(accountInDb is not null)
        {
            return new ResponseWrapper<AccountResponse>().Success(data: accountInDb.Adapt<AccountResponse>());
        }
        return new ResponseWrapper<AccountResponse>().Failed("Account Does Not Exists.");
    }
}