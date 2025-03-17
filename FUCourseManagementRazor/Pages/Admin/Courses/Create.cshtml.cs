using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FUBusiness;
using FUBusiness.Models;
using FURepositories;
using Microsoft.AspNetCore.Authorization;

namespace FUCourseManagementRazor.Pages.Admin.Courses
{
    public class CreateModel : PageModel
    {
        private readonly ICourseRepository _courseRepository;

        public CreateModel(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IActionResult OnGet()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/AccessDenied");
            }
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _courseRepository.AddCourse(Course);
            return RedirectToPage("./Index");
        }
    }
}
