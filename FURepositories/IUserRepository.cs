using FUBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FURepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
    }
}
