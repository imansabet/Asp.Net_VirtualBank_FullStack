using Common.Requests;
using FluentValidation;

namespace BankUI.Pages.Banking.Validators;

public class TransactionValidator : AbstractValidator<TransactionRequest>
{
    public TransactionValidator()
    {
        RuleFor(transaction => transaction.Type)
            .IsInEnum();

        RuleFor(transaction => transaction.Amount)
           .GreaterThan(0M)
           .When(transaction => transaction.Type == Common.Enums.TransactionType.Deposit);


        RuleFor(transaction => transaction.Amount)
            .GreaterThan(0M)
            .LessThanOrEqualTo(transaction => transaction.CurrentBalance)
            .WithMessage("Not Enough Balance !")
            .When(transaction => transaction.Type == Common.Enums.TransactionType.Withdrawal);
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (requestModel, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TransactionRequest>
            .CreateWithOptions((TransactionRequest)requestModel, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
        {
            return Array.Empty<string>();
        }
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
