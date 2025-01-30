using EmployeeManagementSystemPAU.Models;
using EmployeeManagementSystemPAU.Service.Emp;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystemPAU.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeRepository;

        public EmployeeController(IEmployeeService employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetAllEmployeesWithReviews();
            return View(employees); 
        }
        [HttpGet("search")]
        public async Task<IActionResult> Index(string searchTerm = "", int page = 1, int pageSize = 5)
        {
            var employees = await _employeeRepository.SearchEmployees(searchTerm, page, pageSize);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(await _employeeRepository.GetTotalEmployeeCount(searchTerm) / (double)pageSize);

            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _employeeRepository.GetAllDepartments();
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            var departments = await _employeeRepository.GetAllDepartments();
            ViewBag.Departments = departments;
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            var departments = await _employeeRepository.GetAllDepartments();
            ViewBag.Departments = departments;
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }

            var departments = await _employeeRepository.GetAllDepartments();
            ViewBag.Departments = departments;
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return PartialView("_EmployeeDetailsPartial", employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeRepository.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
