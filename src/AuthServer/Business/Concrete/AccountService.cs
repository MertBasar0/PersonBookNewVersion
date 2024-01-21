using AuthServer.Core.Dtos;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Identity;
using Utilities.Dtos.AuthenticationApi;
using Utilities.Wrappers.WrapperGeneric;

namespace Business.Concrete
{
    public class AccountService : IAccountManager
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppUser> roleManager;

        public AccountService(UserManager<AppUser> userManager, RoleManager<AppUser> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<AuthApiResponseGenericModel<TokenDto>> Login(AuthApiLoginRequestDto authApiLoginRequestDto)
        {
            AppUser user = await  userManager.FindByNameAsync(authApiLoginRequestDto.Mail);

            //Buradan sonra kullanıcının null kontrolü ve token üretimi ile devam edilecek.

        }

        public Task<AuthApiResponseGenericModel<NoDataDto>> Register(AuthApiRegisterRequestDto authApiRegisterRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
