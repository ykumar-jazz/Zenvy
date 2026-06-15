using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zenvy.application.DTOs.Products;
using zenvy.application.Interfaces.Services;

namespace zenvy.api.Controller;
[Authorize]
[Route("api/products")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var response = await _productService.CreateProductAsync(request);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryRequest request)
    {
        var response = await _productService.GetProductsAsync(request);
        return Ok(response);
    }

    [HttpGet("{productMasterId:int}")]
    public async Task<IActionResult> GetProductById(int productMasterId)
    {
        var response = await _productService.GetProductByIdAsync(productMasterId);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpPut("{productMasterId:int}")]
    public async Task<IActionResult> UpdateProduct(int productMasterId, [FromBody] CreateProductRequest request)
    {
        var success = await _productService.UpdateProductAsync(productMasterId, request);
        return Ok(new { Success = success });
    }

    [HttpDelete("{productMasterId:int}")]
    public async Task<IActionResult> DeleteProduct(int productMasterId)
    {
        var success = await _productService.DeleteProductAsync(productMasterId);
        return Ok(new { Success = success });
    }

    [HttpPatch("{productMasterId:int}/status")]
    public async Task<IActionResult> UpdateProductStatus(int productMasterId, [FromBody] UpdateProductStatusRequest request)
    {
        var success = await _productService.UpdateProductStatusAsync(productMasterId, request.IsActive);
        return Ok(new { Success = success });
    }

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetProductDropdown()
    {
        var response = await _productService.GetProductDropdownAsync();
        return Ok(response);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts([FromQuery] string? search)
    {
        var response = await _productService.SearchProductsAsync(search);
        return Ok(response);
    }
}
