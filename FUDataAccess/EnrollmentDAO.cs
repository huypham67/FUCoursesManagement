using FUBusiness.Models;
using FUBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FUDataAccess
{
    public class EnrollmentDAO
    {
        private static EnrollmentDAO _instance = null;
        private static readonly object _lock = new();
        private FuCourseManagementContext _dbContext;

        public static EnrollmentDAO Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new EnrollmentDAO();
                    return _instance;
                }
            }
        }

        public async Task<bool> EnrollStudent(int userId, int courseId)
        {
            _dbContext = new();
            // Kiểm tra xem sinh viên đã đăng ký khóa học chưa
            bool alreadyEnrolled = _dbContext.EnrollmentRecords.Any(e => e.UserId == userId && e.CourseId == courseId && e.Dropped == false);
            if (alreadyEnrolled)
                return false;

            // Kiểm tra còn chỗ không
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null || course.Capacity <= 0)
                return false;

            // Thêm bản ghi mới
            var enrollment = new EnrollmentRecord
            {
                UserId = userId,
                CourseId = courseId,
                EnrollDate = DateTime.Now,
                Dropped = false
            };

            await _dbContext.EnrollmentRecords.AddAsync(enrollment);
            course.Capacity--;
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<List<EnrollmentRecord>> GetEnrolledCourses(int userId)
        {
            _dbContext = new();
            return await _dbContext.EnrollmentRecords
                .Where(e => e.UserId == userId && e.Dropped == false).ToListAsync();
        }
        public async Task<List<EnrollmentRecord>> GetEnrollmentHistory(int userId)
        {
            _dbContext = new();
            return await _dbContext.EnrollmentRecords
                .Where(e => e.UserId == userId).Include(e => e.Course).ToListAsync();
        }

        public async Task<EnrollmentRecord?> GetEnrollmentRecord(int userId, int courseId)
        {
            _dbContext = new();
            return await _dbContext.EnrollmentRecords
                        .Where(e => e.UserId == userId && e.CourseId == courseId && e.Dropped == false)
                        .Include(e => e.Course).FirstOrDefaultAsync();
        }

        public async Task<bool> DropEnrollment(int userId, int courseId)
        {
            _dbContext = new();
            var enrollment = await GetEnrollmentRecord(userId, courseId);
            if (enrollment != null)
            {
                enrollment.Dropped = true;
                enrollment.Course.Capacity++;
                _dbContext.EnrollmentRecords.Update(enrollment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<EnrollmentRecord>> GetAllEnrollmentRecords()
        {
            _dbContext = new();
            return await _dbContext.EnrollmentRecords
                .Include(e => e.Course)
                .Include(e => e.User).ToListAsync();
        }
    }
}
