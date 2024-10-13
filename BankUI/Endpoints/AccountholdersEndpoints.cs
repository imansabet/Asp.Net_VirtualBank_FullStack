namespace BankUI.Endpoints;

public static class AccountHoldersEndpoints
{
    public const string Add = "api/accountholders/add";
    public const string Update = "api/accountholders/update";
    public const string Delete = "api/accountholders";
    public const string GetAll = "api/accountholders/all";

    public static string GetById(int id)
    {
        return $"api/accountholders/{id}";
    }
}
