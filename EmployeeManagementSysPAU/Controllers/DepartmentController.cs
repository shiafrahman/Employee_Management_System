using EmployeeManagementSysPAU.Service.Dept;
using EmployeeManagementSystemPAU.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSysPAU.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentRepository;
        public DepartmentController(IDepartmentService departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index()
        {
            var departments =await _departmentRepository.GetAllDepartmentsAsync();
            return View(departments);
        }

        public async Task<IActionResult> GetDepartmentPerformance()
        {
            var departmentPerformanceData = await _departmentRepository.GetDepartmentPerformanceAsync();
            return Json(departmentPerformanceData);
        }

        public async Task<IActionResult> Create()
        {
            var employees = await _departmentRepository.GetEmployeesByDepartmentIdAsync();
            ViewBag.Employee = employees;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.CreateDepartmentAsync(dept);
                return RedirectToAction(nameof(Index));
            }
            var employees = await _departmentRepository.GetEmployeesByDepartmentIdAsync();
            ViewBag.Employees = employees;
            return View(dept);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dept = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (dept == null)
            {
                return NotFound();
            }

            var employees = await _departmentRepository.GetEmployeesByDepartmentIdAsync();
            ViewBag.Employees = employees;
            return View(dept);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.UpdateDepartmentAsync(dept);
                return RedirectToAction(nameof(Index));
            }

            var employees = await _departmentRepository.GetEmployeesByDepartmentIdAsync();
            ViewBag.Employees = employees;
            return View(dept);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
