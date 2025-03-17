using FUBusiness;
using FUBusiness.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUDataAccess
{
    public class UserDAO
    {
        private static UserDAO _instance = null;
        private static readonly object _lock = new();
        private FuCourseManagementContext _dbContext;


        public static UserDAO Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new UserDAO();
                    return _instance;
                }
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            _dbContext = new();
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
