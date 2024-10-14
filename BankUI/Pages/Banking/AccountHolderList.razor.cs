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

}
