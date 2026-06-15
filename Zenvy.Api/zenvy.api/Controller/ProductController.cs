using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zenvy.application.DTOs.Products;
using zenvy.application.Interfaces.Services;

namespace zenvy.api.Controller;
[Authorize]
[Route("api/product")]
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
}