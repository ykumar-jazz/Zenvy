using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using zenvy.application.DTOs.MarketplaceSettlements;
using zenvy.application.Interfaces.Repositories;

namespace zenvy.infrastructure.persistence.sqlserver.ado.net.repository;

public class MarketplaceSettlementRepository(IConfiguration configuration) : IMarketplaceSettlementRepository
{
    private readonly string sqlConnectionString = configuration?.GetConnectionString("DefaultConnection") ?? "";

    public async Task<long> CreateAsync(MarketplaceSettlementRequest request)
    {
        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_CreateMarketplaceSettlement", connection);
        command.Parameters.AddWithValue("@OrderId", request.OrderId);
        command.Parameters.AddWithValue("@ChannelId", request.ChannelId);
        command.Parameters.AddWithValue("@SaleAmount", request.SaleAmount);
        command.Parameters.AddWithValue("@MarketplaceCommission", request.MarketplaceCommission);
        command.Parameters.AddWithValue("@ShippingCharge", request.ShippingCharge);
        command.Parameters.AddWithValue("@TaxDeduction", request.TaxDeduction);
        command.Parameters.AddWithValue("@Status", request.Status);
        command.Parameters.AddWithValue("@SettlementDate", (object?)request.SettlementDate ?? DBNull.Value);

        return Convert.ToInt64(await command.ExecuteScalarAsync());
    }

    public async Task<IEnumerable<MarketplaceSettlementResponse>> GetAllAsync()
    {
        var settlements = new List<MarketplaceSettlementResponse>();

        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_GetMarketplaceSettlements", connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            settlements.Add(MapResponse(reader));
        }

        return settlements;
    }

    public async Task<MarketplaceSettlementResponse?> GetByIdAsync(long settlementId)
    {
        using var connection = new SqlConnection(sqlConnectionString);
        await connection.OpenAsync();

        using var command = CreateStoredProcedureCommand("usp_GetMarketplaceSettlementById", connection);
        command.Parameters.AddWithValue("@SettlementId", settlementId);

        using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;

        return MapResponse(reader);
    }

    private static SqlCommand CreateStoredProcedureCommand(string procedureName, SqlConnection connection)
    {
        return new SqlCommand(procedureName, connection) { CommandType = CommandType.StoredProcedure };
    }

    private static MarketplaceSettlementResponse MapResponse(SqlDataReader reader)
    {
        return new MarketplaceSettlementResponse
        {
            SettlementId = reader.GetInt64(reader.GetOrdinal("SettlementId")),
            OrderId = reader.GetInt64(reader.GetOrdinal("OrderId")),
            ExternalOrderId = reader.IsDBNull(reader.GetOrdinal("ExternalOrderId")) ? null : reader.GetString(reader.GetOrdinal("ExternalOrderId")),
            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
            OrderGrandTotal = reader.GetDecimal(reader.GetOrdinal("OrderGrandTotal")),
            ChannelId = reader.GetInt32(reader.GetOrdinal("ChannelId")),
            ChannelName = reader.GetString(reader.GetOrdinal("ChannelName")),
            CustomerName = reader.IsDBNull(reader.GetOrdinal("CustomerName")) ? null : reader.GetString(reader.GetOrdinal("CustomerName")),
            SaleAmount = reader.GetDecimal(reader.GetOrdinal("SaleAmount")),
            MarketplaceCommission = reader.GetDecimal(reader.GetOrdinal("MarketplaceCommission")),
            ShippingCharge = reader.GetDecimal(reader.GetOrdinal("ShippingCharge")),
            TaxDeduction = reader.GetDecimal(reader.GetOrdinal("TaxDeduction")),
            NetReceived = reader.GetDecimal(reader.GetOrdinal("NetReceived")),
            SettlementDate = reader.GetDateTime(reader.GetOrdinal("SettlementDate")),
            Status = reader.GetString(reader.GetOrdinal("Status"))
        };
    }
}
