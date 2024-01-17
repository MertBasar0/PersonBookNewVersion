using DataAccess.Abstract;
using DataAccess.Concrete.Infrastructure;
using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<AppRole> _roleManager;
        public readonly PasswordHasher<AppUser> _passwordHasher;
        public readonly SignInManager<AppUser> _signInManager;
        public readonly AuthDbContext _authDbContext;


        public UserRepository(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, PasswordHasher<AppUser> passwordHasher, SignInManager<AppUser> signInManager, AuthDbContext authDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
            _authDbContext = authDbContext;
        }

        public Task<AppUser> CreateUser()
        {

            //var result = _userManager.CreateAsync();

            //_authDbContext.

            return null;

        }

        public Task<List<AppUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetUserInActiveByData(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetUserInActiveById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> UpdateUserByData(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> UpdateUserById(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> UpdateUserByIdAndData(int id, AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
