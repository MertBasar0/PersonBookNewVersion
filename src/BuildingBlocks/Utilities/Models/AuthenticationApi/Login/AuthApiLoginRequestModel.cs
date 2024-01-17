using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Models.AuthenticationApi.Login
{
    public class AuthApiLoginRequestModel
    {
        public string Username { get; set; }

        public string Password { get; set; }


        public AuthApiLoginRequestModel()
        {

        }

        public AuthApiLoginRequestModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
