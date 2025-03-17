using FUBusiness.Models;
using FUDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FURepositories
{
    public class CourseRepository : ICourseRepository
    {
        public async Task<List<Course>> GetAllCourses()
        {
            return await CourseDAO.Instance.GetAllCourses();
        }

        public async Task<Course?> GetCourseById(int id)
        {
            return await CourseDAO.Instance.GetCourseById(id);
        }

        public async Task AddCourse(Course course)
        {
            await CourseDAO.Instance.AddCourse(course);
        }

        public async Task UpdateCourse(Course course)
        {
            await CourseDAO.Instance.UpdateCourse(course);
        }

        public async Task DeleteCourse(int id)
        {
            await CourseDAO.Instance.DeleteCourse(id);
        }

        public async Task<List<Course>> SearchCoursesByTitleAndCategory(string title, string category)
        {
            return await CourseDAO.Instance.SearchCoursesByTitleAndCategory(title, category);
        }
    }
}
