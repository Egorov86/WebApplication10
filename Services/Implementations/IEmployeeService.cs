using WebApplication10.Data.Entities;

namespace WebApplication10.Services
{
    public interface IEmployeeService
    {
        IList<Employee> GetEmployees();

        Employee? GetEmployeeById(int id);
    }
}
