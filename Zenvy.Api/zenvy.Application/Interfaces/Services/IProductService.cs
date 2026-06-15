using zenvy.application.DTOs.Products;

namespace zenvy.application.Interfaces.Services;
public interface IProductService
{
    Task<CreateProductResponse> CreateProductAsync(CreateProductRequest request);
}