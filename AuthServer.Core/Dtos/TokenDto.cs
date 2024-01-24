namespace AuthServer.Core.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        public string AccessTokenExpiration { get; set; }


        public TokenDto()
        {
                
        }

        public TokenDto(string accessToken, string accessTokenExpiration)
        {
            AccessToken = accessToken;
            AccessTokenExpiration = accessTokenExpiration;
        }
    }
}