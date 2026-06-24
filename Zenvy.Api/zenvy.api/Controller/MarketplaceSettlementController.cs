using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zenvy.application.DTOs.MarketplaceSettlements;
using zenvy.application.Interfaces.Services;

namespace zenvy.api.Controller;

[Authorize]
[Route("api/v{version:apiVersion}/marketplace-settlements")]
[ApiController]
public class MarketplaceSettlementController(IMarketplaceSettlementService marketplaceSettlementService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateMarketplaceSettlement([FromBody] MarketplaceSettlementRequest request)
    {
        var settlementId = await marketplaceSettlementService.CreateMarketplaceSettlementAsync(request);
        return Ok(new { SettlementId = settlementId });
    }

    [HttpGet]
    public async Task<IActionResult> GetMarketplaceSettlements()
    {
        var response = await marketplaceSettlementService.GetMarketplaceSettlementsAsync();
        return Ok(response);
    }

    [HttpGet("{settlementId:long}")]
    public async Task<IActionResult> GetMarketplaceSettlementById(long settlementId)
    {
        var response = await marketplaceSettlementService.GetMarketplaceSettlementByIdAsync(settlementId);
        if (response == null) return NotFound();
        return Ok(response);
    }
}
