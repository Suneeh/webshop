using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.Infrastructure.Extensions;
using Webshop.Infrastructure.Persistence;

namespace Webshop.Api.Endpoints.Products;

public class DeleteProduct : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapDelete("/products/{id:int}", HandleAsync)
            .WithTags("Product", "Admin")
            .RequireAuthorization("admin");
    }

    private static async Task<IResult> HandleAsync(
        [FromRoute] int id,
        [FromServices] ShopDbContext ctx
    )
    {
        await ctx.Products.Where(product => product.Id == id).ExecuteDeleteAsync();
        return Results.Ok();
    }
}