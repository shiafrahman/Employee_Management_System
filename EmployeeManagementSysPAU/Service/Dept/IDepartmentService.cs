using EmployeeManagementSysPAU.ViewModel;
using EmployeeManagementSystemPAU.Models;

namespace EmployeeManagementSysPAU.Service.Dept
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentViewModel>> GetAllDepartmentsAsync();          
        Task<Department> GetDepartmentByIdAsync(int departmentId);      
        Task<int> CreateDepartmentAsync(Department department);         
        Task<int> UpdateDepartmentAsync(Department department);              
        Task<int> DeleteDepartmentAsync(int departmentId);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync();
        Task<IEnumerable<DepartmentPerformanceViewModel>> GetDepartmentPerformanceAsync();
    }
}
