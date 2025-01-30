using Dapper;
using EmployeeManagementSystemPAU.DB;
using EmployeeManagementSystemPAU.Models;
using EmployeeManagementSystemPAU.ViewModel;

namespace EmployeeManagementSystemPAU.Service.Emp
{
    public class EmployeeService:IEmployeeService
    {
        private readonly DapperContext _context;
        public EmployeeService(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EmployeeWithReviews>> GetAllEmployeesWithReviews()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"
            SELECT e.EmployeeID, e.Name, e.Email, e.Position,e.Phone,e.JoinDate,e.Status, d.DepartmentName,
                   pr.ReviewID, pr.ReviewDate, pr.ReviewScore, pr.ReviewNotes
            FROM Employee e
            LEFT JOIN Department d ON e.DepartmentID = d.DepartmentID
            LEFT JOIN PerformanceReview pr ON e.EmployeeID = pr.EmployeeID
            WHERE e.IsDeleted = 0
            ORDER BY e.EmployeeID, pr.ReviewDate DESC";

                var employeeDict = new Dictionary<int, EmployeeWithReviews>();

                var employees = await connection.QueryAsync<EmployeeWithReviews, PerformanceReview, EmployeeWithReviews>(
                    query,
                    (employee, review) =>
                    {
                        if (!employeeDict.TryGetValue(employee.EmployeeID, out var empEntry))
                        {
                            empEntry = employee;
                            empEntry.PerformanceReviews = new List<PerformanceReview>();
                            employeeDict.Add(empEntry.EmployeeID, empEntry);
                        }

                        if (review != null)
                        {
                            empEntry.PerformanceReviews.Add(review);
                        }

                        return empEntry;
                    },
                    splitOn: "ReviewID"
                );

                return employeeDict.Values;
            }
        }


        public async Task<IEnumerable<EmployeeWithReviews>> SearchEmployees(string searchTerm, int page, int pageSize)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"
            SELECT e.EmployeeID, e.Name, e.Email, e.Position,e.Phone,e.JoinDate,e.Status, d.DepartmentName,
                   pr.ReviewID, pr.ReviewDate, pr.ReviewScore, pr.ReviewNotes
            FROM Employee e
            LEFT JOIN Department d ON e.DepartmentID = d.DepartmentID
            LEFT JOIN PerformanceReview pr ON e.EmployeeID = pr.EmployeeID
            WHERE e.IsDeleted = 0
            
                AND (@SearchTerm IS NULL OR e.Name LIKE '%' + @SearchTerm + '%')
            ORDER BY e.EmployeeID
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                var employeeDict = new Dictionary<int, EmployeeWithReviews>();

                var employees = await connection.QueryAsync<EmployeeWithReviews, PerformanceReview, EmployeeWithReviews>(
                    query,
                    (employee, review) =>
                    {
                        if (!employeeDict.TryGetValue(employee.EmployeeID, out var empEntry))
                        {
                            empEntry = employee;
                            empEntry.PerformanceReviews = new List<PerformanceReview>();
                            employeeDict.Add(empEntry.EmployeeID, empEntry);
                        }

                        if (review != null)
                        {
                            empEntry.PerformanceReviews.Add(review);
                        }

                        return empEntry;
                    },
                    new { SearchTerm = searchTerm, Offset = (page - 1) * pageSize, PageSize = pageSize },
                    splitOn: "ReviewID"
                );

                return employeeDict.Values;
            }
        }



        public async Task<Employee> GetEmployeeById(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM Employee WHERE EmployeeID = @Id AND IsDeleted = 0";
                return await connection.QuerySingleOrDefaultAsync<Employee>(query, new { Id = id });
            }
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "INSERT INTO Employee (Name, Email, Phone, Position, JoinDate, DepartmentID, Status) " +
                            "VALUES (@Name, @Email, @Phone, @Position, @JoinDate, @DepartmentID, @Status)";
                return await connection.ExecuteAsync(query, employee);
            }
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "UPDATE Employee SET Name = @Name, Email = @Email, Phone = @Phone, " +
                            "Position = @Position, JoinDate = @JoinDate, DepartmentID = @DepartmentID,Status=@Status " +
                            "WHERE EmployeeID = @EmployeeID AND IsDeleted = 0";
                return await connection.ExecuteAsync(query, employee);
            }
        }

        public async Task<int> DeleteEmployee(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "UPDATE Employee SET IsDeleted = 1, Status = 'inactive' WHERE EmployeeID = @Id";
                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT DepartmentID, DepartmentName FROM Department";
                return await connection.QueryAsync<Department>(query);
            }
        }

        public async Task<int> GetTotalEmployeeCount(string searchTerm)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"
            SELECT COUNT(*) 
            FROM Employee 
            WHERE IsDeleted = 0 
            AND (@SearchTerm IS NULL OR Name LIKE '%' + @SearchTerm + '%')";

                return await connection.ExecuteScalarAsync<int>(query, new { SearchTerm = searchTerm });
            }
        }
    }
}
