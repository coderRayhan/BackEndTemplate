using Application.Features.Identity.Roles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Identity;
public class RolesController : BaseApiController
{
    [HttpGet("GetPermissions")]
    public async Task<IActionResult> GetPermissions(string roleId)
    {
        var permissions = await Mediator.Send(new GetPermissionsQuery(roleId));
        return Ok(permissions);
    }
}
