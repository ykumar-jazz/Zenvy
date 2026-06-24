using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zenvy.application.DTOs.SalesChannels;
using zenvy.application.Interfaces.Services;

namespace zenvy.api.Controller;

[Authorize]
[Route("api/v{version:apiVersion}/sales-channels")]
[ApiController]
public class SalesChannelController(ISalesChannelService salesChannelService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSalesChannels()
    {
        var response = await salesChannelService.GetSalesChannelsAsync();
        return Ok(response);
    }

    [HttpGet("{channelId:int}")]
    public async Task<IActionResult> GetSalesChannelById(int channelId)
    {
        var response = await salesChannelService.GetSalesChannelByIdAsync(channelId);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSalesChannel([FromBody] SalesChannelRequest request)
    {
        var channelId = await salesChannelService.CreateSalesChannelAsync(request);
        return CreatedAtAction(nameof(GetSalesChannelById), new { channelId }, new { ChannelId = channelId });
    }

    [HttpPut("{channelId:int}")]
    public async Task<IActionResult> UpdateSalesChannel(int channelId, [FromBody] SalesChannelRequest request)
    {
        var updated = await salesChannelService.UpdateSalesChannelAsync(channelId, request);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{channelId:int}")]
    public async Task<IActionResult> DeleteSalesChannel(int channelId)
    {
        var deleted = await salesChannelService.DeleteSalesChannelAsync(channelId);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
