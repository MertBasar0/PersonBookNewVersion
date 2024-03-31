using AutoMapper;
using Core.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Business.Abstraction;
using PersonBookWebApplication.Areas.Per.Models;

namespace PersonBookWebApplication.Per.Main.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
    [Area("Per")]
    public class PersonController : Controller
    {
        private readonly IPersonAppService _personAppService;
        private readonly IMapper _mapper;

        public PersonController(IPersonAppService personAppService, IMapper mapper)
        {
            _personAppService = personAppService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(PersonViewModel personModel)
        {
            personModel.Gender = Core.Enums.Gender.Unknown;
            if (ModelState.IsValid)
            {
                var personDto = _mapper.Map<PersonViewModel, PersonDto>(personModel);
                await _personAppService.CreatePersonAsync(personDto);
                return await Task.FromResult(RedirectToAction("Index"));
            }
            return await Task.FromResult(View(personModel));
        }
    }
}
