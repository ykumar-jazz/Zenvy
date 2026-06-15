using zenvy.application.DTOs.Products;
namespace zenvy.application.Interfaces.Repositories;
public interface IProductsRepository
{
    Task<int> CreateAsync(CreateProductRequest request);
}