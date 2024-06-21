using backend.Authorization;
using Microsoft.AspNetCore.Authorization;

class RbacHandler : AuthorizationHandler<RbacRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RbacRequirement requirement)
    {
        var claims = context.User.Claims;
        if (!context.User.HasClaim(c => c.Type == "permissions"))
        {
            return Task.CompletedTask;
        }

        var permission = context.User.FindFirst(c => c.Type == "permissions" && c.Value == requirement.Permission);

        if (permission == null)
        {
            return Task.CompletedTask;
        }

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}