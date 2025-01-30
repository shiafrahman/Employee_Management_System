using EmployeeManagementSystemPAU.Models;

namespace EmployeeManagementSysPAU.Service.PerfomanceRev
{
    public interface IPerformanceReviewService
    {
        Task<IEnumerable<PerformanceReview>> GetReviewsByEmployeeId(int employeeId);
        Task<PerformanceReview> GetReviewById(int reviewId);
        Task<int> AddReview(PerformanceReview review);
        Task<int> UpdateReview(PerformanceReview review);
        Task<int> DeleteReview(int reviewId);
    }
}
