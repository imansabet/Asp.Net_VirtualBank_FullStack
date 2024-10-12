using Common.Enums;

namespace Common.Responses;

public class AccountResponse
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public bool IsActive { get; set; }
    public decimal Balance { get; set; }
    public AccountType Type { get; set; }
    public int AccountHolderId { get; set; }
    public AccountHolderResponse AccountHolder { get; set; }
}
