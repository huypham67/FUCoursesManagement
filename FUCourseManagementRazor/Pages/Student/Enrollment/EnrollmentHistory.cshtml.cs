using FUBusiness.Models;
using FURepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FUCourseManagementRazor.Pages.Student.Enrollment
{
    [Authorize(Roles = "Student")]
    public class EnrollmentHistoryModel : PageModel
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentHistoryModel(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public List<EnrollmentRecord> Enrollments { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = GetUserIdFromClaims();
            if (userId == null)
            {
                TempData["Error"] = "User not logged in.";
                return RedirectToPage("/Login");
            }

            Enrollments = await _enrollmentRepository.GetEnrollmentHistory(userId.Value);
            return Page();
        }

        private int? GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(userIdClaim, out int userId) ? userId : null;
        }
    }
}
