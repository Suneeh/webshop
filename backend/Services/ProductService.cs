using backend.Database.Entities;
using backend.Endpoints.Categories;
using backend.Endpoints.Products;

namespace backend.Services;

public class ProductService : IProductService
{
    public int CalculateRating(Product product)
    {
        if (product.Ratings.Count == 0)
        {
            return 0;
        }

        return product.Ratings.Sum(rating => rating.Value) / product.Ratings.Count;
    }
    
    public GetProduct.GetProductDetailDto TransformProductsToDetailDto(Product product)
    {
        return new GetProduct.GetProductDetailDto
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
    
    public IEnumerable<GetCategory.GetProductListDto> TransformProductsToListDto(IEnumerable<Product> products)
    {
        return products.Select(prod => new GetCategory.GetProductListDto
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
}