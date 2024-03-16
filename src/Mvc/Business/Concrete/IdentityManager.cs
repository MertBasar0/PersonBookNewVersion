using Business.Abstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class IdentityManager : IIdentityManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> GetUserMail()
        {
            var claims = _httpContextAccessor.HttpContext.User;


            return Task.FromResult(string.Empty);
        }
    }
}
