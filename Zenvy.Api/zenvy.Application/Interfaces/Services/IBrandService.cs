using zenvy.application.DTOs.Brands;

namespace zenvy.Application.Interfaces.Services;

public interface IBrandService
{
    Task<int> CreateBrandAsync(BrandRequest request);

    Task<BrandResponse> GetBrandByIdAsync(int brandId);

    Task<IEnumerable<BrandResponse>> GetAllBrandsAsync();

    Task<bool> UpdateBrandAsync(int brandId, UpdateBrandRequest request);

    Task<bool> DeleteBrandAsync(int brandId);
}