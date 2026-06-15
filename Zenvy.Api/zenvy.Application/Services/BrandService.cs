using zenvy.application.DTOs.Brands;
using zenvy.application.Interfaces.Repositories;
using zenvy.Application.Interfaces.Services;
using zenvy.Domain.Entities;

namespace zenvy.application.Services;
public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<int> CreateBrandAsync(BrandRequest request)
    {

        var createdBrand = await _brandRepository.CreateAsync(request);
        return createdBrand;
    }

    public async Task<BrandResponse> GetBrandByIdAsync(int brandId)
    {
        var brand = await _brandRepository.GetByIdAsync(brandId) 
        ?? throw new KeyNotFoundException("Brand not found");
        return new BrandResponse
        {
            BrandId = brand.BrandId,
            Name = brand.Name,
            Description = brand.Description,
            Status = brand.Status
        };
    }

    public async Task<IEnumerable<BrandResponse>> GetAllBrandsAsync()
    {
        var brands = await _brandRepository.GetAllAsync();
        return brands.Select(b => new BrandResponse
        {
            BrandId = b.BrandId,
            Name = b.Name,
            Description = b.Description,
            Status = b.Status
        });
    }

    public async Task<bool> UpdateBrandAsync(int brandId, UpdateBrandRequest request)
    {
        _ = await _brandRepository.GetByIdAsync(brandId) 
        ?? throw new KeyNotFoundException("Brand not found");
        var update=new UpdateBrandRequest
        {
            Name = request.Name,
            Description = request.Description,
            Status = request.Status
        };

       return await _brandRepository.UpdateAsync(brandId, update);
    }

    public async Task<bool> DeleteBrandAsync(int brandId)
    {
        _ = await _brandRepository.GetByIdAsync(brandId) ?? throw new KeyNotFoundException("Brand not found");
       return await _brandRepository.DeleteAsync(brandId);
    }
}