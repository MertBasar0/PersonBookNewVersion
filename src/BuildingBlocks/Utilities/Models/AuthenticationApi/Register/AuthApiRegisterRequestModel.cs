using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Models.AuthenticationApi.Register
{
    public class AuthApiRegisterRequestModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public AuthApiRegisterRequestModel()
        {

        }

        public AuthApiRegisterRequestModel(string username, string password, string email, string telephone)
        {
            Username = username;
            Password = password;
            Email = email;
            Telephone = telephone;
        }
    }
}
