using Common.Enums;

namespace Common.Responses;

public class AccountResponse
{
    public string AccountNumber { get; set; }
     public bool IsActive { get; set; }
    public AccountType Type { get; set; }
    public int AccountHolderId { get; set; }
    public AccountHolderResponse AccountHolder { get; set; }
}
