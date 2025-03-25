using backend.Database;
using backend.Extensions;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Endpoints.Products;

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
        [FromServices] ShopDbContext ctx,
        [FromServices] IProductService productService
    )
    {
        var product = await ctx.Products
            .Include(prod => prod.Ratings)
            .SingleOrDefaultAsync(prod => prod.Id == id);
        return product == null
            ? Results.NotFound()
            : Results.Ok(productService.TransformProductsToDetailDto(product));
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