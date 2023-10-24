using Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization;
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User == null)
        {
            return Task.CompletedTask;
        }
        var permissions = context.User.Claims.Where(e => e.Type == CustomClaimTypes.Permission &&
                                                    e.Value == requirement.Permission &&
                                                    e.Issuer == "LOCAL AUTHORITY");
        if(permissions.Any())
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
