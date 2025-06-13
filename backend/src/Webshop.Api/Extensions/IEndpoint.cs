using Microsoft.AspNetCore.Routing;

namespace Webshop.Infrastructure.Extensions;

public interface IEndpoint
{
    void Register(IEndpointRouteBuilder endpoints);
}
