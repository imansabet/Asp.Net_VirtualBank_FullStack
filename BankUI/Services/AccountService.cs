using BankUI.Endpoints;
using BankUI.Extensions;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using System.Net.Http.Json;

namespace BankUI.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;

    public AccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ResponseWrapper<int>> AddAccountAsync(CreateAccountRequest createAccount)
    {
        var response = await _httpClient.PostAsJsonAsync(AccountsEndpoints.Add, createAccount);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<AccountResponse>> GetAccountByAccountNumberAsync(string accountnumber)
    {
        var response = await _httpClient.GetAsync(AccountsEndpoints.GetByAccountNumber(accountnumber));
        return await response.ToResponse<AccountResponse>();
    }

    public async Task<ResponseWrapper<AccountResponse>> GetAccountByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync(AccountsEndpoints.GetById(id));
        return await response.ToResponse<AccountResponse>();

    }

    public async Task<ResponseWrapper<List<AccountResponse>>> GetAccountsByAccountHolderIdAsync(int accountHolderId)
    {
        var response = await _httpClient.GetAsync(AccountsEndpoints.GetAccountsByAccountHolderId(accountHolderId));
        return await response.ToResponse<List<AccountResponse>>();
    }

    public async Task<ResponseWrapper<List<TransactionResponse>>> GetAccountTransactionsAsync(int accountId)
    {
        var response = await _httpClient.GetAsync(AccountsEndpoints.GetTransactionsByAccountId(accountId));
        return await response.ToResponse<List<TransactionResponse>>();
    }

    public async Task<ResponseWrapper<List<AccountResponse>>> GetAllAccountsAsync()
    {
        var response = await _httpClient.GetAsync(AccountsEndpoints.GetAll);
        return await response.ToResponse<List<AccountResponse>>();
    }

    public async Task<ResponseWrapper<int>> TransactAccountAsync(TransactionResponse transaction)
    {
        var response = await _httpClient.PostAsJsonAsync(AccountsEndpoints.Transact,transaction);
        return await response.ToResponse<int>();
    }
}
