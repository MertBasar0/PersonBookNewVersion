﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonBookWebApplication.Consts;
using PersonBookWebApplication.Models;
using System.Text;
using System.Text.Json;
using Utilities.Dtos;
using Utilities.Dtos.AuthenticationApi;
using Utilities.Wrappers.WrapperGeneric;

namespace PersonBookWebApplication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly HttpClient _AuthenticationServerClient;
        private readonly IMapper _mapper;
        public AccountController(IHttpClientFactory httpClientFactory, IMapper mapper)
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
            if (!ModelState.IsValid)
            {
                return View("Index", loginModel);
            }
            var loginDto = _mapper.Map<AuthApiLoginRequestDto>(loginModel);
            try
            {
                var requestResponse = await _AuthenticationServerClient.PostAsJsonAsync<AuthApiLoginRequestDto>(ClientConsts.AuthenticationServerLogin, loginDto);
                if (requestResponse.IsSuccessStatusCode)
                {
                    var readedResponse = await requestResponse.Content.ReadFromJsonAsync<AuthApiResponseGenericModel<JwtTokenDto>>();
                    //AuthApiResponseGenericModel<JwtTokenDto> deserializedResponse = JsonConvert.DeserializeObject<AuthApiResponseGenericModel<JwtTokenDto>>(readedResponse);
                    if (readedResponse == null || !readedResponse.IsSuccess)
                    {
                        TempData["badRequestMessage"] = "Giriş başarısız. Lütfen bilgilerinizi kontrol edin.";
                        return View("Index");
                    }
                    var securityKeyByteArray = Encoding.UTF8.GetBytes(readedResponse.Data?.AccessToken);
                    HttpContext.Session.Set("session", securityKeyByteArray);
                }
                return RedirectToAction("Index", "Person", new { area = "Main" });
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerModel)
        {
            if (!ModelState.IsValid) return View("Index");

            var registerDto = _mapper.Map<AuthApiRegisterRequestDto>(registerModel);

            HttpResponseMessage requestResponse = await _AuthenticationServerClient.PostAsJsonAsync(ClientConsts.AuthenticationServerRegister, registerDto);
        
            if(requestResponse.IsSuccessStatusCode)
            {
                AuthApiResponseGenericModel<NoDataDto>? readedResponse = await requestResponse.Content?.ReadFromJsonAsync<AuthApiResponseGenericModel<NoDataDto>>();

                if(readedResponse == null || !readedResponse.IsSuccess)
                {
                    TempData["badRequestMessage"] = "Kayıt işlemi gerçekleştirilememiştir.";
                    return View("Index");
                }
                TempData["successMessage"] = "Kayıt işlemi başarıyla gerçekleştirildi";
            }
            return View("Index");
        }
    }
}
