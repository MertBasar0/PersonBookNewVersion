using Microsoft.IdentityModel.Tokens;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Identity;
using Utilities.Dtos.AuthenticationApi;
using Utilities.Wrappers.WrapperGeneric;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using AuthServer.Core;
using System.Security.Cryptography;
using System.Security.Authentication;
using System.Text;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Utilities.Dto;
using Utilities.Dtos;
using Utilities;
using System.Text.Json;

namespace Business.Concrete
{
    public class AccountService : IAccountManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private CommonTokenOption _tokenOptions;



        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<CommonTokenOption> tokenOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenOptions = tokenOptions.Value;
        }

        public async Task<AuthApiResponseGenericModel<JwtTokenDto>> LoginAsync(AuthApiLoginRequestDto authApiLoginRequestDto)
        {
            AppUser user = await  _userManager.FindByEmailAsync(authApiLoginRequestDto.Mail);
            byte[] securityKey = Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey);
            if (user != null)
            {

                if (await _userManager.CheckPasswordAsync(user, authApiLoginRequestDto.Password))
                {
                    var claims = await _userManager.GetClaimsAsync(user);



                    var securityToken = new JwtSecurityToken(
                        issuer: _tokenOptions.Issuer,
                        audience: _tokenOptions.Audiances,
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey)), SecurityAlgorithms.HmacSha256)
                        );



                    JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

                    var token = jwtSecurityTokenHandler.WriteToken(securityToken);

                    return AuthApiResponseGenericModel<JwtTokenDto>.Success(new JwtTokenDto(token, DateTime.UtcNow.AddDays(1).ToString()), System.Net.HttpStatusCode.OK);

                }
                
            }
            return AuthApiResponseGenericModel<JwtTokenDto>.Fail(ErrorDto.Create("Giriş işlemi başarısız", true), System.Net.HttpStatusCode.Unauthorized);
        }

        public async Task<AuthApiResponseGenericModel<NoDataDto>> RegisterAsync(AuthApiRegisterRequestDto authApiRegisterRequestDto)
        {
            var user = AppUser.CreateUser(authApiRegisterRequestDto.Username, authApiRegisterRequestDto.Email);
            IdentityResult result = null;
            try
            {
                result = await _userManager.CreateAsync(user, authApiRegisterRequestDto.Password);

                var createdUser = await _userManager.FindByEmailAsync(authApiRegisterRequestDto.Email);

                if (createdUser != null)
                {
                    await _userManager.AddClaimsAsync(createdUser, new List<Claim>() { new Claim(ClaimTypes.Name, authApiRegisterRequestDto.Username), new Claim(ClaimTypes.Email, authApiRegisterRequestDto.Email), new Claim(ClaimTypes.Role, "admin")} );
                }
            }
            catch (Exception)
            {

                throw;
            }
            if (result.Succeeded)
                return AuthApiResponseGenericModel<NoDataDto>.Success(new NoDataDto(), System.Net.HttpStatusCode.OK);
            else
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return AuthApiResponseGenericModel<NoDataDto>.Fail(ErrorDto.Create(errors, true), System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
