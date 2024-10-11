using Common.Enums;

namespace Common.Requests;

public record CreateAccountRequest
    (
      int AccountHolderId 
      ,decimal Balance 
      ,AccountType Type
    );



