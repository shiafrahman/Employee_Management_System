namespace EmployeeManagementSystemPAU.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public DateTime JoinDate { get; set; }
        public int DepartmentID { get; set; }
        public string Status { get; set; }
        //public List<PerformanceReview> PerformanceReviews { get; set; }
    }
}
