using FUBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FURepositories
{
    public interface IEnrollmentRepository
    {
        Task<bool> DropEnrollment(int value, int id);
        Task<bool> EnrollStudent(int userId, int courseId);
        Task<List<EnrollmentRecord>> GetAllEnrollmentRecords();
        Task<List<EnrollmentRecord>> GetEnrolledCourses(int userId);
        Task<List<EnrollmentRecord>> GetEnrollmentHistory(int userId);
        Task<EnrollmentRecord?> GetEnrollmentRecord(int userId, int courseId);
    }
}
