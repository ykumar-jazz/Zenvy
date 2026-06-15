using zenvy.domain.Entities;

namespace zenvy.application.DTOs.Products;
public class CreateProductRequest
{
    public ProductMaster ProductMaster { get; set; } = new ProductMaster();
    public Product Product { get; set; } = new Product();
    public List<ProductVariants> ProductVariants { get; set; } = [];
    public List<ProductImages> ProductImages { get; set; } = [];
}