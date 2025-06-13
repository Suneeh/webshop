using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.Infrastructure.Extensions;
using Webshop.Infrastructure.Persistence;

namespace Webshop.Api.Endpoints.Categories;

public class GetCategories : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/categories", HandleAsync)
            .WithTags("Category", "Anonymous")
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [FromServices] ShopDbContext ctx
    )
    {
        return Results.Ok(await ctx.Categories.Select(cat => new GetCategoryListDto
        {
            Id = cat.Id,
            Name = cat.Name,
        }).ToArrayAsync());
    }

    public record GetCategoryListDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
    }
}