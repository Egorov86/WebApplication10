using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication10.Data;
using WebApplication10.Data.Entities;

namespace WebApplication10.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DatabaseContext _database;

        public EmployeeService(DatabaseContext database)
        {
            _database = database;
        }

        private Employee ReadEmployee(SqlDataReader reader)
        {
            return new Employee()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            };
        }

        public IList<Employee> GetEmployees()
        {
            IList<Employee> employees = new List<Employee>();

            using (SqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Employees";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return employees;

                    while (reader.Read())
                    {
                        Employee employee = ReadEmployee(reader);
                        employees.Add(employee);
                    }
                }
            }

            return employees;
        }

        public Employee? GetEmployeeById(int id)
        {
            using (SqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT TOP 1 * FROM Employees WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return null;

                    reader.Read();

                    return ReadEmployee(reader);
                }
            }
        }

        public void DeleteEmployeeById(int id)
        {
            using (SqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Employees WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}