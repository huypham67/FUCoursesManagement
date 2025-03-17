using FUBusiness.Models;
using FUDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FURepositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUserByEmail(string email)
        {
            return await UserDAO.Instance.GetUserByEmail(email);
        }
    }
}
