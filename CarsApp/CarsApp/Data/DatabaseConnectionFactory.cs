using System.Data;
using Cars.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Cars.Data;

public class DatabaseConnectionFactory
{
    readonly DbSettings dbSettings;

    public DatabaseConnectionFactory(IOptions<DbSettings> dbSettings)
    {
        this.dbSettings = dbSettings.Value;
    }

    public IDbConnection GetConnection()
    {
        var connection = new SqlConnection(dbSettings.DefaultConnection); // Updated to use Microsoft.Data.SqlClient.SqlConnection  
        connection.Open();
        return connection;
    }
}

