using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using zenvy.Application.DTOs.Category;
using zenvy.Application.Interfaces.Repositories;

namespace zenvy.Infrastructure.Persistence.SQLServer.ADO.net.Repository;

public class CategoryRepository(IConfiguration configuration) : ICategoryRepository
{
    private readonly string sqlConnectionString = configuration?.GetConnectionString("DefaultConnection") ?? "";

    public async Task<long> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            using var connection = new SqlConnection(sqlConnectionString);

            await connection.OpenAsync();

            using var command =
                new SqlCommand(
                    "insert into Categories (Name, Description) values (@name, @desc); SELECT SCOPE_IDENTITY();",
                    connection);

            command.Parameters.AddWithValue("@name", request.CategoryName);
            command.Parameters.AddWithValue("@desc",
                (object?)request.Description ?? DBNull.Value);
            //command.Parameters.AddWithValue("@createdBy", );

            return Convert.ToInt64(
                await command.ExecuteScalarAsync());
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"An error occurred while creating the category: {ex.Message}");
            // Optionally, you can rethrow the exception or return a specific value to indicate failure
            throw;
        }
    }

    public Task<bool> DeleteAsync(long categoryId)
    {
        try
        {
            using var connection = new SqlConnection(sqlConnectionString);

            connection.Open();

            using var command =
                new SqlCommand("DELETE FROM Categories WHERE CategoryId = @id", connection);

            command.Parameters.AddWithValue("@id", categoryId);

            int rowsAffected = command.ExecuteNonQuery();

            return Task.FromResult(rowsAffected > 0);
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"An error occurred while deleting the category: {ex.Message}");
            // Optionally, you can rethrow the exception or return a specific value to indicate failure
            throw;
        }
    }

    public Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {
        //throw new NotImplementedException();
        try
        {
            var categories = new List<CategoryResponse>();

            using var connection = new SqlConnection(sqlConnectionString);

            connection.Open();

            using var command = new SqlCommand("SELECT CategoryId, Name, Description, Status FROM Categories", connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                categories.Add(new CategoryResponse
                {
                    CategoryId = reader.GetInt32(0),
                    CategoryName = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    IsActive = reader.GetBoolean(3)
                });
            }

            return Task.FromResult(categories.AsEnumerable());
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"An error occurred while retrieving categories: {ex.Message}");
            // Optionally, you can rethrow the exception or return an empty list to indicate failure
            throw;
        }
    }

    public Task<CategoryResponse?> GetByIdAsync(long categoryId)
    {
        try
        {
            using var connection = new SqlConnection(sqlConnectionString);

            connection.Open();

            using var command = new SqlCommand("SELECT CategoryId, Name, Description, Status FROM Categories WHERE CategoryId = @id", connection);

            command.Parameters.AddWithValue("@Id", categoryId);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var category = new CategoryResponse
                {
                    CategoryId = reader.GetInt32(0),
                    CategoryName = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    IsActive = reader.GetBoolean(3)
                };

                return Task.FromResult<CategoryResponse?>(category);
            }

            return Task.FromResult<CategoryResponse?>(null);
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"An error occurred while retrieving the category: {ex.Message}");
            // Optionally, you can rethrow the exception or return null to indicate failure
            throw;
        }
    }

    public Task<bool> UpdateAsync(UpdateCategoryRequest request)
    {
       try
        {
            using var connection = new SqlConnection(sqlConnectionString);

            connection.Open();

            using var command =
                new SqlCommand("UPDATE Categories SET Name = @name, Description = @description WHERE CategoryId = @id", connection);

            command.Parameters.AddWithValue("@id", request.CategoryId);
            command.Parameters.AddWithValue("@name", request.CategoryName);
            command.Parameters.AddWithValue("@description",
                (object?)request.Description ?? DBNull.Value);

            int rowsAffected = command.ExecuteNonQuery();

            return Task.FromResult(rowsAffected > 0);
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            Console.WriteLine($"An error occurred while updating the category: {ex.Message}");
            // Optionally, you can rethrow the exception or return a specific value to indicate failure
            throw;
        }
    }
}