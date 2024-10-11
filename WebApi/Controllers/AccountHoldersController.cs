using Application.Features.AccountHolders.Commands;
using Application.Features.AccountHolders.Queries;
using Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class AccountHoldersController : BaseApiController
{
    [HttpPost("add")]
    public async Task<IActionResult> AddAccountHolderAsync([FromBody] CreateAccountHolder createAccountHolder)
    {
        var response = await Sender.Send(new CreateAccountHolderCommand { CreateAccountHolder = createAccountHolder });

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAccountHolderAsync([FromBody] UpdateAccountHolder updateAccountHolder)
    {
        var response = await Sender.Send(new UpdateAccountHolderCommand { UpdateAccountHolder = updateAccountHolder });

        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccountHolderAsync(int id)
    {
        var response = await Sender.Send(new DeleteAccountHolderCommand { Id = id }); 
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountHolderByIdAsync(int id)
    {
        var response = await Sender.Send(new GetAccountHolderByIdQuery { Id = id });
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAccountHoldersAsync()
    {
        var response = await Sender.Send(new GetAccountHoldersQuery () );
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);
    }
}
