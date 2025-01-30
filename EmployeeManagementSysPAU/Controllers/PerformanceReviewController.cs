using EmployeeManagementSysPAU.Service.PerfomanceRev;
using EmployeeManagementSystemPAU.Models;
using EmployeeManagementSystemPAU.Service.Emp;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSysPAU.Controllers
{
    public class PerformanceReviewController : Controller
    {
        private readonly IPerformanceReviewService _reviewRepository;
        private readonly IEmployeeService _employeeRepository;

        public PerformanceReviewController(IPerformanceReviewService reviewRepository, IEmployeeService employeeRepository)
        {
            _reviewRepository = reviewRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<IActionResult> Index(int employeeId)
        {
            var reviews = await _reviewRepository.GetReviewsByEmployeeId(employeeId);
            ViewBag.Employee = await _employeeRepository.GetEmployeeById(employeeId);
            return View(reviews);
        }

        public IActionResult Create(int employeeId)
        {
            var review = new PerformanceReview { EmployeeID = employeeId, ReviewDate = DateTime.Now };
            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PerformanceReview review)
        {
            
            
                await _reviewRepository.AddReview(review);
                return RedirectToAction("Index", "Employee"); 
            
            //return View(review);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _reviewRepository.GetReviewById(id);
            if (review == null) return NotFound();
            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PerformanceReview review)
        {
           
            
                await _reviewRepository.UpdateReview(review);
                return RedirectToAction("Index", "Employee"); 
            
            //return View(review);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewRepository.GetReviewById(id);
            if (review != null)
            {
                await _reviewRepository.DeleteReview(id);
                return RedirectToAction("Index", new { employeeId = review.EmployeeID });
            }
            return NotFound();
        }
    }
}
