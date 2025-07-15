using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entities;
using Webshop.Infrastructure.Extensions;
using Webshop.Infrastructure.Persistence;

namespace Webshop.Api.Endpoints.Discounts;

public class CreateDiscount : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPut("/discount", HandleAsync)
            .WithTags("Discount", "Admin")
            .RequireAuthorization("admin");
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] RequestDto dto,
        [FromServices] TimeProvider time,
        [FromServices] ShopDbContext ctx
    )
    {
        var products = await ctx.Products
            .Where(product => dto.ProductIds.Contains(product.Id))
            .ToListAsync();
        var newDiscount = new Discount(dto.DiscountValue, dto.Name, dto.Description, dto.EndDate)
        {
            Products = products
        };
        ctx.Discounts.Add(newDiscount);
        await ctx.SaveChangesAsync();
        return Results.Ok();
    }

    public record RequestDto
    {
        [Required] public required string Name { get; init; }
        [Required] public required double DiscountValue { get; init; }
        [Required] public required int[] ProductIds { get; init; }
        public required string? Description { get; init; }
        public DateTimeOffset? EndDate { get; init; }
    }
}