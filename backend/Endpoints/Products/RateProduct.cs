using System.ComponentModel.DataAnnotations;
using backend.Database;
using backend.Database.Entities;
using backend.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Endpoints.Products;

public class RateProduct : IEndpoint
{
    public void Register(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/products/{id:int}/rate", HandleAsync)
            .WithTags("Product", "User")
            .RequireAuthorization("user");
    }

    private static async Task<IResult> HandleAsync(
        [FromRoute] int id,
        [FromBody] RateProductDto dto,
        [FromServices] TimeProvider time,
        [FromServices] ShopDbContext ctx,
        HttpContext context
    )
    {
        var product = await ctx.Products.FindAsync(id);
        if (product == null)
        {
            return Results.BadRequest();
        }

        const string emailClaimKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        var mail = context.User.Claims.FirstOrDefault(c => c.Type == emailClaimKey)?.Value;
        if (mail == null)
        {
            return Results.BadRequest();
        }

        var rating = await ctx.Ratings.SingleOrDefaultAsync(rating => rating.ProductId == id && rating.Email == mail) ??
                     new Rating(mail, dto.Rating, id);

        ctx.Ratings.Add(rating);
        await ctx.SaveChangesAsync();
        return Results.Ok();
    }

    public record RateProductDto
    {
        [Required] public required int Rating { get; init; }
    }
}