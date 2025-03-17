using FUBusiness.Models;
using FUDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FURepositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        public async Task<bool> DropEnrollment(int userId, int courseId)
        {
            return await EnrollmentDAO.Instance.DropEnrollment(userId, courseId);
        }

        public async Task<bool> EnrollStudent(int userId, int courseId)
        {
            return await EnrollmentDAO.Instance.EnrollStudent(userId, courseId);
        }

        public async Task<List<EnrollmentRecord>> GetAllEnrollmentRecords()
        {
            return await EnrollmentDAO.Instance.GetAllEnrollmentRecords();
        }

        public async Task<List<EnrollmentRecord>> GetEnrolledCourses(int userId)
        {
            return await EnrollmentDAO.Instance.GetEnrolledCourses(userId);
        }

        public async Task<List<EnrollmentRecord>> GetEnrollmentHistory(int userId)
        {
            return await EnrollmentDAO.Instance.GetEnrollmentHistory(userId);
        }

        public async Task<EnrollmentRecord?> GetEnrollmentRecord(int userId, int courseId)
        {
            return await EnrollmentDAO.Instance.GetEnrollmentRecord(userId, courseId);
        }
    }
}
