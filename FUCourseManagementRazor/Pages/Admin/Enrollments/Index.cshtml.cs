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

namespace FUCourseManagementRazor.Pages.Admin.Enrollments
{
    public class IndexModel : PageModel
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public IndexModel(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public IList<EnrollmentRecord> EnrollmentRecord { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/AccessDenied");
            }
            EnrollmentRecord = await _enrollmentRepository.GetAllEnrollmentRecords();
            return Page();
        }
    }
}
