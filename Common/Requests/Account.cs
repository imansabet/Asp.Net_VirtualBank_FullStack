using Common.Enums;

namespace Common.Requests;

public record CreateAccountRequest
(
      int AccountHolderId 
      ,decimal Balance 
      ,AccountType Type
);

//public record WithdrawalRequest(int AccountId,decimal Amount);
//public record DepositRequest(int AccountId,decimal Amount);
public record TransactionRequest(int AccountId,decimal Amount,TransactionType Type);


  