using EmployeeManagementSystemPAU.Models;
using EmployeeManagementSystemPAU.ViewModel;

namespace EmployeeManagementSystemPAU.Service.Emp
{
    public interface IEmployeeService
    {
        //Task<IEnumerable<EmployeeWithDepartment>> GetAllEmployees();
        Task<IEnumerable<EmployeeWithReviews>> GetAllEmployeesWithReviews();
        Task<Employee> GetEmployeeById(int id);
        Task<int> AddEmployee(Employee employee);
        Task<int> UpdateEmployee(Employee employee);
        Task<int> DeleteEmployee(int id);
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<int> GetTotalEmployeeCount(string searchTerm);
        Task<IEnumerable<EmployeeWithReviews>> SearchEmployees(string searchTerm, int page, int pageSize);
    }
}
