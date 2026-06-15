using zenvy.Application.DTOs.Category;
using zenvy.Application.Interfaces.Repositories;
using zenvy.Application.Interfaces.Services;
namespace zenvy.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task<long> CreateAsync(CreateCategoryRequest request)
    {
        return await _categoryRepository.CreateAsync(request);
    }

    public Task<bool> DeleteAsync(long categoryId)
    {
       return _categoryRepository.DeleteAsync(categoryId);
    }

    public Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {

        return _categoryRepository.GetAllAsync();
    }

    public Task<CategoryResponse?> GetByIdAsync(long categoryId)
    {
        //throw new NotImplementedException();
        return _categoryRepository.GetByIdAsync(categoryId);
    }

    public Task<bool> UpdateAsync(UpdateCategoryRequest request)
    {
        //throw new NotImplementedException();    
        return _categoryRepository.UpdateAsync(request);
    }
}