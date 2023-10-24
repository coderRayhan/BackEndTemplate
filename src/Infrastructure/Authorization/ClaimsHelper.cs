using Application.Features.Identity.Roles.Queries;
using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Security.Claims;

namespace Infrastructure.Authorization;
public static class ClaimsHelper
{
    public static void GetPermissions(this List<PermissionDto> permissions, Type policy)
    {
        FieldInfo[]? fields = policy.GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fields)
        {
            permissions.Add(new PermissionDto { Type = "Permissions", Value = field.GetValue(null).ToString() });
        }
    }

    public static void GetAllPermissions(this List<PermissionDto> permissions)
    {
        List<FieldInfo>? fields = Permissions.GetAllFields();
        foreach (var field in fields)
        {
            permissions.Add(new PermissionDto { Type = "Permissions", Value = field.GetValue(null).ToString() });
        }
    }

    public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission)
    {
        var allCLaims = await roleManager.GetClaimsAsync(role);
        if(!allCLaims.Any(e => e.Type == CustomClaimTypes.Permission && e.Value == permission))
        {
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
        }
    }
}
