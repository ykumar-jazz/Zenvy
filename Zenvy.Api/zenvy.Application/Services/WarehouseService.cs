using zenvy.application.DTOs.Warehouses;
using zenvy.application.Interfaces.Repositories;
using zenvy.application.Interfaces.Services;

namespace zenvy.Application.Services;

public class WarehouseService(IWarehouseRepository warehouseRepository) : IWarehouseService
{
    public Task<int> CreateWarehouseAsync(WarehouseRequest request)
    {
        return warehouseRepository.CreateAsync(request);
    }

    public Task<IEnumerable<WarehouseResponse>> GetWarehousesAsync()
    {
        return warehouseRepository.GetAllAsync();
    }

    public Task<WarehouseResponse?> GetWarehouseByIdAsync(int warehouseId)
    {
        return warehouseRepository.GetByIdAsync(warehouseId);
    }

    public Task<bool> UpdateWarehouseAsync(int warehouseId, WarehouseRequest request)
    {
        return warehouseRepository.UpdateAsync(warehouseId, request);
    }

    public Task<bool> DeleteWarehouseAsync(int warehouseId)
    {
        return warehouseRepository.DeleteAsync(warehouseId);
    }

    public Task<IEnumerable<WarehouseDropdownResponse>> GetWarehouseDropdownAsync()
    {
        return warehouseRepository.GetDropdownAsync();
    }
}
