using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtProvider = jwtProvider;
    }
    public async Task<(bool isSuccess, string result)> Login(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username) ?? await _userManager.FindByEmailAsync(username);
        if(user != null)
        {
            var isSuccess = await _userManager.CheckPasswordAsync(user, password);
            if(isSuccess)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = await _jwtProvider.CreateAsync(user.Id);
                return (true, token);
            }
        }
        return (false, "Invalid Credentials");
    }
}
