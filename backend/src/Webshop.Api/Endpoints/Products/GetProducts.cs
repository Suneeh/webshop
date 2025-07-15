using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entities;
using Webshop.Infrastructure.Extensions;
using Webshop.Infrastructure.Persistence;

namespace Webshop.Api.Endpoints.Products;

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
        [FromServices] ShopDbContext ctx
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
        var products = await query.ToArrayAsync();
        return Results.Ok(products.Select(product => new GetProductListDto
        {
            Id = product.Id,
            Name = product.Name,
            Color = product.ColorCodeHex,
            NetPrice = product.NetPrice,
            TaxRate = product.TaxRate,
            Rating = product.GetRating(),
            Discount = product.GetBiggestDiscount()
        }).ToArray());
    }

    public record GetProductListDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string Color { get; init; }
        public required double NetPrice { get; init; }
        public required double TaxRate { get; init; }
        public required double Rating { get; init; }
        public required double Discount { get; init; }
    }
}