using FUBusiness.Models;
using FURepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUCourseManagementRazor.Pages.Student.Enrollment
{
    public class EnrollmentHistoryModel : PageModel
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EnrollmentHistoryModel(IEnrollmentRepository enrollmentRepository, IHttpContextAccessor httpContextAccessor)
        {
            _enrollmentRepository = enrollmentRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<EnrollmentRecord> Enrollments { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Student")
            {
                return RedirectToPage("/AccessDenied");
            }
            int? userId = GetUserIdFromSession();
            if (userId == null)
            {
                TempData["Error"] = "User not logged in.";
                return RedirectToPage("/Login");
            }

            Enrollments = await _enrollmentRepository.GetEnrollmentHistory(userId.Value);
            return Page();
        }

        private int? GetUserIdFromSession()
        {
            return _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
        }
    }
}
