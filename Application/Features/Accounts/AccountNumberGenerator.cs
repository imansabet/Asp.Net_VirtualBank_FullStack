namespace Application.Features.Accounts;

public static class AccountNumberGenerator
{
    public static string GenerateAccountNumber() => DateTime.Now.ToString("yyMMddHHmmss");
   
}
