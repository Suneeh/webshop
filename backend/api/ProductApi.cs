using System.ComponentModel.DataAnnotations;
using backend.ShopDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class ProductApi
{
    public static void RegisterProductEndpoints(this WebApplication app)
    {
        var product = app.MapGroup("/product").WithTags("Product");

        product.MapGet("/{id}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            return await ctx.Products.FindAsync(id) is Product product
                ? Results.Ok(product)
                : Results.NotFound();
        });

        product.MapPut("/", async (
            [FromBody] ProductPutDto dto,
            [FromServices] TimeProvider time,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            var product = new Product(dto.Name, dto.NetPrice, dto.TaxRate)
            {
                ChangedDate = time.GetUtcNow(),
                Description = dto.Description,
            };
            ctx.Products.Add(product);
            await ctx.SaveChangesAsync();
            return Results.Ok();
        });

        product.MapPatch("/{id}", async (
            [FromRoute] int id,
            [FromBody] ProductPatchDto dto,
            [FromServices] TimeProvider time,
            [FromServices] ShopDbContext ctx
        ) =>
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
            product.ChangedDate = time.GetUtcNow();
            await ctx.SaveChangesAsync();
            return Results.Ok();
        });

        product.MapDelete("/{id}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            await ctx.Products.Where(product => product.Id == id).ExecuteDeleteAsync();
            return Results.Ok();
        });

    }

    public record ProductPutDto
    {
        [Required]
        public required string Name { get; init; }
        public string? Description { get; init; }
        [Required]
        public required double NetPrice { get; init; }
        [Required]
        public required double TaxRate { get; init; }
    }

    public record ProductPatchDto
    {
        public required RequiredValue<string>? Name { get; init; }
        public required RequiredValue<string>? Description { get; init; }
        public required RequiredValue<double>? NetPrice { get; init; }
        public required RequiredValue<double>? TaxRate { get; init; }
    }

    public record RequiredValue<T> where T : notnull
    {
        [Required]
        public required T NewValue { get; init; }
    }
}
