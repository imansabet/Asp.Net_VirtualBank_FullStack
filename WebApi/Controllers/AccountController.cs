using Application.Features.Accounts.Commands;
using Application.Features.Accounts.Queries;
using Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class AccountController : BaseApiController
{
    [HttpPost("add")]
    public async Task<IActionResult> AddAccountAsync([FromBody] CreateAccountRequest createAccount)
    {
        var response = await Sender.Send(new CreateAccountCommand { CreateAccount = createAccount });
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);

    }
    

    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetAccountByIdAsync(int id )
    {
        var response = await Sender.Send(new GetAccountByIdQuery() { Id = id });
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpGet("by-account-number/{accountNumber}")]
    public async Task<IActionResult> GetAccountByIdAsync(string accountNumber)
    {
        var response = await Sender.Send(new GetAccountByAccountNumberQuery() { AccountNumber = accountNumber });
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAccounts()
    {
        var response = await Sender.Send(new GetAccountsQuery() );
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);
    }


    [HttpPost("transact")]
    public async Task<IActionResult> TransactAsync([FromBody] TransactionRequest transaction)
    {
        var response = await Sender.Send(new CreateTransactionCommand { Transaction = transaction });
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);

    }
    [HttpGet("transactions/{accountId}")]
    public async Task<IActionResult> GetAccountTransactionsAsync(int accountId)
    {
        var response = await Sender.Send(new GetAccountTransactionsQuery { AccountId = accountId });
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);

    }
    [HttpGet("by-account-holder-id/{accountHolderId}")]
    public async Task<IActionResult> GetAccountsByAccountHolderIdAsync(int accountholderId)
    {
        var response = await Sender.Send(new GetAccountsByAccountHolderIdQuery { AccountHolderId = accountholderId });
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);

    }
}
