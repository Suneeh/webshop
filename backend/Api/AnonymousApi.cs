using backend.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Api;

public static class AnonymousApi
{
    public static void RegisterAnonymousEndpoints(this WebApplication app)
    {
        var anonymousRouteGroup = app.MapGroup("/").WithTags("Anonymous").AllowAnonymous();

        anonymousRouteGroup.MapGet("/categories/{id:int}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            var res = await ctx.Categories.Include(c => c.Products).SingleOrDefaultAsync(c => c.Id == id);
            return  res != null
                ? Results.Ok(new GetCategoryDetailDto
                {
                    Id = res.Id,
                    Name = res.Name,
                    Description = res.Description,
                    Products = res.Products.Select(product => new GetProductListDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Color = product.ColorCodeHex,
                        NetPrice = product.NetPrice,
                        TaxRate = product.TaxRate,
                    }).ToArray()
                })
                : Results.NotFound();
        });

        anonymousRouteGroup.MapGet("/categories/", async (
            [FromServices] ShopDbContext ctx
        ) =>
        {
            return await ctx.Categories.Select(cat => new GetCategoryListDto
            {
                Id = cat.Id,
                Name = cat.Name,
            }).ToArrayAsync();
        });
        
        anonymousRouteGroup.MapGet("/products/{id:int}", async (
            [FromRoute] int id,
            [FromServices] ShopDbContext ctx
        ) =>
        {
            var product = await ctx.Products.FindAsync(id);
            if (product == null)
                return Results.NotFound();
            
            return Results.Ok(new GetProductDetailDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Color = product.ColorCodeHex,
                NetPrice = product.NetPrice,
                TaxRate = product.TaxRate,
                CreationDate = product.CreationDate,
                ChangedDate = product.ChangedDate,
                CategoryId = product.CategoryId,
            });
        });

        anonymousRouteGroup.MapGet("/products/", async (
            [FromServices] ShopDbContext ctx
        ) =>
        {
            return await ctx.Products.Select(prod => new GetProductDetailDto
            {
                Id = prod.Id,
                Name = prod.Name,
                Description = prod.Description,
                Color = prod.ColorCodeHex,
                NetPrice = prod.NetPrice,
                TaxRate = prod.TaxRate,
                CreationDate = prod.CreationDate,
                ChangedDate = prod.ChangedDate,
                CategoryId = prod.CategoryId
            }).ToArrayAsync();
        });
    }
    
    public record GetCategoryDetailDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string? Description { get; init; }
        public required GetProductListDto[] Products { get; init; }
    }
    
    public record GetCategoryListDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
    }
    
    public record GetProductListDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string Color { get; init; }
        public required double NetPrice { get; init; }
        public required double TaxRate { get; init; }
    }
    
    public record GetProductDetailDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string? Description { get; init; }
        public required string Color { get; init; }
        public required double NetPrice { get; init; }
        public required double TaxRate { get; init; }
        public required DateTimeOffset CreationDate { get; init; }
        public required DateTimeOffset ChangedDate { get; init; }
        public required int? CategoryId { get; init; }
    }
}