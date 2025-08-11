using Microsoft.AspNetCore.Mvc;
using WebApplication10.Services.Implementations;
using WebApplication10.Data.Entities;
using WebApplication10.Services;

namespace WebApplication10.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        

        public IActionResult Index()
        {
            //IList<Employee> employees = _employeeService.GetEmployees();
            return View();
        }
        public IActionResult GetById([FromRoute] int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Employee? employee = _employeeService.GetEmployeeById((int)id);

            if (employee == null)
                return RedirectToAction("Index");

            return View(employee);
        }
    }
}
