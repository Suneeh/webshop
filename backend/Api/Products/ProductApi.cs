using System.ComponentModel.DataAnnotations;
using backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Api.Products;

public static class ProductApi
{
    public static void RegisterProductEndpoints(this WebApplication app)
    {
        var productRouteGroup = app.MapGroup("/products").WithTags("Products");

        productRouteGroup.MapGet("/{id}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            var dbResult = await ctx.Products.FindAsync(id);
            if (dbResult == null)
                return Results.NotFound();
            
            return Results.Ok(new ProductGetDto
            {
                Id = dbResult.Id,
                Name = dbResult.Name,
                Description = dbResult.Description,
                NetPrice = dbResult.NetPrice,
                TaxRate = dbResult.TaxRate,
                CreationDate = dbResult.CreationDate,
                ChangedDate = dbResult.ChangedDate,
                CategoryId = dbResult.CategoryId,
            });
        });

        productRouteGroup.MapGet("/", async (
            [FromServices] ShopDbContext ctx
        ) =>
        {
            return await ctx.Products.Select(prod => new ProductGetDto
            {
                Id = prod.Id,
                Name = prod.Name,
                Description = prod.Description,
                NetPrice = prod.NetPrice,
                TaxRate = prod.TaxRate,
                CreationDate = prod.CreationDate,
                ChangedDate = prod.ChangedDate,
                CategoryId = prod.CategoryId,
            }).ToArrayAsync();
        });

        productRouteGroup.MapPut("/", async (
            [FromBody] ProductPutDto dto,
            [FromServices] TimeProvider time,
            [FromServices] ShopDbContext ctx
        ) =>
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
        }).RequireAuthorization("manage");

        productRouteGroup.MapPatch("/{id}", async (
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
            if (dto.CategoryId != null)
                product.CategoryId = dto.CategoryId.NewValue;
            product.ChangedDate = time.GetUtcNow();
            await ctx.SaveChangesAsync();
            return Results.Ok();
        }).RequireAuthorization("manage");

        productRouteGroup.MapDelete("/{id}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            await ctx.Products.Where(product => product.Id == id).ExecuteDeleteAsync();
            return Results.Ok();
        }).RequireAuthorization("manage");

    }

    public record ProductGetDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string? Description { get; init; }
        public required double NetPrice { get; init; }
        public required double TaxRate { get; init; }
        public required DateTimeOffset CreationDate { get; init; }
        public required DateTimeOffset ChangedDate { get; init; }
        public required int? CategoryId { get; init; }
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
        [Required]
        public required int? CategoryId { get; init; }
    }

    public record ProductPatchDto
    {
        public required RequiredValue<string>? Name { get; init; }
        public required RequiredValue<string>? Description { get; init; }
        public required RequiredValue<double>? NetPrice { get; init; }
        public required RequiredValue<double>? TaxRate { get; init; }
        public required RequiredValue<int?>? CategoryId { get; init; }
    }

    public record RequiredValue<T>
    {
        [Required]
        public required T NewValue { get; init; }
    }
}
