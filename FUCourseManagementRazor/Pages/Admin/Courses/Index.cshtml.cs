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
    public class IndexModel : PageModel
    {
        private readonly ICourseRepository _courseRepository;

        public IndexModel(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IList<Course> Course { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Course = await _courseRepository.GetAllCourses();
        }
    }
}
