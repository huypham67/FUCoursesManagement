using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FUBusiness;
using FUBusiness.Models;
using FURepositories;

namespace FUCourseManagementRazor.Pages.Admin.Courses
{
    public class EditModel : PageModel
    {
        private readonly ICourseRepository _courseRepository;

        public EditModel(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetCourseById(id.Value);
            if (course == null)
            {
                return NotFound();
            }
            Course = course;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/AccessDenied");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _courseRepository.UpdateCourse(Course);

            return RedirectToPage("./Index");
        }

    }
}
