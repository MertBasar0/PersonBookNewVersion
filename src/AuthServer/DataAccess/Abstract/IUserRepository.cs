using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserById(int id);
        Task<List<AppUser>> GetAllUsers();
        Task<bool> SetUserInActiveById(int id);
        Task<bool> SetUserInActiveByData(AppUser user);
        Task<AppUser> UpdateUserByData(AppUser user);
        Task<AppUser> UpdateUserById(AppUser user);
        Task<AppUser> UpdateUserByIdAndData(int id, AppUser user);
        Task<AppUser> CreateUser();
    }
}
