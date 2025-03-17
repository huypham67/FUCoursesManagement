using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FUBusiness;
using FUBusiness.Models;
using FURepositories;

namespace FUCourseManagementRazor.Pages.Student.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ICourseRepository courseRepository, 
            IEnrollmentRepository enrollmentRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchCategory { get; set; }

        public IList<Course> Course { get;set; } = default!;

        public List<int?> EnrolledCourses { get; set; } = new();
        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Student")
            {
                return RedirectToPage("/AccessDenied");
            }
            int? userId = GetUserIdFromSession();
            Course = await _courseRepository.GetAllCourses();
            if (userId != null)
            {
                var enrolledRecords = await _enrollmentRepository.GetEnrolledCourses(userId.Value);
                EnrolledCourses = enrolledRecords.Select(e => e.CourseId).ToList();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            Course = await _courseRepository.SearchCoursesByTitleAndCategory(SearchTitle, SearchCategory);
            return Page();
        }

        public async Task<IActionResult> OnPostEnrollAsync(int id)
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

            bool success = await _enrollmentRepository.EnrollStudent(userId.Value, id);

            if (success)
            {
                TempData["Success"] = "Successfully enrolled in the course.";
            }
            else
            {
                TempData["Error"] = "Failed to enroll. Course may be full or already enrolled.";
            }

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostToggleEnrollmentAsync(int id)
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

            var enrollment = await _enrollmentRepository.GetEnrollmentRecord(userId.Value, id);

            if (enrollment != null)
            {
                // Nếu đã đăng ký, thì hủy đăng ký
                bool success = await _enrollmentRepository.DropEnrollment(userId.Value, id);

                TempData["Success"] = success ? "Successfully dropped the course." : "Failed to drop the course.";
            }
            else
            {
                // Nếu chưa đăng ký, thực hiện đăng ký
                bool success = await _enrollmentRepository.EnrollStudent(userId.Value, id);

                TempData["Success"] = success ? "Successfully enrolled in the course." : "Failed to enroll. Course may be full or already enrolled.";
            }
            return RedirectToPage();
        }

        private int? GetUserIdFromSession()
        {
            return _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
        }

    }
}
