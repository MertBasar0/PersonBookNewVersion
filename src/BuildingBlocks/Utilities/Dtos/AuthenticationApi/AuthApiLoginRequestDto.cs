using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Dtos.AuthenticationApi
{
    public class AuthApiLoginRequestDto
    {
        public String Mail { get; set; }

        public String Password { get; set; }


        public AuthApiLoginRequestDto()
        {
        }

        public AuthApiLoginRequestDto(String mail, String password)
        {
            Mail = mail;
            Password = password;
        }
    }
}
