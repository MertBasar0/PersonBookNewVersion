using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Dtos
{
    public class JwtTokenDto
    {
        public string AccessToken { get; set; }

        public string AccessTokenExpiration { get; set; }


        public JwtTokenDto()
        {

        }

        public JwtTokenDto(string accessToken, string accessTokenExpiration)
        {
            AccessToken = accessToken;
            AccessTokenExpiration = accessTokenExpiration;
        }
    }
}
