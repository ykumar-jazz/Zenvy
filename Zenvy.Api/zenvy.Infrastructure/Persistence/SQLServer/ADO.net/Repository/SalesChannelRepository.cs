using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using zenvy.application.DTOs.SalesChannels;
using zenvy.application.Interfaces.Repositories;

namespace zenvy.infrastructure.persistence.sqlserver.ado.net.repository;

public class SalesChannelRepository(IConfiguration configuration) : ISalesChannelRepository
{
    private readonly string sqlConnectionString = configuration?.GetConnectionString("DefaultConnection") ?? "";

    public async Task<IEnumerable<SalesChannelResponse>> GetAllAsync()
    {
        var channels = new List<SalesChannelResponse>();

        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_GetSalesChannels", connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            channels.Add(MapResponse(reader));
        }

        return channels;
    }

    public async Task<SalesChannelResponse?> GetByIdAsync(int channelId)
    {
        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_GetSalesChannelById", connection);
        command.Parameters.AddWithValue("@ChannelId", channelId);

        using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;

        return MapResponse(reader);
    }

    public async Task<int> CreateAsync(SalesChannelRequest request)
    {
        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_CreateSalesChannel", connection);
        command.Parameters.AddWithValue("@ChannelName", request.ChannelName);
        command.Parameters.AddWithValue("@ChannelType", (object?)request.ChannelType ?? DBNull.Value);
        command.Parameters.AddWithValue("@Status", request.Status == 1);

        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    public async Task<bool> UpdateAsync(int channelId, SalesChannelRequest request)
    {
        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_UpdateSalesChannel", connection);
        command.Parameters.AddWithValue("@ChannelId", channelId);
        command.Parameters.AddWithValue("@ChannelName", request.ChannelName);
        command.Parameters.AddWithValue("@ChannelType", (object?)request.ChannelType ?? DBNull.Value);
        command.Parameters.AddWithValue("@Status", request.Status == 1);

        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int channelId)
    {
        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_DeleteSalesChannel", connection);
        command.Parameters.AddWithValue("@ChannelId", channelId);

        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    private static SalesChannelResponse MapResponse(SqlDataReader reader)
    {
        return new SalesChannelResponse
        {
            ChannelId = reader.GetInt32(reader.GetOrdinal("ChannelId")),
            ChannelName = reader.GetString(reader.GetOrdinal("ChannelName")),
            ChannelType = reader.IsDBNull(reader.GetOrdinal("ChannelType")) ? null : reader.GetString(reader.GetOrdinal("ChannelType")),
            Status = reader.GetBoolean(reader.GetOrdinal("Status")) ? 1 : 0
        };
    }

    private static SqlCommand CreateStoredProcedureCommand(string procedureName, SqlConnection connection)
    {
        return new SqlCommand(procedureName, connection) { CommandType = CommandType.StoredProcedure };
    }
}
