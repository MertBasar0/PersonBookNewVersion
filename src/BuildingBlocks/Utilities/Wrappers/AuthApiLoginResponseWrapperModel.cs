using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Models.AuthenticationApi.Login;
using Utilities.Wrappers.WrapperGeneric;

namespace Utilities.Wrappers
{
    public class AuthApiLoginResponseWrapperModel
    {
        public AuthApiResponseGenericModel<AuthApiLoginResponseModel>? AuthLoginResponseModel { get; set; }
    }
}
