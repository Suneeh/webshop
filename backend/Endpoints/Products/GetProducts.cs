using System.Globalization;
using backend.Database;
using backend.Database.Entities;
using backend.Extensions;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Endpoints.Products;

public class GetProducts : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/products", HandleAsync)
            .WithTags("Product", "Anonymous")
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [FromQuery] string? sortBy,
        [FromQuery] string? sortOrder,
        [FromQuery] int? skip,
        [FromQuery] int? take,
        [FromServices] ShopDbContext ctx,
        [FromServices] IProductService productService
    )
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
        return Results.Ok(productService.TransformProductsToListDto(await query.ToArrayAsync()));
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
}
