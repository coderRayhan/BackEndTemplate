using Application.Common.Interfaces;
using System.Security.Claims;

namespace Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public string? Id => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
