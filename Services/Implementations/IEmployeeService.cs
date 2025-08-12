using WebApplication10.Data.Entities;

namespace WebApplication10.Services
{
    public interface IEmployeeService
    {
        IList<Employee> GetEmployees();
        Employee? GetEmployeeById(int id);
        void DeleteEmployeeById(int id);
        void UpdateEmployee(Employee updatedEmployee);        
        void CreateEmployee(Employee employee);

        IList<Employee> GetEmployees(string? sortOrder = null);

    }
}
