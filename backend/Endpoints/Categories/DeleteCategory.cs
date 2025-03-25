using backend.Database;
using backend.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Endpoints.Categories;

public class DeleteCategory : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapDelete("/categories/{id:int}", HandleAsync)
            .WithTags("Category", "Admin")
            .RequireAuthorization("admin");
    }

    private static async Task<IResult> HandleAsync(
        [FromRoute] int id,
        [FromServices] ShopDbContext ctx
    )
    {
        await ctx.Categories.Where(category => category.Id == id).ExecuteDeleteAsync();
        return Results.Ok();
    }
}