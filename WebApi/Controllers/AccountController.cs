using Application.Features.Accounts.Commands;
using Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
}
