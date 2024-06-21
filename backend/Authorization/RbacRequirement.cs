using Microsoft.AspNetCore.Authorization;

namespace backend.Authorization;

class RbacRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission ?? throw new ArgumentNullException(nameof(permission));
}
