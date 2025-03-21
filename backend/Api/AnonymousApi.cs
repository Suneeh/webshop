using System.Globalization;
using backend.Database;
using backend.Database.Entities;
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
            var res = await ctx.Categories
                .Include(c => c.Products)
                .ThenInclude(prod => prod.Ratings)
                .SingleOrDefaultAsync(c => c.Id == id);
            return res != null
                ? Results.Ok(new GetCategoryDetailDto
                {
                    Id = res.Id,
                    Name = res.Name,
                    Description = res.Description,
                    Products = TransformProductsToListDto(res.Products).ToArray()
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
            var product = await ctx.Products
                .Include(prod => prod.Ratings)
                .SingleOrDefaultAsync(prod => prod.Id == id);
            return product == null
                ? Results.NotFound()
                : Results.Ok(TransformProductsToDetailDto(product));
        });

        anonymousRouteGroup.MapGet("/products", async (
            [FromServices] ShopDbContext ctx,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortOrder,
            [FromQuery] int? skip,
            [FromQuery] int? take
        ) =>
        {
            IQueryable<Product> query = ctx.Products.Include(prod => prod.Ratings);
            if (string.IsNullOrEmpty(sortBy))
                sortBy = "Name";
            if (string.IsNullOrEmpty(sortOrder))
                sortOrder = "asc";
            if (string.IsNullOrEmpty(sortOrder))
                sortOrder = "asc";
            sortBy = char.ToUpper(sortBy[0], CultureInfo.InvariantCulture) + sortBy[1..];
            query = sortOrder == "asc"
                ? query.OrderBy(product => EF.Property<object>(product, sortBy))
                : query.OrderByDescending(product => EF.Property<object>(product, sortBy));

            query = query.Skip(skip ?? 0);
            query = query.Take(take ?? 15);
            return TransformProductsToListDto(await query.ToArrayAsync());
        });
    }

    private static int CalculateRating(Product product)
    {
        if (product.Ratings.Count == 0)
        {
            return 0;
        }
        return product.Ratings.Sum(rating => rating.Value) / product.Ratings.Count;
    }

    private static GetProductDetailDto TransformProductsToDetailDto(Product product)
    {
        return new GetProductDetailDto
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
            Rating = CalculateRating(product)
        };
    }

    private static IEnumerable<GetProductListDto> TransformProductsToListDto(IEnumerable<Product> products)
    {
        return products.Select(prod => new GetProductListDto
            {
                Id = prod.Id,
                Name = prod.Name,
                Color = prod.ColorCodeHex,
                NetPrice = prod.NetPrice,
                TaxRate = prod.TaxRate,
                Rating = CalculateRating(prod)
            })
            .OrderByDescending(product => product.Color)
            .ToList();
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
        public required double Rating { get; init; }
    }

    public record GetProductDetailDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string? Description { get; init; }
        public required string Color { get; init; }
        public required double NetPrice { get; init; }
        public required double TaxRate { get; init; }
        public required double Rating { get; init; }
        public required DateTimeOffset CreationDate { get; init; }
        public required DateTimeOffset ChangedDate { get; init; }
        public required int? CategoryId { get; init; }
    }
}