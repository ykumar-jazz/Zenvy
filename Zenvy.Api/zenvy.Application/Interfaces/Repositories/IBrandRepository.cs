using zenvy.application.DTOs.Brands;

namespace zenvy.application.Interfaces.Repositories;
public interface IBrandRepository
{
    Task<int> CreateAsync(BrandRequest request);

    Task<bool> UpdateAsync(int brandId,UpdateBrandRequest request);

    Task<bool> DeleteAsync(int brandId);

    Task<BrandResponse?> GetByIdAsync(int brandId);

    Task<IEnumerable<BrandResponse>> GetAllAsync();
}