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

namespace FUCourseManagementRazor.Pages.Admin.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly ICourseRepository _courseRepository;

        public DetailsModel(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetCourseById(id.Value);
            if (course == null)
            {
                return NotFound();
            }
            else
            {
                Course = course;
            }
            return Page();
        }
    }
}
