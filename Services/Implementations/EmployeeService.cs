
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
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                MiddleName = reader.GetString(reader.GetOrdinal("MiddleName")),
                Education = reader.GetString(reader.GetOrdinal("Education")),
                Profession = reader.GetString(reader.GetOrdinal("Profession")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                WorkExperience = reader.GetInt32(reader.GetOrdinal("WorkExperience")),
                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                Address = reader.GetString(reader.GetOrdinal("Address")),
                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                YearsOld = reader.GetInt32(reader.GetOrdinal("YearsOld")),
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
        public void UpdateEmployee(Employee updatedEmployee)
        {
            using (SqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                // Создаем команду SQL для обновления записи
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"UPDATE Employees SET 
                                LastName=@LastName,
                                FirstName=@FirstName,
                                MiddleName=@MiddleName,
                                Education=@Education,
                                Profession=@Profession,
                                WorkExperience=@WorkExperience,
                                DateOfBirth=@DateOfBirth,
                                Address=@Address,                               
                                PhoneNumber=@PhoneNumber,                               
                                Email=@Email WHERE Id=@Id;";

                // Добавляем параметры команды
                command.Parameters.AddWithValue("@LastName", updatedEmployee.LastName);
                command.Parameters.AddWithValue("@FirstName", updatedEmployee.FirstName);
                command.Parameters.AddWithValue("@MiddleName", updatedEmployee.MiddleName);
                command.Parameters.AddWithValue("@Education", updatedEmployee.Education);
                command.Parameters.AddWithValue("@Profession", updatedEmployee.Profession);
                command.Parameters.AddWithValue("@Email", updatedEmployee.Email);
                command.Parameters.AddWithValue("@WorkExperience", updatedEmployee.WorkExperience);
                command.Parameters.AddWithValue("@DateOfBirth", updatedEmployee.DateOfBirth);
                command.Parameters.AddWithValue("@Address", updatedEmployee.Address);
                command.Parameters.AddWithValue("@PhoneNumber", updatedEmployee.PhoneNumber);                
                command.Parameters.AddWithValue("@Id", updatedEmployee.Id);

                // Выполняем обновление
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected != 1)
                {
                    throw new InvalidOperationException($"Ошибка обновления сотрудника с ID {updatedEmployee.Id}.");
                }
            }
        }
        public void CreateEmployee(Employee newEmployee)
        {
            using (SqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO Employees(LastName, FirstName, MiddleName, Education, Profession, Email, WorkExperience, DateOfBirth, Address, PhoneNumber, CreatedAt) 
                                     VALUES(@LastName, @FirstName, @MiddleName, @Education, @Profession, @Email, @WorkExperience, @DateOfBirth, @Address, @PhoneNumber, @CreatedAt)";

                command.Parameters.AddWithValue("@LastName", newEmployee.LastName);
                command.Parameters.AddWithValue("@FirstName", newEmployee.FirstName);
                command.Parameters.AddWithValue("@MiddleName", newEmployee.MiddleName);
                command.Parameters.AddWithValue("@Education", newEmployee.Education);
                command.Parameters.AddWithValue("@Profession", newEmployee.Profession);
                command.Parameters.AddWithValue("@Email", newEmployee.Email);
                command.Parameters.AddWithValue("@WorkExperience", newEmployee.WorkExperience);
                command.Parameters.AddWithValue("@DateOfBirth", newEmployee.DateOfBirth);
                command.Parameters.AddWithValue("@Address", newEmployee.Address);
                command.Parameters.AddWithValue("@PhoneNumber", newEmployee.PhoneNumber);                
                command.Parameters.AddWithValue("@CreatedAt", newEmployee.CreatedAt);

                command.ExecuteNonQuery();
            }
        }
        public IList<Employee> GetEmployees(string? sortOrder = null)
        {
            var employees = new List<Employee>();
            //    = _repository.GetAll(); // Предположим, что получаем данные из репозитория
            //var existingEmployee = _employeeService.GetEmployeeById(id);

            switch (sortOrder)
            {
                case "lastname_desc":
                    return employees.OrderByDescending(e => e.LastName).ToList();
                default:
                    return employees.OrderBy(e => e.LastName).ToList();
            }
        }
        
    }
}
