using zenvy.Application.DTOs.Category;

namespace zenvy.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<long> CreateAsync(CreateCategoryRequest request);

    Task<bool> UpdateAsync(UpdateCategoryRequest request);

    Task<bool> DeleteAsync(long categoryId);

    Task<CategoryResponse?> GetByIdAsync(long categoryId);

    Task<IEnumerable<CategoryResponse>> GetAllAsync();
}