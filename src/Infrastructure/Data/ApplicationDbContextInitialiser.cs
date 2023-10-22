using Domain.Constants;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using static Domain.Constants.Permissions;

namespace Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger, 
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured during the initialization of database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while seeding the database");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        //Default Roles
        var superAdminRole = new IdentityRole(Roles.SuperAdmin.ToLower());

        if(_roleManager.Roles.All(r => r.Name != superAdminRole.Name))
        {
            await _roleManager.CreateAsync(superAdminRole);
        }

        // default user
        var superAdmin = new ApplicationUser { UserName = "superadmin@gmail.com", Email = "superadmin@gmail.com", EmailConfirmed = true };
        if(_userManager.Users.All(u => u.UserName != superAdmin.UserName || u.Email != superAdmin.Email))
        {
            await _userManager.CreateAsync(superAdmin, "123Pa$$word!");
            if (!string.IsNullOrEmpty(superAdminRole.Name))
            {
                await _userManager.AddToRolesAsync(superAdmin, new[] { superAdminRole.Name });
            }
        }

        //default permissions
        await _roleManager.AddClaimAsync(superAdminRole, new Claim(CustomClaimTypes.Permission, ApplicationUsers.Create));
        await _roleManager.AddClaimAsync(superAdminRole, new Claim(CustomClaimTypes.Permission, ApplicationUsers.View));
        await _roleManager.AddClaimAsync(superAdminRole, new Claim(CustomClaimTypes.Permission, ApplicationUsers.Edit));
        await _roleManager.AddClaimAsync(superAdminRole, new Claim(CustomClaimTypes.Permission, ApplicationUsers.Delete));
    }
}
