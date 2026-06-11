using DataAccessLayer.Core;
using DataAccessLayer.Interfaces;
using DataAccessLayer.SqlServer;
using Microsoft.Extensions.Configuration;

namespace zenvy.shared;

public class DataAccessSetup
{
    protected IDataAccess DataAccess;
    protected IDapperDataAccessHelper DapperDataAccessHelper;

    // Lazy initialization of configuration to ensure it's only loaded once and reused
    private static readonly Lazy<IConfiguration> Configuration = new(() =>
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        string basePath = AppContext.BaseDirectory;
        if (!File.Exists(Path.Combine(basePath, "appsettings.json")))
        {
            basePath = Directory.GetParent(basePath)?.Parent?.Parent?.FullName ?? basePath;
        }

        return new ConfigurationBuilder().SetBasePath(basePath)
                                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                                        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                                        .Build();
    });

    // Lazy initialization of the connection string, ensuring it's only fetched once
    private static readonly Lazy<string> ConnectionString = new Lazy<string>(() =>
    {
        //var connectionString = Configuration.Value.GetValue<string>("ConnectionStrings:JazTravelConnection");
        var connectionString = Configuration.Value.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("Connection string for DefaultConnection not found in configuration.");
        }
        return connectionString;
    });

    protected DataAccessSetup()
    {

        DapperDataAccessHelper = new DapperDataAccessHelper(ConnectionString.Value);


        var parameterFactory = new SqlParameterFactory();
        DataAccess = new DataAccess(ConnectionString.Value)
        {
            ParameterFactory = parameterFactory
        };

    }
}


