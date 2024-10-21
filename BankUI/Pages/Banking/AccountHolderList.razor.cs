using BankUI.Pages.Shared;
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
        try
        {
            var response = await _accountHolderService.GetAllAccountHoldersAsync();
            if (response.IsSuccessful)
            {
                AccountHolders = response.Data ?? new List<AccountHolderResponse>(); 
            }
            else
            {
                if (response.Messages != null && response.Messages.Any())
                {
                    foreach (var message in response.Messages)
                    {
                        _snackbar.Add(message, Severity.Error);
                    }
                }
                else
                {
                    _snackbar.Add("Error loading account holders.", Severity.Error);   
                }
            }
        }
        catch (Exception ex)
        {
            _snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
        }
        finally
        {
            _loading = false;
        }
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
    private async Task DeleteAsync(int accountHolderId,string firstName , string lastName)
    {
        string message = $"Are You Sure You Want To delete {firstName}{lastName} ?";

        var parameters = new DialogParameters 
        {
            { nameof(Shared.DeleteConfirmationDialog.Message),message},
        };
        var options = new DialogOptions
        {
            CloseButton = true,
            FullWidth = true,
            MaxWidth = MaxWidth.Small,
            BackdropClick = false,
        };
        var dialog = _dialogService.Show<DeleteConfirmationDialog>("Delete", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            var response = await _accountHolderService.DeleteAccountHolderAsync(accountHolderId);
            if (response.IsSuccessful)
            {
                await LoadAccountHoldersAsync();

                _snackbar.Add(response.Messages[0], Severity.Success);
            }
            else
            {

                foreach (var resMessage in response.Messages)
                {
                    _snackbar.Add(resMessage, Severity.Error);
                }
            }

        }
    }

    private void ManageAccounts(int accountHolderId)
    {
        _navigation.NavigateTo($"/banking/manage-accounts/{accountHolderId}");
    }
}
