using Application.Features.AccountHolders.Command;
using Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
public class AccountHoldersController : BaseApiController
{
    [HttpPost("add")]
    public async Task<IActionResult> AddAccountHolderAsync([FromBody] CreateAccountHolder createAccountHolder )
    {
        var response = await Sender.Send(new CreateAccountHolderCommand { CreateAccountHolder = createAccountHolder });
        
        if (response.IsSuccessful) 
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

}
