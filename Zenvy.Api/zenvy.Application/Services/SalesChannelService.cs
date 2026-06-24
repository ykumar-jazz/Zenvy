using zenvy.application.DTOs.SalesChannels;
using zenvy.application.Interfaces.Repositories;
using zenvy.application.Interfaces.Services;

namespace zenvy.Application.Services;

public class SalesChannelService(ISalesChannelRepository salesChannelRepository) : ISalesChannelService
{
    public Task<IEnumerable<SalesChannelResponse>> GetSalesChannelsAsync()
    {
        return salesChannelRepository.GetAllAsync();
    }

    public Task<SalesChannelResponse?> GetSalesChannelByIdAsync(int channelId)
    {
        return salesChannelRepository.GetByIdAsync(channelId);
    }

    public Task<int> CreateSalesChannelAsync(SalesChannelRequest request)
    {
        return salesChannelRepository.CreateAsync(request);
    }

    public Task<bool> UpdateSalesChannelAsync(int channelId, SalesChannelRequest request)
    {
        return salesChannelRepository.UpdateAsync(channelId, request);
    }

    public Task<bool> DeleteSalesChannelAsync(int channelId)
    {
        return salesChannelRepository.DeleteAsync(channelId);
    }
}
