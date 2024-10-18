using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Shared;

public partial class DeleteConfirmationDialog
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Parameter] public string Message { get; set; }

    void Agreed() => MudDialog.Close(DialogResult.Ok(true));
    void Cancel() => MudDialog.Cancel();

}
