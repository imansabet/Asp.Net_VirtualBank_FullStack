using Common.Enums;

namespace Common.Requests;

public class CreateAccountRequest
{
    public int AccountHolderId { get; set; }
    public decimal Balance { get; set; }
    public  AccountType Type { get; set; }

}


//public record WithdrawalRequest(int AccountId,decimal Amount);
//public record DepositRequest(int AccountId,decimal Amount);
public class TransactionRequest
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }

}


  