namespace Excalibur.Avalon.Jwt
{
    /// <summary>
    /// Base entity that will be used to create tokens based on refresh token
    /// </summary>
    public class RefreshRequest
    {
        public string RefreshToken { get; set; }
    }
}