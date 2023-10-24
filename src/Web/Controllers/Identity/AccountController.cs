using Application.Common.Interfaces;
using Application.Features.Identity.Account.Commands;
using Application.Features.Identity.Account.Queries;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Identity;
public class AccountController : BaseApiController
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserDto user)
    {
        var registerCommand = Mapper.Map<RegisterCommand>(user);
        var result = await Mediator.Send(registerCommand);
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserDto user)
    {
        var response = await Mediator.Send(new LoginCommand(user.UserName, user.Password));
        if (response.isSuccess)
            return Ok(response.result);
        return BadRequest(response.result);
    }
}
