using zenvy.application.DTOs.Warehouses;

namespace zenvy.application.Interfaces.Services;

public interface IWarehouseService
{
    Task<int> CreateWarehouseAsync(WarehouseRequest request);
    Task<IEnumerable<WarehouseResponse>> GetWarehousesAsync();
    Task<WarehouseResponse?> GetWarehouseByIdAsync(int warehouseId);
    Task<bool> UpdateWarehouseAsync(int warehouseId, WarehouseRequest request);
    Task<bool> DeleteWarehouseAsync(int warehouseId);
    Task<IEnumerable<WarehouseDropdownResponse>> GetWarehouseDropdownAsync();
}
