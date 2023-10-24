using Application.Common.Interfaces;
using System.Reflection;
using System.Runtime.CompilerServices;
using Web.Services;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddHttpContextAccessor();
        services.AddScoped<IUser, CurrentUser>();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", o =>
            {
                o.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });
        });

        return services;
    }
}
