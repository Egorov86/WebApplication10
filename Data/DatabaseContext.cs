using Microsoft.Data.SqlClient;

namespace WebApplication10.Data
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Default");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new MissingFieldException("Faield to get Default connection string");
            _connectionString = connectionString;
        }
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
