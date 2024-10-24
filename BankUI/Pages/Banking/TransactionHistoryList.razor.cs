using Common.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class TransactionHistoryList
{
    public List<TransactionResponse> Transactions { get; set; } = [];
    [Parameter] public int AccountId { get; set; }
    public AccountResponse Account { get; set; } = new();
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
    {
        await GetAccountDetailsAsync();
        await LoadTransactionsAsync();   
        _loading = false;
    }
    private async Task GetAccountDetailsAsync() 
    {
        var response = await _accountService.GetAccountByIdAsync(AccountId);
        if (response.IsSuccessful)
        {
            Account = response.Data;
        }
        else
        {
            foreach (var message in response.Messages)
            {
                _snackbar.Add(message, Severity.Error);
            }
        }
    }
    private async Task LoadTransactionsAsync() 
    {
        var response = await _accountService.GetAccountTransactionsAsync(AccountId);
        if (response.IsSuccessful)
        {
            Transactions = response.Data;
        }
        else
        {
            foreach (var message in response.Messages)
            {
                _snackbar.Add(message, Severity.Error);
            }
        }
    }
    private void PageClosed()
    {
        _navigation.NavigateTo($"/banking/manage-accounts/{Account.AccountHolderId}");

    }

}
