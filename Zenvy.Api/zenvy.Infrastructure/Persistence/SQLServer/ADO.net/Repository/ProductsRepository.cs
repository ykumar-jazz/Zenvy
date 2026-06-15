using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using zenvy.application.DTOs.Products;
using zenvy.application.Interfaces.Repositories;
using zenvy.domain.Entities;

namespace zenvy.infrastructure.persistence.sqlserver.ado.net.repository.products;

public class ProductsRepository(IConfiguration configuration) : IProductsRepository
{
    private readonly string sqlConnectionString = configuration?.GetConnectionString("DefaultConnection") ?? "";

    public async Task<int> CreateAsync(CreateProductRequest request)
    {
        try
        {
            using var connection = new SqlConnection(sqlConnectionString);
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();
            try
            {
                using var command = new SqlCommand("usp_CreateProduct", connection, transaction);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductCode", request.ProductMaster.ProductCode);
                command.Parameters.AddWithValue("@ProductName", request.ProductMaster.ProductName);
                command.Parameters.AddWithValue("@CategoryId", request.ProductMaster.CategoryId);
                command.Parameters.AddWithValue("@BrandId", (object?)request.ProductMaster.BrandId ?? DBNull.Value);
                command.Parameters.AddWithValue("@Description", (object?)request.ProductMaster.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@HSNCode", (object?)request.ProductMaster.HSNCode ?? DBNull.Value);
                command.Parameters.AddWithValue("@GSTPercentage", request.ProductMaster.GSTPercentage);
                command.Parameters.AddWithValue("@CreatedBy", "1"); // Changed to string since SP expects VARCHAR(50)

                // Pass the incoming standard scalar cost/selling pricing for the main Product entity
                //command.Parameters.AddWithValue("@CostPrice", request.Product.CostPrice);
                //command.Parameters.AddWithValue("@SellingPrice", request.Product.SellingPrice);
                //command.Parameters.AddWithValue("@Quantity", request.Product.Quantity);

                // Structured Parameter: Variants
                var variantParam = command.Parameters.AddWithValue("@Variants", CreateVariantTable(request.ProductVariants));
                variantParam.SqlDbType = SqlDbType.Structured;
                variantParam.TypeName = "dbo.ProductVariantType";

                // Structured Parameter: Images
                var imageParam = command.Parameters.AddWithValue("@Images", CreateImageTable(request.ProductImages));
                imageParam.SqlDbType = SqlDbType.Structured;
                imageParam.TypeName = "dbo.ProductImageType";

                // CRITICAL FIX: Stored Procedure returns a result set (2 columns, 1 row), NOT a single scalar value.
                using var reader = await command.ExecuteReaderAsync();
                int productMasterId = 0;
                if (await reader.ReadAsync())
                {
                    productMasterId = Convert.ToInt32(reader["ProductMasterId"]);
                }
                
                reader.Close(); // Explicitly close reader before committing transaction
                transaction.Commit();
                return productMasterId;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating the Product: {ex.Message}");
            throw;
        }
    }

    private static DataTable CreateVariantTable(IEnumerable<ProductVariants> variants)
    {
        var table = new DataTable();

        // Must match the DB type column order exactly:
        table.Columns.Add("SKU", typeof(string));
        table.Columns.Add("Barcode", typeof(string));
        table.Columns.Add("Size", typeof(string));
        table.Columns.Add("Color", typeof(string));
        table.Columns.Add("Material", typeof(string));
        table.Columns.Add("Gender", typeof(string));
        table.Columns.Add("Season", typeof(string));
        table.Columns.Add("CurrentPrice", typeof(decimal));
        table.Columns.Add("Status", typeof(bool)); 

        foreach (var item in variants)
        {
            table.Rows.Add(
                item.SKU ?? (object)DBNull.Value,
                item.Barcode ?? (object)DBNull.Value,
                item.Size ?? (object)DBNull.Value,
                item.Color ?? (object)DBNull.Value,
                item.Material ?? (object)DBNull.Value,
                item.Gender ?? (object)DBNull.Value,
                item.Season ?? (object)DBNull.Value,
                item.CurrentPrice,
                item.Status
            );
        }

        return table;
    }

    private static DataTable CreateImageTable(IEnumerable<ProductImages> images)
    {
        var table = new DataTable();

        table.Columns.Add("ImageUrl", typeof(string));
        table.Columns.Add("IsPrimary", typeof(bool));

        foreach (var item in images)
        {
            table.Rows.Add(
                item.ImageUrl ?? (object)DBNull.Value,
                item.IsPrimary
            );
        }

        return table;
    }
}