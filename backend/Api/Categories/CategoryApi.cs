using System.ComponentModel.DataAnnotations;
using backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Api.Categories;

public static class CategoryApi
{
    public static void RegisterCategoryEndpoints(this WebApplication app)
    {
        var categoryRouteGroup = app.MapGroup("/categories").WithTags("Categories");

        categoryRouteGroup.MapGet("/{id:int}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) => await ctx.Categories.FindAsync(id) is { } category
            ? Results.Ok((object?)category)
            : Results.NotFound());

        categoryRouteGroup.MapGet("/", async (
            [FromServices] ShopDbContext ctx
        ) => await ctx.Categories.ToArrayAsync());

        categoryRouteGroup.MapPut("/", async (
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

        categoryRouteGroup.MapPatch("/{id:int}", async (
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

        categoryRouteGroup.MapDelete("/{id:int}", async (
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
