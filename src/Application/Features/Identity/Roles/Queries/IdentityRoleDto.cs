using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Roles.Queries;
public class IdentityRoleDto
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public List<PermissionDto> Permissions { get; set; }
}
