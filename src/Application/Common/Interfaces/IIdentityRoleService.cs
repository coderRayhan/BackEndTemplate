using Application.Common.Models;
using Application.Features.Identity.Roles.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;
public interface IIdentityRoleService
{
    Task<List<IdentityRoleDto>> GetRolesAsync();
    Task<IdentityRoleDto> GetRoleAsync(string roleId);
    Task<string?> GetRoleNameAsync(string roleId);
    Task<(Result result, string roleId)> CreateRoleAsync(string roleName);
    Task<(Result result, string roleId)> UpdateRoleAsync(string roleId, string roleName);
    Task<Result> DeleteRoleAsync(string roleId);
    Task<IdentityRoleDto> GetPermissionsByRoleAsync(string roleId);
    Task<string> AddOrRemovePermissionAsync(string roleId, List<PermissionDto> permissions);
}
