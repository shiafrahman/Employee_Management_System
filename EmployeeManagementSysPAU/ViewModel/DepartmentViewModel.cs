namespace EmployeeManagementSysPAU.ViewModel
{
    public class DepartmentViewModel
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal Budget { get; set; }
        public int? ManagerID { get; set; }
        public string ManagerName { get; set; }
    }

    public class DepartmentPerformanceViewModel
    {
        public string DepartmentName { get; set; }
        public decimal AvgPerformanceScore { get; set; }
    }
}
