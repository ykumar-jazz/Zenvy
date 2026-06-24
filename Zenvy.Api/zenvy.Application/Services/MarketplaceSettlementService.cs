using zenvy.application.DTOs.MarketplaceSettlements;
using zenvy.application.Interfaces.Repositories;
using zenvy.application.Interfaces.Services;

namespace zenvy.Application.Services;

public class MarketplaceSettlementService(IMarketplaceSettlementRepository marketplaceSettlementRepository) : IMarketplaceSettlementService
{
    public Task<long> CreateMarketplaceSettlementAsync(MarketplaceSettlementRequest request)
    {
        return marketplaceSettlementRepository.CreateAsync(request);
    }

    public Task<IEnumerable<MarketplaceSettlementResponse>> GetMarketplaceSettlementsAsync()
    {
        return marketplaceSettlementRepository.GetAllAsync();
    }

    public Task<MarketplaceSettlementResponse?> GetMarketplaceSettlementByIdAsync(long settlementId)
    {
        return marketplaceSettlementRepository.GetByIdAsync(settlementId);
    }
}
