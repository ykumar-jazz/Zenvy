using zenvy.application.DTOs.Warehouses;

namespace zenvy.application.Interfaces.Repositories;

public interface IWarehouseRepository
{
    Task<int> CreateAsync(WarehouseRequest request);
    Task<IEnumerable<WarehouseResponse>> GetAllAsync();
    Task<WarehouseResponse?> GetByIdAsync(int warehouseId);
    Task<bool> UpdateAsync(int warehouseId, WarehouseRequest request);
    Task<bool> DeleteAsync(int warehouseId);
    Task<IEnumerable<WarehouseDropdownResponse>> GetDropdownAsync();
}
