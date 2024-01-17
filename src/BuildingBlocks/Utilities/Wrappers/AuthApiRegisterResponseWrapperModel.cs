using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Models;
using Utilities.Models.AuthenticationApi.Register;
using Utilities.Wrappers.WrapperGeneric;

namespace Utilities.Wrappers
{
    public class AuthApiRegisterResponseWrapperModel
    {
        public AuthApiResponseGenericModel<AuthApiRegisterResponseModel>? AuthRegisterResponseModel { get; set; }
    }
}
