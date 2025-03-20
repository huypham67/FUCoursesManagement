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
using Microsoft.AspNetCore.Authorization;

namespace FUCourseManagementRazor.Pages.Admin.Enrollments
{
    [Authorize(Roles = "Admin")]
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
            EnrollmentRecord = await _enrollmentRepository.GetAllEnrollmentRecords();
            return Page();
        }
    }
}
