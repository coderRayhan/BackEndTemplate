using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Roles.Queries;
public record GetPermissionsQuery(string RoleId) : IRequest<IdentityRoleDto>;
internal sealed class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, IdentityRoleDto>
{
    private readonly IIdentityRoleService _identityRoleService;

    public GetPermissionsQueryHandler(IIdentityRoleService identityRoleService)
    {
        _identityRoleService = identityRoleService;
    }

    public async Task<IdentityRoleDto> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var permissions = await _identityRoleService.GetPermissionsByRoleAsync(request.RoleId);
            return permissions;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
