using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.Infrastructure.Extensions;
using Webshop.Infrastructure.Persistence;

namespace Webshop.Api.Endpoints.Categories;

public class GetCategory : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/categories/{id:int}", HandleAsync)
            .WithTags("Category", "Anonymous")
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [FromRoute] int id,
        [FromServices] ShopDbContext ctx)
    {
        var res = await ctx.Categories
            .Include(c => c.Products)
            .ThenInclude(prod => prod.Ratings)
            .Include(c => c.Products)
            .ThenInclude(prod => prod.Discounts)
            .SingleOrDefaultAsync(c => c.Id == id);
        return res != null
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
                    Rating = product.GetRating(),
                    Discount = product.GetBiggestDiscount()
                }).ToArray()
            })
            : Results.NotFound();
    }

    public record GetCategoryDetailDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string? Description { get; init; }
        public required GetProductListDto[] Products { get; init; }
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