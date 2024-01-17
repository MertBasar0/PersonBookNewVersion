using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Models.AuthenticationApi.Login
{
    public class AuthApiLoginResponseModel
    {
        public bool IsAuthorized { get; set; }
        public string AccessToken { get; set; }
        public UserRefreshToken? RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
