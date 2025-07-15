using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.Infrastructure.Extensions;
using Webshop.Infrastructure.Persistence;

namespace Webshop.Api.Endpoints.Products;

public class GetProduct : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/products/{id:int}", HandleAsync)
            .WithTags("Product", "Anonymous")
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [FromRoute] int id,
        [FromServices] ShopDbContext ctx
    )
    {
        var product = await ctx.Products
            .Include(prod => prod.Ratings)
            .Include(prod => prod.Discounts)
            .Select(product => new GetProductDetailDto
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
                Rating = product.GetRating(),
                Discount = product.GetBiggestDiscount()
            })
            .SingleOrDefaultAsync(prod => prod.Id == id);
        return product == null
            ? Results.NotFound()
            : Results.Ok(product);
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
        public required double? Discount { get; init; }
    }
}