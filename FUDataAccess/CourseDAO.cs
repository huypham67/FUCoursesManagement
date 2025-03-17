using FUBusiness.Models;
using FUBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;

namespace FUDataAccess
{
    public class CourseDAO
    {
        private static CourseDAO _instance = null;
        private static readonly object _lock = new();
        private FuCourseManagementContext _dbContext;

        public static CourseDAO Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new CourseDAO();
                    return _instance;
                }
            }
        }

        public async Task<List<Course>> GetAllCourses()
        {
            _dbContext = new();
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task<Course?> GetCourseById(int id)
        {
            _dbContext = new();
            return await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCourse(Course course)
        {
            _dbContext = new();
            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCourse(Course course)
        {
            _dbContext = new();
            _dbContext.Courses.Update(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCourse(int id)
        {
            _dbContext = new();
            var course = await _dbContext.Courses.FindAsync(id);
            if (course != null)
            {
                _dbContext.Courses.Remove(course);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Course>> SearchCoursesByTitleAndCategory(string title, string category)
        {
            _dbContext = new();
            var query = _dbContext.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(c => c.Title.Contains(title));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(c => c.Category.Contains(category));

            return await query.ToListAsync();
        }
    }
}
