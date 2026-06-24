using zenvy.application.DTOs.MarketplaceSettlements;

namespace zenvy.application.Interfaces.Repositories;

public interface IMarketplaceSettlementRepository
{
    Task<long> CreateAsync(MarketplaceSettlementRequest request);
    Task<IEnumerable<MarketplaceSettlementResponse>> GetAllAsync();
    Task<MarketplaceSettlementResponse?> GetByIdAsync(long settlementId);
}
