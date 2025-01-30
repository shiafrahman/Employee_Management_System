using Dapper;
using EmployeeManagementSysPAU.ViewModel;
using EmployeeManagementSystemPAU.DB;
using EmployeeManagementSystemPAU.Models;
using EmployeeManagementSystemPAU.ViewModel;

namespace EmployeeManagementSysPAU.Service.Dept
{
    public class DepartmentService: IDepartmentService
    {
        private readonly DapperContext _dbConnection;
        public DepartmentService(DapperContext dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartmentsAsync()
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                var query = @"
                    SELECT 
                        d.DepartmentID,
                        d.DepartmentName,
                        d.Budget,
                        e.Name AS ManagerName
                    FROM Department d
                    LEFT JOIN Employee e ON e.EmployeeID = d.ManagerID
                    WHERE d.ManagerID IS NOT NULL
                    AND e.Status = 'active'
                    AND (e.IsDeleted = 0 OR e.IsDeleted IS NULL)";
                return await connection.QueryAsync<DepartmentViewModel>(query);
            }
           
        }
        

        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                var query = "SELECT * FROM Department WHERE DepartmentID = @DepartmentID";
                return await connection.QuerySingleOrDefaultAsync<Department>(query, new { DepartmentID = departmentId });
            }


            
            
        }

        public async Task<int> CreateDepartmentAsync(Department department)
        {

            using (var connection = _dbConnection.CreateConnection())
            {
                var query = @"
                                INSERT INTO Department (DepartmentName, Budget, ManagerID)
                                VALUES (@DepartmentName, @Budget, @ManagerID);
                                SELECT CAST(SCOPE_IDENTITY() AS INT);";
                return await connection.ExecuteAsync(query, department);
            }
            
        }

        public async Task<int> UpdateDepartmentAsync(Department department)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                var query = @"
                            UPDATE Department
                            SET DepartmentName = @DepartmentName, Budget = @Budget, ManagerID = @ManagerID
                            WHERE DepartmentID = @DepartmentID";
                return await connection.ExecuteAsync(query, department);
            }


            
        }

        public async Task<int> DeleteDepartmentAsync(int departmentId)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                var query = "DELETE FROM Department WHERE DepartmentID = @DepartmentID";
                return await connection.ExecuteAsync(query, new { DepartmentID = departmentId });
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync()
        {

            using (var connection = _dbConnection.CreateConnection())
            {
                var query = "SELECT EmployeeID,Name FROM Employee where Status='active'";
                return await connection.QueryAsync<Employee>(query);
            }
           
            
        }

        public async Task<IEnumerable<DepartmentPerformanceViewModel>> GetDepartmentPerformanceAsync()
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                var query = "SELECT DepartmentName, AvgPerformanceScore FROM DepartmentPerformance";
                return await connection.QueryAsync<DepartmentPerformanceViewModel>(query);
            }
           
           
        }

    }
}
