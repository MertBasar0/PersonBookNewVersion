using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Dtos.AuthenticationApi
{
    public class AuthApiRegisterRequestDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public AuthApiRegisterRequestDto()
        {

        }

        public AuthApiRegisterRequestDto(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
