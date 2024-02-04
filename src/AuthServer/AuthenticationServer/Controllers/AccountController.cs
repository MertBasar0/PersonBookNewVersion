using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Utilities.Dtos;
using Utilities.Dtos.AuthenticationApi;
using Utilities.Wrappers.WrapperGeneric;

namespace AuthenticationServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountService;
        public AccountController(IAccountManager accountService)
        {
            _accountService = accountService;
        }


        [HttpGet]
        public IActionResult actionResult()
        {
            return null;
        }

        [HttpPost]
        public async Task<AuthApiResponseGenericModel<JwtTokenDto>> Login([FromBody]AuthApiLoginRequestDto authApiLoginRequestDto)
        {
            var response = await _accountService.LoginAsync(authApiLoginRequestDto);
            return response;
        }

        [HttpPost]
        public async Task<AuthApiResponseGenericModel<NoDataDto>> Register(AuthApiRegisterRequestDto authApiRegisterRequestDto)
        {
            return await _accountService.RegisterAsync(authApiRegisterRequestDto);
        }
    }
}
