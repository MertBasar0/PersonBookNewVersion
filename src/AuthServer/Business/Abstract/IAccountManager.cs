using AuthServer.Core.Dtos;
using Utilities.Dtos.AuthenticationApi;
using Utilities.Wrappers.WrapperGeneric;

namespace Business.Abstract
{
    public interface IAccountManager
    {
        Task<AuthApiResponseGenericModel<TokenDto>> Login(AuthApiLoginRequestDto authApiLoginRequestDto);
        Task<AuthApiResponseGenericModel<NoDataDto>> Register(AuthApiRegisterRequestDto authApiRegisterRequestDto);
    }
}
