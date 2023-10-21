using Application.Features.Identity.Account.Commands;
using Application.Features.Identity.Account.Queries;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Identity;
public class AccountController : BaseApiController
{

    public async Task<IActionResult> Register([FromBody] UserDto user)
    {
        var registerCommand = Mapper.Map<RegisterCommand>(user);
        var result = await Mediator.Send(registerCommand);
        return null;
    }
}
