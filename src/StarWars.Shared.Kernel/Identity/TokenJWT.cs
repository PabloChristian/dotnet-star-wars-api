namespace StarWars.Shared.Kernel.Identity
{
    public class TokenJwt
    {
        public TokenJwt(bool authenticated, string token)
        {
            Authenticated = authenticated;
            Token = token;
            RefreshToken = string.Empty;
        }

        public bool Authenticated { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; internal set; }
    }
}
