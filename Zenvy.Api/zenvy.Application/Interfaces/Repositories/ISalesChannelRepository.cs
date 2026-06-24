using zenvy.application.DTOs.SalesChannels;

namespace zenvy.application.Interfaces.Repositories;

public interface ISalesChannelRepository
{
    Task<IEnumerable<SalesChannelResponse>> GetAllAsync();
    Task<SalesChannelResponse?> GetByIdAsync(int channelId);
    Task<int> CreateAsync(SalesChannelRequest request);
    Task<bool> UpdateAsync(int channelId, SalesChannelRequest request);
    Task<bool> DeleteAsync(int channelId);
}
