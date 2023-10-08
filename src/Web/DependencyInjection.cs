using Application.Common.Interfaces;
using System.Reflection;
using System.Runtime.CompilerServices;
using Web.Services;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IUser, CurrentUser>();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddHttpContextAccessor();

        return services;
    }
}
