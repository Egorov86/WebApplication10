using Microsoft.AspNetCore.Mvc;
using WebApplication10.Data.Entities;
using WebApplication10.Services;
using WebApplication10.Services.Implementations;

namespace WebApplication10.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult AddNewEmployee()
        {
            return View(); // Вернёт представление AddNewEmployee.cshtml
        }
        [HttpPost]
        public IActionResult SaveNewEmployee(Employee newEmployee)
        {
            try
            {
                _employeeService.CreateEmployee(newEmployee);
                return RedirectToAction(nameof(Index)); // После успешного создания вернёмся на страницу списка работников
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(nameof(AddNewEmployee), newEmployee); // В случае ошибки вернёмся на форму создания
            }
        }
        public IActionResult Index()
        {
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

        public IActionResult DeleteEmployee([FromRoute] int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            _employeeService.DeleteEmployeeById((int)id);

            return RedirectToAction("Index");
        }
        public IActionResult UpdateEmployee([FromRoute] int id, [FromBody] Employee updatedEmployee)
        {
            try
            {
                var existingEmployee = _employeeService.GetEmployeeById(id);

                if (existingEmployee == null)
                {
                    return NotFound(); // работник не найден
                }

                // Обновляем данные
                updatedEmployee.Id = id; // Убедимся, что ид совпадает
                _employeeService.UpdateEmployee(updatedEmployee);

                return Ok(new { message = $"Данные работника успешно обновлены." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult EditEmployee(int id, Employee updatedEmployee)
        {
            try
            {
                var existingEmployee = _employeeService.GetEmployeeById(id);

                if (existingEmployee == null)
                {
                    return NotFound(); // Сотрудник не найден
                }

                // Обновляем данные работника
                updatedEmployee.Id = id;
                _employeeService.UpdateEmployee(updatedEmployee);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(nameof(GetById), updatedEmployee); // Возвращаемся на страницу редактирования с сообщением об ошибке
            }
        }
        [HttpGet]
        public IActionResult Index(string? sortOrder = null)
        {
            IList<Employee> employees = _employeeService.GetEmployees();

            // Сортируем сотрудников на основе параметра сортировки
            if (sortOrder == "lastname_asc")
            {
                employees = employees.OrderBy(e => e.LastName).ToList();
            }
            else if (sortOrder == "lastname_desc")
            {
                employees = employees.OrderByDescending(e => e.LastName).ToList();
            }

            // Передаем сотрудников в представление
            ViewData["SortOrder"] = sortOrder;
            return View(employees);
        }

    }
}
