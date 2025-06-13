using Microsoft.AspNetCore.Authorization;

namespace Webshop.Infrastructure.Authorization;

public class RbacRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission ?? throw new ArgumentNullException(nameof(permission));
}
