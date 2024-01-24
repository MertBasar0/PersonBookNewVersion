using Microsoft.IdentityModel.Tokens;
using AuthServer.Core.Dtos;
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

namespace Business.Concrete
{
    public class AccountService : IAccountManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private TokenOption _tokenOptions;



        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<TokenOption> tokenOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenOptions = tokenOptions.Value;
        }

        public async Task<AuthApiResponseGenericModel<JwtTokenDto>> LoginAsync(AuthApiLoginRequestDto authApiLoginRequestDto)
        {
            AppUser user = await  _userManager.FindByEmailAsync(authApiLoginRequestDto.Mail);

            if(user != null)
            {
                var canSignIn = await _signInManager.CanSignInAsync(user);

                if (canSignIn)
                {
                    if(await _userManager.CheckPasswordAsync(user, authApiLoginRequestDto.Password))
                    {
                        var claims = await _userManager.GetClaimsAsync(user);

                        var securityToken = new JwtSecurityToken(
                            issuer: _tokenOptions.Issuer,
                            audience: _tokenOptions.Audience,
                            claims: claims,
                            notBefore: DateTime.UtcNow,
                            expires: DateTime.UtcNow.AddDays(1),
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey)), HashAlgorithmType.Sha256.ToString())
                            );

                        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

                        String token = jwtSecurityTokenHandler.WriteToken(securityToken);

                        return AuthApiResponseGenericModel<JwtTokenDto>.Success(new JwtTokenDto(token, DateTime.UtcNow.AddDays(1).ToString()), System.Net.HttpStatusCode.OK);
                    }
                }
            }
            return AuthApiResponseGenericModel<JwtTokenDto>.Fail(ErrorDto.Create("Giriş işlemi başarısız", true), System.Net.HttpStatusCode.Unauthorized);
        }

        public async Task<AuthApiResponseGenericModel<NoDataDto>> RegisterAsync(AuthApiRegisterRequestDto authApiRegisterRequestDto)
        {
            var user = AppUser.CreateUser(authApiRegisterRequestDto.Username, authApiRegisterRequestDto.Email);
            user.PhoneNumber = authApiRegisterRequestDto.Telephone;
            IdentityResult result = await _userManager.CreateAsync(user, authApiRegisterRequestDto.Password);
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
