using Common.Enums;
using System.Security.Principal;

namespace Common.Responses;

public class TransactionResponse
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }


}
