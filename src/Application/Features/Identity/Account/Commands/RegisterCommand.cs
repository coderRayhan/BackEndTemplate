using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Account.Commands;
public class RegisterCommand : IRequest<string>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.CreateUserAsync(request.UserName, request.Password);
        if (response.Result.Succeeded)
        {
            return response.UserId;
        }
        return response.Result.Errors.ToString();
    }
}
