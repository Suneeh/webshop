using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Webshop.Infrastructure.Authorization;
using Webshop.Infrastructure.Middlewares;

namespace Webshop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, RbacHandler>();
        return services;
    }

    public static IApplicationBuilder UseSecureHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SecureHeadersMiddleware>();
    }
}