using System.ComponentModel.DataAnnotations;
using backend.Database;
using backend.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Api;

public static class ManageApi
{
    public static void RegisterManageEndpoints(this WebApplication app)
    {
        var manageRouteGroup = app.MapGroup("/manage/").WithTags("Manage").RequireAuthorization("manage");

        manageRouteGroup.MapPut("/categories/", async (
            [FromBody] CategoryPutDto dto,
            [FromServices] TimeProvider time,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            var category = new Category(dto.Name)
            {
                Description = dto.Description,
            };
            ctx.Categories.Add(category);
            await ctx.SaveChangesAsync();
            return Results.Ok();
        });

        manageRouteGroup.MapPatch("/categories/{id:int}", async (
            [FromRoute] int id,
            [FromBody] CategoryPatchDto dto,
            [FromServices] TimeProvider time,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            var category = await ctx.Categories.FindAsync(id);
            if (category == null) return Results.NotFound();
            if (dto.Name != null)
                category.Name = dto.Name.NewValue;
            if (dto.Description != null)
                category.Description = dto.Description.NewValue;
            await ctx.SaveChangesAsync();
            return Results.Ok();
        });

        manageRouteGroup.MapDelete("/categories/{id:int}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            await ctx.Categories.Where(category => category.Id == id).ExecuteDeleteAsync();
            return Results.Ok();
        });

        manageRouteGroup.MapPut("/products/", async (
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
        });

        manageRouteGroup.MapPatch("/products/{id:int}", async (
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
        });

        manageRouteGroup.MapDelete("/products/{id:int}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            await ctx.Products.Where(product => product.Id == id).ExecuteDeleteAsync();
            return Results.Ok();
        });
    }

    public record CategoryPutDto
    {
        [Required] public required string Name { get; init; }
        public string? Description { get; init; }
    }

    public record CategoryPatchDto
    {
        public required RequiredValue<string>? Name { get; init; }
        public required RequiredValue<string>? Description { get; init; }
    }

    public record RequiredValue<T> where T : notnull
    {
        [Required] public required T NewValue { get; init; }
    }

    public record ProductPutDto
    {
        [Required] public required string Name { get; init; }
        public string? Description { get; init; }
        [Required] public required double NetPrice { get; init; }
        [Required] public required double TaxRate { get; init; }
        [Required] public required int? CategoryId { get; init; }
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