using System.ComponentModel.DataAnnotations;
using backend.Database;
using backend.Database.Entities;
using backend.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace backend.Endpoints.Categories;

public class CreateCategory : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPut("/categories", HandleAsync)
            .WithTags("Category", "Admin")
            .RequireAuthorization("admin");
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] CategoryPutDto dto,
        [FromServices] TimeProvider time,
        [FromServices] ShopDbContext ctx
    )
    {
        var category = new Category(dto.Name)
        {
            Description = dto.Description,
        };
        ctx.Categories.Add(category);
        await ctx.SaveChangesAsync();
        return Results.Ok();
    }


    public record CategoryPutDto
    {
        [Required] public required string Name { get; init; }
        public string? Description { get; init; }
    }
}