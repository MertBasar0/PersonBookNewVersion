using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonBookWebApplication.Consts;
using PersonBookWebApplication.Models;
using System.Text;
using Utilities.Dtos;
using Utilities.Dtos.AuthenticationApi;
using Utilities.Wrappers.WrapperGeneric;

namespace PersonBookWebApplication.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly HttpClient _AuthenticationServerClient;
        private readonly IMapper _mapper;
        public LoginController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _AuthenticationServerClient = httpClientFactory.CreateClient(ClientConsts.AuthenticationServerName);
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginModel)
        {
            var loginDto = _mapper.Map<AuthApiLoginRequestDto>(loginModel);

            var requestResponse = await _AuthenticationServerClient.PostAsJsonAsync(ClientConsts.AuthenticationServerLogin, loginDto);

            if (requestResponse.IsSuccessStatusCode)
            {
                var readedResponse = await requestResponse.Content.ReadFromJsonAsync<AuthApiResponseGenericModel<JwtTokenDto>>();
                if (readedResponse == null || !readedResponse.IsSuccess)
                    return View("Index");
                HttpContext.Session.Set("session", Encoding.UTF8.GetBytes(readedResponse.Data.AccessToken));
                return RedirectToAction("Index", "Person", new {area = "Main"});
            }
            return View();
        }
    }
}
