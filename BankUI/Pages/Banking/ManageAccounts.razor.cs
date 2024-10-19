using Common.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace BankUI.Pages.Banking;

public partial class ManageAccounts
{
    [Parameter] public int AccountHolderId { get; set; }
     public AccountHolderResponse AccountHolder { get; set; } = new();

    private bool _loading = true;
    protected override async Task OnInitializedAsync()
    {
        var response = await _accountHolderService.GetAccountHolderByIdAsync(AccountHolderId);
        if (response.IsSuccessful)
        {
            AccountHolder = response.Data;
        }
        else
        {
            foreach(var message in response.Messages)
            {
                _snackbar.Add(message, (MudBlazor.Severity)Severity.Error);
            }
        }
        _loading = false;
    }
    private void PageClosed() 
    {
        _navigation.NavigateTo("/banking/account-holder-list");
    
    }
}
