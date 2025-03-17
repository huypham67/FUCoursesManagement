using FUBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FURepositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCourses();
        Task<Course?> GetCourseById(int id);
        Task AddCourse(Course course);
        Task UpdateCourse(Course course);
        Task DeleteCourse(int id);
        Task<List<Course>> SearchCoursesByTitleAndCategory(string title, string category);
    }
}
