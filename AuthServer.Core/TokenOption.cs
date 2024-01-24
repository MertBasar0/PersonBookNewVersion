using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core
{
    public class TokenOption
    {
        public String Issuer { get; set; }

        public String Audience { get; set; }

        public String SecurityKey { get; set; }
    }
}
