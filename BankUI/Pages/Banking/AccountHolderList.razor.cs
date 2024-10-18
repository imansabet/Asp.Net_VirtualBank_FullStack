using Common.Requests;
using Common.Responses;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class AccountHolderList
{
    public List<AccountHolderResponse> AccountHolders { get; set; } = new List<AccountHolderResponse>();
    private bool _loading = true;
    protected override async Task OnInitializedAsync()
    {
        await LoadAccountHoldersAsync();

    }

    private async Task LoadAccountHoldersAsync()
    {
        var response = await _accountHolderService.GetAllAccountHoldersAsync();
        if (response.IsSuccessful)
        {
            AccountHolders = response.Data;
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

    private async Task AddAccountHolderAsync()
    {
        var parameters = new DialogParameters();
        var options = new DialogOptions
        {
            CloseButton = true,
            //MaxWidth = MaxWidth.ExtraLarge,
            FullWidth = true,
            BackdropClick = false,
            //DisableBackDropClick = true 
        };
        var dialog = _dialogService.Show<AddAccountHolderDialog>("Add Account Holder", parameters, options);

        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadAccountHoldersAsync();
            //await Console.Out.WriteLineAsync("Button Clicked,");
        }

    }
    private async Task UpdateAccountHolderAsync(int accountHolderId)
    {
        var parameters = new DialogParameters();
        var accountHolder = AccountHolders.FirstOrDefault(accountHolder => accountHolder.Id == accountHolderId);
        if (accountHolder == null)
        {
            _snackbar.Add("Account holder not found", Severity.Error);
            return;
        }
        parameters.Add(nameof(UpdateAccountHolderDialog.UpdateAccountHolderRequest), new UpdateAccountHolder
        {
            Id  = accountHolderId,
            FirstName = accountHolder.FirstName,
            LastName = accountHolder.LastName,
            ContactNumber = accountHolder.ContactNumber,
            Email = accountHolder.Email
        });
        var options = new DialogOptions
        {
            CloseButton = true,
            FullWidth = true,
            BackdropClick = false,
        };
        var dialog = _dialogService.Show<UpdateAccountHolderDialog>("Update Account Holder", parameters, options);

        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadAccountHoldersAsync();
            //await Console.Out.WriteLineAsync("Button Clicked,");
        }
    }
}
