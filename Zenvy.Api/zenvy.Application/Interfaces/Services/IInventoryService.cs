using zenvy.application.DTOs.Inventory;

namespace zenvy.application.Interfaces.Services;

public interface IInventoryService
{
    Task<int> CreateInventoryAsync(InventoryRequest request);
    Task<IEnumerable<InventoryResponse>> GetInventoryAsync();
    Task<InventoryResponse?> GetInventoryByIdAsync(int inventoryId);
    Task<bool> UpdateInventoryAsync(int inventoryId, InventoryRequest request);
    Task<bool> AdjustInventoryAsync(InventoryAdjustmentRequest request);
    Task<bool> DamageInventoryAsync(InventoryDamageRequest request);
    Task<bool> TransferInventoryAsync(InventoryTransferRequest request);
    Task<IEnumerable<InventoryTransactionResponse>> GetInventoryTransactionsAsync();
    Task<IEnumerable<InventoryTransactionResponse>> GetInventoryTransactionsByVariantAsync(int variantId);
}
