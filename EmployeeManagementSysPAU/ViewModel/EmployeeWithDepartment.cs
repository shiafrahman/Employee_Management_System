using EmployeeManagementSystemPAU.Models;

namespace EmployeeManagementSystemPAU.ViewModel
{
    public class EmployeeWithDepartment
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public DateTime JoinDate { get; set; }
        public string Status { get; set; }
        public string DepartmentName { get; set; }
    }

    public class EmployeeWithReviews
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string DepartmentName { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }

        // List of performance reviews for this employee
        public List<PerformanceReview> PerformanceReviews { get; set; }
    }
}
