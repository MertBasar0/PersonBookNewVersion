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
            var personDto = _mapper.Map<PersonViewModel, PersonDto>(personModel);
            await _personAppService.CreatePerson(personDto);
            return await Task.FromResult(View(personDto));
        }
    }
}
