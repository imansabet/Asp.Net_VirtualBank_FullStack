using Common.Responses;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class AccountHolderList
{
    public List<AccountHolderResponse> AccountHolders { get; set; } = [];
    private bool _loading = true;
    protected override async Task OnInitializedAsync()
    {
        var response = await _accountHolderService.GetAllAccountHoldersAsync();
        if (response.IsSuccessful)
        {
            AccountHolders = response.Data;
        }
        else 
        {
            foreach(var message in response.Messages)
            {
                 _snackbar.Add(message,Severity.Error);
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
            MaxWidth = MaxWidth.ExtraLarge,
            BackdropClick = false,
            //DisableBackDropClick = true 
        };
        var dialog = _dialogService.Show<AddAccountHolderDialog>("Add Account Holder", parameters, options);
        
        var result = await dialog.Result;

        if (!result.Canceled) 
        {
            await Console.Out.WriteLineAsync("Button Clicked,");
            
        }
        
    }
}
