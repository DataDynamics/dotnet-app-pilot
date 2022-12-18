using System.Numerics;
using log4net;
using log4net.Config;
using Npgsql;

namespace DataDynamics.App.Database;

public class CustomerSqlRepository
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(CustomerSqlRepository));

    private readonly string connectionString;

    public CustomerSqlRepository()
    {
        connectionString = "Host=localhost;Database=test;Username=postgres;Password=Clave:123";

        XmlConfigurator.Configure(new FileInfo("log4net.config"));
    }

    public void FindById(BigInteger id)
    {
        using var conn = new NpgsqlConnection(connectionString);
        using (var cmd = new NpgsqlCommand("select * from customers where id = @id", conn))
        {
            cmd.Parameters.AddWithValue("id", id);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) Console.Out.WriteLine(reader.GetString(1));
        }
    }

    public void FindAll()
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.EnableParameterLogging();
        using var dataSource = dataSourceBuilder.Build();

        var cmd = dataSource.CreateCommand("SELECT * from customers");
        var reader = cmd.ExecuteReader();
        while (reader.Read()) Console.Out.WriteLine(reader.GetValue(1));
    }

    public void Insert(long id, string fullName)
    {
        using var dataSource = NpgsqlDataSource.Create(connectionString);
        using (var cmd = dataSource.CreateCommand("INSERT INTO customers (id, full_name) VALUES ($1, $2)"))
        {
            cmd.Parameters.AddWithValue(100);
            cmd.Parameters.AddWithValue("Hello world");
            cmd.ExecuteNonQueryAsync();
        }
    }
}