using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace BankUI.Services;

public interface IAccountHoldersService
{
    Task<ResponseWrapper<int>> AddAccountHolderAsync(CreateAccountHolder createAccountHolder);
    Task<ResponseWrapper<int>> UpdateAccountHolderAsync(UpdateAccountHolder updateAccountHolder);
    Task<ResponseWrapper<int>> DeleteAccountHolderAsync(int id);
    Task<ResponseWrapper<AccountHolderResponse>> GetAccountHolderByIdAsync(int id);
    Task<ResponseWrapper<List<AccountHolderResponse>>> GetAllAccountHoldersAsync();
}
