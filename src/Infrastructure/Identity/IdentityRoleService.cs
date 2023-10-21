using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Identity.Roles.Queries;
using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity;
public class IdentityRoleService : IIdentityRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public IdentityRoleService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }
    public async Task<List<IdentityRoleDto>> GetRolesAsync() => _mapper.Map<List<IdentityRoleDto>>(await _roleManager.Roles.ToListAsync());

    public async Task<IdentityRoleDto> GetRoleAsync(string roleId) => _mapper.Map<IdentityRoleDto>(await _roleManager.Roles.Where(a => a.Id == roleId).FirstOrDefaultAsync());

    public async Task<string?> GetRoleNameAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
            return Guard.Against.NotFound(roleId, roleId);
        return await _roleManager.GetRoleNameAsync(role);
    }

    public async Task<(Result result, string roleId)> CreateRoleAsync(string roleName)
    {
        var role = new IdentityRole { Name = roleName };
        var result = await _roleManager.CreateAsync(role);
        return (result.ToApplicationResult(), role.Id);
    }

    public async Task<(Result result, string roleId)> UpdateRoleAsync(string roleId, string roleName)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if(role == null)
        {
            throw new NotImplementedException();
        }
        role.Name = roleName;
        var result = await _roleManager.UpdateAsync(role);
        return (result.ToApplicationResult(), role.Id);
    }

    public async Task<Result> DeleteRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            throw new NotImplementedException();
        }
        var result = await _roleManager.DeleteAsync(role);
        return (result.ToApplicationResult());
    }

    public async Task<List<PermissionDto>> GetPermissionsByRole(string roleId)
    {
        var claims = await _roleManager.GetClaimsAsync(
            await _roleManager.FindByIdAsync(roleId));
        var permissions = _mapper.Map<List<PermissionDto>>(claims);
        return permissions;
    }

    public async Task<string> AddOrRemovePermissionAsync(string roleId, List<PermissionDto> permissions)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        var claims = await _roleManager.GetClaimsAsync(role);
        foreach(var claim in claims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }
        claims = _mapper.Map<List<Claim>>(permissions);
        foreach(var permission in claims)
        {
            await _roleManager.AddClaimAsync(role, permission);
        }
        return role.Id;
    }
}
