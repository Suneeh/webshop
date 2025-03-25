using Microsoft.Extensions.DependencyInjection.Extensions;

namespace backend.Extensions;

public static class IEndpointExtensions
{
    public static IServiceCollection AddMinimalEndpoints(this IServiceCollection services)
    {
        var assembly = typeof(Program).Assembly;
        var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type));
        services.TryAddEnumerable(serviceDescriptors);
        return services;
    }
    
    public static IApplicationBuilder RegisterMinimalEndpoints(this WebApplication app)
    {
        var endpoints = app.Services
            .GetRequiredService<IEnumerable<IEndpoint>>();
        foreach (var endpoint in endpoints)
        {
            endpoint.Register(app);
        }
        return app;
    }
}