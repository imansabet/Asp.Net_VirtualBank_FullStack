using Microsoft.AspNetCore.Components;

namespace BankUI.Pages.Banking;

public partial class ManageAccounts
{
    [Parameter] public int AccountHolderId { get; set; }

    private void PageClosed() 
    {
        _navigation.NavigateTo("/banking/account-holder-list");
    
    }
}
