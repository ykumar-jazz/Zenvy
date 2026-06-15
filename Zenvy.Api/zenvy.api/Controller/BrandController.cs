using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zenvy.application.DTOs.Brands;
using zenvy.Application.DTOs.Category;
using zenvy.Application.Interfaces.Services;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BrandsController(IBrandService brandService) : ControllerBase
{
    private readonly IBrandService brandService = brandService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BrandRequest request)
    {
        int categoryId = await brandService.CreateBrandAsync(request);
        return Ok(new { CategoryId = categoryId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,[FromBody] UpdateBrandRequest request)
    {
        bool success = await brandService.UpdateBrandAsync(id,request);
        return Ok(new { Success = success });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await brandService.DeleteBrandAsync(id);
        return Ok(new { Success = success });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await brandService.GetBrandByIdAsync(id);
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await brandService.GetAllBrandsAsync();
        return Ok(categories);
    }
}