using Dapper;
using EmployeeManagementSystemPAU.DB;
using EmployeeManagementSystemPAU.Models;

namespace EmployeeManagementSysPAU.Service.PerfomanceRev
{
    public class PerformanceReviewService:IPerformanceReviewService
    {
        private readonly DapperContext _context;
        public PerformanceReviewService(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PerformanceReview>> GetReviewsByEmployeeId(int employeeId)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM PerformanceReview WHERE EmployeeID = @EmployeeID";
                return await connection.QueryAsync<PerformanceReview>(query, new { EmployeeID = employeeId });
            }
        }

        public async Task<PerformanceReview> GetReviewById(int reviewId)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM PerformanceReview WHERE ReviewID = @ReviewID";
                return await connection.QuerySingleOrDefaultAsync<PerformanceReview>(query, new { ReviewID = reviewId });
            }
        }

        public async Task<int> AddReview(PerformanceReview review)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"INSERT INTO PerformanceReview (EmployeeID, ReviewDate, ReviewScore, ReviewNotes)
                          VALUES (@EmployeeID, @ReviewDate, @ReviewScore, @ReviewNotes)";
                return await connection.ExecuteAsync(query, review);
            }
        }

        public async Task<int> UpdateReview(PerformanceReview review)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = @"UPDATE PerformanceReview 
                          SET ReviewDate = @ReviewDate, ReviewScore = @ReviewScore, ReviewNotes = @ReviewNotes
                          WHERE ReviewID = @ReviewID";
                return await connection.ExecuteAsync(query, review);
            }
        }

        public async Task<int> DeleteReview(int reviewId)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "DELETE FROM PerformanceReview WHERE ReviewID = @ReviewID";
                return await connection.ExecuteAsync(query, new { ReviewID = reviewId });
            }
        }
    }
}
