using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using zenvy.application.DTOs.Brands;
using zenvy.application.Interfaces.Repositories;

namespace zenvy.infrastructure.Persistence.SqlServer.ADO;

public class BrandRepository(IConfiguration configuration) : IBrandRepository
{
    private readonly string sqlConnectionString = configuration?.GetConnectionString("DefaultConnection") ?? "";

    public async Task<int> CreateAsync(BrandRequest request)
    {
        try
        {
            using var connection = new SqlConnection(sqlConnectionString);

            await connection.OpenAsync();

            using var command =
                new SqlCommand(
                    "insert into Brands (Name, Description,Status) values (@name, @desc,@status); SELECT SCOPE_IDENTITY();",
                    connection);

            command.Parameters.AddWithValue("@name", request.Name);
            command.Parameters.AddWithValue("@desc",
                (object?)request.Description ?? DBNull.Value);        
            command.Parameters.AddWithValue("@status", request.Status);

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"An error occurred while creating the Brands: {ex.Message}");
            // Optionally, you can rethrow the exception or return a specific value to indicate failure
            throw;
        }                       
    }

    public async Task<bool> DeleteAsync(int brandId)
    {
        try
        {
            using var connection = new SqlConnection(sqlConnectionString);

            connection.Open();

            using var command =
                new SqlCommand("DELETE FROM Brands WHERE BrandId = @id", connection);

            command.Parameters.AddWithValue("@id", brandId);

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"An error occurred while deleting the Brands: {ex.Message}");
            // Optionally, you can rethrow the exception or return a specific value to indicate failure
            throw;
        }
    }

    public Task<IEnumerable<BrandResponse>> GetAllAsync()
    {
        try
        {
            var brands = new List<BrandResponse>();

            using var connection = new SqlConnection(sqlConnectionString);

            connection.Open();

            using var command = new SqlCommand("SELECT BrandId, Name, Description, Status FROM Brands", connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                brands.Add(new BrandResponse
                {
                    BrandId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Status = reader.GetBoolean(3)
                });
            }

            return Task.FromResult(brands.AsEnumerable());
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"An error occurred while retrieving categories: {ex.Message}");
            // Optionally, you can rethrow the exception or return an empty list to indicate failure
            throw;
        }
    
    }

    public Task<BrandResponse?> GetByIdAsync(int brandId)
    {
       try
        {
            using var connection = new SqlConnection(sqlConnectionString);

            connection.Open();

            using var command = new SqlCommand("SELECT BrandId, Name, Description, Status FROM Brands WHERE BrandId= @id", connection);

            command.Parameters.AddWithValue("@Id", brandId);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var Brands = new BrandResponse
                {
                    BrandId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Status = reader.GetBoolean(3)
                };

                return Task.FromResult<BrandResponse?>(Brands);
            }

            return Task.FromResult<BrandResponse?>(null);
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"An error occurred while retrieving the Brands: {ex.Message}");
            // Optionally, you can rethrow the exception or return null to indicate failure
            throw;
        }
  
    }

    public Task<bool> UpdateAsync(int brandId, UpdateBrandRequest request)
    {
        try
        {
            using var connection = new SqlConnection(sqlConnectionString);

            connection.Open();

            using var command =
                new SqlCommand("UPDATE Brands SET Name = @name, Description = @description WHERE BrandId = @id", connection);
          
            command.Parameters.AddWithValue("@name", request.Name);
            command.Parameters.AddWithValue("@description",
                (object?)request.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@status", request.Status);
            command.Parameters.AddWithValue("@id", brandId);

            int rowsAffected = command.ExecuteNonQuery();

            return Task.FromResult(rowsAffected > 0);
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"An error occurred while updating the Brands: {ex.Message}");
            // Optionally, you can rethrow the exception or return a specific value to indicate failure
            throw;
        }
    
    }
}