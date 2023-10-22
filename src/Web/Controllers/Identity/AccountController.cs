using Application.Common.Interfaces;
using Application.Features.Identity.Account.Commands;
using Application.Features.Identity.Account.Queries;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Identity;
public class AccountController : BaseApiController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IJwtProvider JwtProvider { get; }

    public AccountController(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        JwtProvider = jwtProvider;
        _signInManager = signInManager;
    }
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
        var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
        if (result.Succeeded)
        {
            var userid = _userManager.FindByNameAsync(user.UserName).Result.Id;
            var token = await JwtProvider.CreateAsync(userid);
            return Ok(token);
        }
        return BadRequest("Invalid credentials");
    }
}
