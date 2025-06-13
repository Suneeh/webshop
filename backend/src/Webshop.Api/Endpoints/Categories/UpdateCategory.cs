using Microsoft.AspNetCore.Mvc;
using Webshop.Domain.ValueObjects;
using Webshop.Infrastructure.Extensions;
using Webshop.Infrastructure.Persistence;

namespace Webshop.Api.Endpoints.Categories;

public class UpdateCategory : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPatch("/categories/{id:int}", HandleAsync)
            .WithTags("Category", "Admin")
            .RequireAuthorization("admin");
    }

    private static async Task<IResult> HandleAsync(
        [FromRoute] int id,
        [FromBody] CategoryPatchDto dto,
        [FromServices] TimeProvider time,
        [FromServices] ShopDbContext ctx
    )
    {
        var category = await ctx.Categories.FindAsync(id);
        if (category == null) return Results.NotFound();
        if (dto.Name != null)
            category.Name = dto.Name.NewValue;
        if (dto.Description != null)
            category.Description = dto.Description.NewValue;
        await ctx.SaveChangesAsync();
        return Results.Ok();
    }

    public record CategoryPatchDto
    {
        public required RequiredValue<string>? Name { get; init; }
        public required RequiredValue<string>? Description { get; init; }
    }
}