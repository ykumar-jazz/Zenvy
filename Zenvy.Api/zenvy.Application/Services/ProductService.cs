using zenvy.application.DTOs.Products;
using zenvy.application.Interfaces.Repositories;
using zenvy.application.Interfaces.Services;

namespace zenvy.Application.Services;

public class ProductService(IProductsRepository productRepository) : IProductService
{
    public async Task<CreateProductResponse> CreateProductAsync(CreateProductRequest request)
    {
        var productId = await productRepository.CreateAsync(request);
        return new CreateProductResponse
        {
            ProductId = productId,
            Message = "Product created successfully"
        };
    }
}