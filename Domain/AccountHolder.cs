namespace Domain;

public class AccountHolder : Person
{
    public string ContactNumber { get; set; }
    public string Email { get; set; }

    public List<Account> Accounts { get; set; }

    public AccountHolder Update(string firstName
        ,string lastName,string email,string contactNumber)
    {
        if (firstName is not null && FirstName?.Equals(firstName, StringComparison.CurrentCultureIgnoreCase) is not true)
            FirstName = firstName;

        if (lastName is not null && FirstName?.Equals(lastName, StringComparison.CurrentCultureIgnoreCase) is not true)
            LastName = lastName;

        if (contactNumber is not null && ContactNumber?.Equals(contactNumber, StringComparison.CurrentCultureIgnoreCase) is not true)
            ContactNumber = contactNumber;

        if (email is not null && Email?.Equals(email, StringComparison.CurrentCultureIgnoreCase) is not true)
            Email = email;

        return this;
    }
}
