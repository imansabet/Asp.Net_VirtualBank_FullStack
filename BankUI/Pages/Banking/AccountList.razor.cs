using Common.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class AccountList
{
    [Parameter] public int AccountHolderId { get; set; }
    public List<AccountResponse> Accounts { get; set; } = [];
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
    {
        var response = await _accountService.GetAccountsByAccountHolderIdAsync(AccountHolderId);
        
        if (response.IsSuccessful)
        {
            Accounts = response.Data;
        }
        else
        {
            foreach(var message in response.Messages)
            {
                _snackbar.Add(message, Severity.Error);
            }
        }
        _loading = false;
    }

}
