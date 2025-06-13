using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Webshop.Domain.Entities;
using Webshop.Infrastructure.Extensions;
using Webshop.Infrastructure.Persistence;

namespace Webshop.Api.Endpoints.Products;

public class CreateProduct : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPut("/products", HandleAsync)
            .WithTags("Product", "Admin")
            .RequireAuthorization("admin");
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] ProductPutDto dto,
        [FromServices] TimeProvider time,
        [FromServices] ShopDbContext ctx
    )
    {
        var categoryExists = await ctx.Categories.FindAsync(dto.CategoryId);
        if (categoryExists == null)
        {
            return Results.BadRequest();
        }

        var product = new Product(dto.Name, dto.NetPrice, dto.TaxRate)
        {
            ChangedDate = time.GetUtcNow(),
            Description = dto.Description,
            CategoryId = dto.CategoryId
        };
        ctx.Products.Add(product);
        await ctx.SaveChangesAsync();
        return Results.Ok();
    }

    public record ProductPutDto
    {
        [Required] public required string Name { get; init; }
        public string? Description { get; init; }
        [Required] public required double NetPrice { get; init; }
        [Required] public required double TaxRate { get; init; }
        [Required] public required int? CategoryId { get; init; }
    }
}