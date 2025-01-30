namespace EmployeeManagementSystemPAU.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal Budget { get; set; }
        public int? ManagerID { get; set; }
    }
}
