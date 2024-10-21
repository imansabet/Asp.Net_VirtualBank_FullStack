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
        await LoadAccountsAsync();
    }
    private async Task LoadAccountsAsync() 
    {
        var response = await _accountService.GetAccountsByAccountHolderIdAsync(AccountHolderId);

        if (response.IsSuccessful)
        {
            Accounts = response.Data;
        }
        else
        {
            foreach (var message in response.Messages)
            {
                _snackbar.Add(message, Severity.Error);
            }
        }
        _loading = false;

    }

    private async Task AddAccountAsync() 
    {
        var parameters = new DialogParameters
        {
            { nameof(AddAccountDialog.AccountHolderId),AccountHolderId }
        };
        var options = new DialogOptions
        {
            CloseButton = true,
            FullWidth = true,
            BackdropClick = false,
        };
        var dialog = _dialogService.Show<AddAccountDialog>("Open New Account", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            await LoadAccountsAsync();
        }
    }
}
