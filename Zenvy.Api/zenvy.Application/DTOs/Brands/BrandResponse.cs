namespace zenvy.application.DTOs.Brands;
public class BrandResponse
{
    public long BrandId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool Status { get; set; }
}