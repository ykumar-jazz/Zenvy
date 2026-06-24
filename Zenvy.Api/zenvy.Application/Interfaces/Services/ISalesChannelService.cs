using zenvy.application.DTOs.SalesChannels;

namespace zenvy.application.Interfaces.Services;

public interface ISalesChannelService
{
    Task<IEnumerable<SalesChannelResponse>> GetSalesChannelsAsync();
    Task<SalesChannelResponse?> GetSalesChannelByIdAsync(int channelId);
    Task<int> CreateSalesChannelAsync(SalesChannelRequest request);
    Task<bool> UpdateSalesChannelAsync(int channelId, SalesChannelRequest request);
    Task<bool> DeleteSalesChannelAsync(int channelId);
}
