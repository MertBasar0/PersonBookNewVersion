namespace AuthServer.Core.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        public string AccessTokenExpiration { get; set; }
    }
}