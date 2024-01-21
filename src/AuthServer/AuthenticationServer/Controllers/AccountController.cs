using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities.Dtos.AuthenticationApi;
using Utilities.Wrappers.WrapperGeneric;

namespace AuthenticationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public AuthApiResponseGenericModel<AuthApiLoginRequestDto> Login()
        {
            return null;
        }
    }
}
