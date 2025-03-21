namespace backend.Api;

public static class UserApi
{
    public static void RegisterUserEndpoints(this WebApplication app)
    {
        var manageRouteGroup = app.MapGroup("/user/").WithTags("User").RequireAuthorization("user");

        manageRouteGroup.MapGet("/foo", async () => new AnonymousApi.GetProductListDto
        {
            Id = 0,
            Name = "null",
            Color = "null",
            NetPrice = 0,
            TaxRate = 0,
            Rating = 0
        });
    }
}