using Newtonsoft.Json;

namespace Excalibur.Avalon.Jwt
{
    /// <summary>
    /// Base response entity that will be send back to clients
    /// </summary>
    public class JwtResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("auth_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
    }
}