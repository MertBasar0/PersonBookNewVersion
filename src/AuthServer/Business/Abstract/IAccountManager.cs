using AuthServer.Core.Dtos;
using Utilities.Dtos;
using Utilities.Dtos.AuthenticationApi;
using Utilities.Wrappers.WrapperGeneric;

namespace Business.Abstract
{
    public interface IAccountManager
    {
        Task<AuthApiResponseGenericModel<JwtTokenDto>> LoginAsync(AuthApiLoginRequestDto authApiLoginRequestDto);
        Task<AuthApiResponseGenericModel<NoDataDto>> RegisterAsync(AuthApiRegisterRequestDto authApiRegisterRequestDto);
    }
}
