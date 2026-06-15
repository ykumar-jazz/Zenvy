using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using zenvy.application.DTOs.Warehouses;
using zenvy.application.Interfaces.Repositories;

namespace zenvy.infrastructure.persistence.sqlserver.ado.net.repository;

public class WarehouseRepository(IConfiguration configuration) : IWarehouseRepository
{
    private readonly string sqlConnectionString = configuration?.GetConnectionString("DefaultConnection") ?? "";

    public async Task<int> CreateAsync(WarehouseRequest request)
    {
        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_CreateWarehouse", connection);
        command.Parameters.AddWithValue("@WarehouseName", request.WarehouseName);
        command.Parameters.AddWithValue("@Location", (object?)request.Location ?? DBNull.Value);
        command.Parameters.AddWithValue("@Status", request.Status);

        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    public async Task<IEnumerable<WarehouseResponse>> GetAllAsync()
    {
        var warehouses = new List<WarehouseResponse>();

        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_GetWarehouses", connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            warehouses.Add(MapWarehouse(reader));
        }

        return warehouses;
    }

    public async Task<WarehouseResponse?> GetByIdAsync(int warehouseId)
    {
        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_GetWarehouseById", connection);
        command.Parameters.AddWithValue("@WarehouseId", warehouseId);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapWarehouse(reader);
        }

        return null;
    }

    public async Task<bool> UpdateAsync(int warehouseId, WarehouseRequest request)
    {
        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_UpdateWarehouse", connection);
        command.Parameters.AddWithValue("@WarehouseId", warehouseId);
        command.Parameters.AddWithValue("@WarehouseName", request.WarehouseName);
        command.Parameters.AddWithValue("@Location", (object?)request.Location ?? DBNull.Value);
        command.Parameters.AddWithValue("@Status", request.Status);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int warehouseId)
    {
        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_DeleteWarehouse", connection);
        command.Parameters.AddWithValue("@WarehouseId", warehouseId);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<IEnumerable<WarehouseDropdownResponse>> GetDropdownAsync()
    {
        var warehouses = new List<WarehouseDropdownResponse>();

        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_GetWarehouseDropdown", connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            warehouses.Add(new WarehouseDropdownResponse
            {
                WarehouseId = reader.GetInt32(reader.GetOrdinal("WarehouseId")),
                WarehouseName = reader.GetString(reader.GetOrdinal("WarehouseName"))
            });
        }

        return warehouses;
    }

    private static SqlCommand CreateStoredProcedureCommand(string procedureName, SqlConnection connection)
    {
        return new SqlCommand(procedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
    }

    private static WarehouseResponse MapWarehouse(SqlDataReader reader)
    {
        return new WarehouseResponse
        {
            WarehouseId = reader.GetInt32(reader.GetOrdinal("WarehouseId")),
            WarehouseName = reader.GetString(reader.GetOrdinal("WarehouseName")),
            Location = reader.IsDBNull(reader.GetOrdinal("Location")) ? null : reader.GetString(reader.GetOrdinal("Location")),
            Status = reader.GetBoolean(reader.GetOrdinal("Status"))
        };
    }
}
