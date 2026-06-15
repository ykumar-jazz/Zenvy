using zenvy.application.DTOs.Inventory;
using zenvy.application.Interfaces.Repositories;
using zenvy.application.Interfaces.Services;

namespace zenvy.Application.Services;

public class InventoryService(IInventoryRepository inventoryRepository) : IInventoryService
{
    public Task<int> CreateInventoryAsync(InventoryRequest request)
    {
        return inventoryRepository.CreateAsync(request);
    }

    public Task<IEnumerable<InventoryResponse>> GetInventoryAsync()
    {
        return inventoryRepository.GetAllAsync();
    }

    public Task<InventoryResponse?> GetInventoryByIdAsync(int inventoryId)
    {
        return inventoryRepository.GetByIdAsync(inventoryId);
    }

    public Task<bool> UpdateInventoryAsync(int inventoryId, InventoryRequest request)
    {
        return inventoryRepository.UpdateAsync(inventoryId, request);
    }

    public Task<bool> AdjustInventoryAsync(InventoryAdjustmentRequest request)
    {
        return inventoryRepository.AdjustAsync(request);
    }

    public Task<bool> DamageInventoryAsync(InventoryDamageRequest request)
    {
        return inventoryRepository.DamageAsync(request);
    }

    public Task<bool> TransferInventoryAsync(InventoryTransferRequest request)
    {
        return inventoryRepository.TransferAsync(request);
    }

    public Task<IEnumerable<InventoryTransactionResponse>> GetInventoryTransactionsAsync()
    {
        return inventoryRepository.GetTransactionsAsync();
    }

    public Task<IEnumerable<InventoryTransactionResponse>> GetInventoryTransactionsByVariantAsync(int variantId)
    {
        return inventoryRepository.GetTransactionsByVariantAsync(variantId);
    }
}
