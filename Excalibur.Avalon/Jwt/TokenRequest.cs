namespace Excalibur.Avalon.Jwt
{
    /// <summary>
    /// Base token request entity
    /// </summary>
    public class TokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}