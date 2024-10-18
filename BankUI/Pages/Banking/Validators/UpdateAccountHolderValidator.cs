using Common.Requests;
using FluentValidation;
using MudBlazor;

namespace BankUI.Pages.Banking.Validators;

public class UpdateAccountHolderValidator : AbstractValidator<UpdateAccountHolder>
{
    public UpdateAccountHolderValidator()
    {
        RuleFor(accountHolder => accountHolder.FirstName)
            .Must(f => !string.IsNullOrEmpty(f))
            .WithMessage("First Name is required. ");

        RuleFor(accountHolder => accountHolder.LastName)
           .Must(f => !string.IsNullOrEmpty(f))
           .WithMessage("Last Name is required. ");

        RuleFor(accountHolder => accountHolder.ContactNumber)
          .NotEmpty()
          .Matches(@"^\+?\d{10,15}$")
          .WithMessage("The Contact Number should be a valid phone number with 10 to 15 digits, optionally starting with a '+'.");

        RuleFor(accountHolder => accountHolder.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is Not Valid.");
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (requestModel, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UpdateAccountHolder>
            .CreateWithOptions((UpdateAccountHolder )requestModel, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
        {
            return Array.Empty<string>();
        }
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
