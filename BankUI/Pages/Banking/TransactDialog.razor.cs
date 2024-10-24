using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class TransactDialog
{
    [Parameter] public int AccountId { get; set; }
    [Parameter] public decimal Balance { get; set; }
    public TransactionRequest TransactionRequest { get; set; } = new();
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    MudForm _form = default;

    private async Task SubmitAsync()
    {
        await _form.Validate();
        if (_form.IsValid) 
        {
             await SaveAsync();
        }
    }
    private async Task SaveAsync( )
    {
        TransactionRequest.AccountId = AccountId;
        var response = await _accountService.TransactAccountAsync(TransactionRequest);
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
    private void Cancel() => MudDialog.Cancel();
}
