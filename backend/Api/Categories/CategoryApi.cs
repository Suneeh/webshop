using System.ComponentModel.DataAnnotations;
using backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Api.Categories;

public static class CategoryApi
{
    public static void RegisterCategoryEndpoints(this WebApplication app)
    {
        var category = app.MapGroup("/categories").WithTags("Categories");

        category.MapGet("/{id}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            return await ctx.Categories.FindAsync(id) is Category category
                ? Results.Ok(category)
                : Results.NotFound();
        });

        category.MapGet("/", async (
            [FromServices] ShopDbContext ctx
        ) =>
        {
            return await ctx.Categories.ToArrayAsync();
        });

        category.MapPut("/", async (
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
        }).RequireAuthorization("manage");

        category.MapPatch("/{id}", async (
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
        }).RequireAuthorization("manage");

        category.MapDelete("/{id}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            await ctx.Categories.Where(category => category.Id == id).ExecuteDeleteAsync();
            return Results.Ok();
        }).RequireAuthorization("manage");

    }

    public record CategoryPutDto
    {
        [Required]
        public required string Name { get; init; }
        public string? Description { get; init; }
    }

    public record CategoryPatchDto
    {
        public required RequiredValue<string>? Name { get; init; }
        public required RequiredValue<string>? Description { get; init; }
    }

    public record RequiredValue<T> where T : notnull
    {
        [Required]
        public required T NewValue { get; init; }
    }
}
