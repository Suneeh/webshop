using backend.Database.Entities;
using backend.Endpoints.Categories;
using backend.Endpoints.Products;

namespace backend.Services;

public interface IProductService
{ 
    int CalculateRating(Product product);
    GetProduct.GetProductDetailDto TransformProductsToDetailDto(Product product);
    IEnumerable<GetCategory.GetProductListDto> TransformProductsToListDto(IEnumerable<Product> products);
}