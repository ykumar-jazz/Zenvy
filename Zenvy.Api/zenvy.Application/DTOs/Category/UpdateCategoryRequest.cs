
namespace zenvy.Application.DTOs.Category;
public class UpdateCategoryRequest
{
    public long CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}