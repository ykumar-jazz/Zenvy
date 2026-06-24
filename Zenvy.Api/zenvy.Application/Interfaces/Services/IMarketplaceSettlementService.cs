using zenvy.application.DTOs.MarketplaceSettlements;

namespace zenvy.application.Interfaces.Services;

public interface IMarketplaceSettlementService
{
    Task<long> CreateMarketplaceSettlementAsync(MarketplaceSettlementRequest request);
    Task<IEnumerable<MarketplaceSettlementResponse>> GetMarketplaceSettlementsAsync();
    Task<MarketplaceSettlementResponse?> GetMarketplaceSettlementByIdAsync(long settlementId);
}
