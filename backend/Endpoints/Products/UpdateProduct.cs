using backend.Database;
using backend.Extensions;
using backend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace backend.Endpoints.Products;

public class UpdateProduct : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPatch("/products/{id:int}", HandleAsync)
            .WithTags("Product", "Admin")
            .RequireAuthorization("admin");
    }

    private static async Task<IResult> HandleAsync(
        [FromRoute] int id,
        [FromBody] ProductPatchDto dto,
        [FromServices] TimeProvider time,
        [FromServices] ShopDbContext ctx
    )
    {
        var product = await ctx.Products.FindAsync(id);
        if (product == null) return Results.NotFound();
        if (dto.Name != null)
            product.Name = dto.Name.NewValue;
        if (dto.Description != null)
            product.Description = dto.Description.NewValue;
        if (dto.TaxRate != null)
            product.TaxRate = dto.TaxRate.NewValue;
        if (dto.NetPrice != null)
            product.NetPrice = dto.NetPrice.NewValue;
        if (dto.CategoryId != null)
            product.CategoryId = dto.CategoryId.NewValue;
        product.ChangedDate = time.GetUtcNow();
        await ctx.SaveChangesAsync();
        return Results.Ok();
    }

    public record ProductPatchDto
    {
        public required RequiredValue<string>? Name { get; init; }
        public required RequiredValue<string>? Description { get; init; }
        public required RequiredValue<double>? NetPrice { get; init; }
        public required RequiredValue<double>? TaxRate { get; init; }
        public required RequiredValue<int>? CategoryId { get; init; }
    }
}