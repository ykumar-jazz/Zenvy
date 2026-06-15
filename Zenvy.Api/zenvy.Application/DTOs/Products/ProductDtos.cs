using zenvy.domain.Entities;

namespace zenvy.application.DTOs.Products;

public class ProductQueryRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public int? BrandId { get; set; }
    public bool? IsActive { get; set; }
}

public class ProductResponse
{
    public int ProductMasterId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int? BrandId { get; set; }
    public string? BrandName { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ProductVariants> ProductVariants { get; set; } = [];
    public List<ProductImages> ProductImages { get; set; } = [];
}

public class ProductDropdownResponse
{
    public int ProductMasterId { get; set; }
    public string ProductName { get; set; } = string.Empty;
}

public class UpdateProductStatusRequest
{
    public bool IsActive { get; set; }
}
