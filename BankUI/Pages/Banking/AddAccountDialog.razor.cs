using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class AddAccountDialog
{
    [Parameter] public int AccountHolderId { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    public CreateAccountRequest CreateAccountRequest { get; set; } = new();
    MudForm _form = default;

    private async Task SubmitAsync() 
    {
        await _form.Validate();
        if (_form.IsValid)
        {
            await SaveAsync();
        }
    }
    private async Task SaveAsync()
    {

        CreateAccountRequest.AccountHolderId = AccountHolderId;
        var response = await _accountService.AddAccountAsync(CreateAccountRequest);
        if (response.IsSuccessful)
        {
            _snackbar.Add(response.Messages[0], Severity.Success);
            MudDialog.Close();
        }
        else
        {
            foreach (var message in response.Messages)
            {
                _snackbar.Add(message, Severity.Error);
            }
        }

    }
    void Cancel() => MudDialog.Cancel();
}
