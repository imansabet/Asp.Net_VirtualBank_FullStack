using Microsoft.AspNetCore.Components;

namespace BankUI.Pages.Shared;

public partial class Header
{
    [Parameter] public string Title { get; set; }
    [Parameter] public string Description { get; set; }
}
