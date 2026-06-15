using zenvy.application.DTOs.Inventory;

namespace zenvy.application.Interfaces.Repositories;

public interface IInventoryRepository
{
    Task<int> CreateAsync(InventoryRequest request);
    Task<IEnumerable<InventoryResponse>> GetAllAsync();
    Task<InventoryResponse?> GetByIdAsync(int inventoryId);
    Task<bool> UpdateAsync(int inventoryId, InventoryRequest request);
    Task<bool> AdjustAsync(InventoryAdjustmentRequest request);
    Task<bool> DamageAsync(InventoryDamageRequest request);
    Task<bool> TransferAsync(InventoryTransferRequest request);
    Task<IEnumerable<InventoryTransactionResponse>> GetTransactionsAsync();
    Task<IEnumerable<InventoryTransactionResponse>> GetTransactionsByVariantAsync(int variantId);
}
