using BankUI.Endpoints;
using BankUI.Extensions;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using System.Net.Http.Json;

namespace BankUI.Services;

public class AccountHolderService(HttpClient httpClient) : IAccountHoldersService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ResponseWrapper<int>> AddAccountHolderAsync(CreateAccountHolder createAccountHolder)
    {
        var response = await _httpClient.PostAsJsonAsync(AccountHoldersEndpoints.Add, createAccountHolder);
        return await response.ToResponse<int>();
    }

    public async Task<ResponseWrapper<int>> DeleteAccountHolderAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{AccountHoldersEndpoints.Delete}/{id}");
        return await response.ToResponse<int>();

    }

    public async Task<ResponseWrapper<AccountHolderResponse>> GetAccountHolderByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync(AccountHoldersEndpoints.GetById(id));
        return await response.ToResponse<AccountHolderResponse>();

    }

    public async Task<ResponseWrapper<List<AccountHolderResponse>>> GetAllAccountHoldersAsync()
    {
        var response = await _httpClient.GetAsync(AccountHoldersEndpoints.GetAll);
        return await response.ToResponse<List<AccountHolderResponse>>();
    }

    public async Task<ResponseWrapper<int>> UpdateAccountHolderAsync(UpdateAccountHolder updateAccountHolder)
    {
        var response = await _httpClient.PostAsJsonAsync(AccountHoldersEndpoints.Update, updateAccountHolder);
        return await response.ToResponse<int>();
    }
}
