using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemPAU.Models
{
    public class PerformanceReview
    {
        public int ReviewID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime ReviewDate { get; set; }
        [Range(1, 10, ErrorMessage = "Score must be between 1 and 10.")]
        public int ReviewScore { get; set; }
        public string ReviewNotes { get; set; }
       // public Employee Employee { get; set; }
    }
}
