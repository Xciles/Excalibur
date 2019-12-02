using System.Security.Claims;
using System.Threading.Tasks;
using Excalibur.Avalon.Jwt;

namespace Excalibur.AspNetCore.Jwt
{
    /// <summary>
    /// Interface for creating everything regarding JWT
    /// </summary>
    public interface IJwtFactory
    {
        /// <summary>
        /// Method for generation of an JWT encoded token based on a deviceGuid and Identity.
        /// </summary>
        /// <param name="subject">The subject which is used in sub</param>
        /// <param name="identity">The Identity for the JWT info</param>
        /// <returns>A JWT string</returns>
        Task<string> GenerateEncodedToken(string subject, ClaimsIdentity identity);

        /// <summary>
        /// Method for generating a <see cref="ClaimsIdentity"/> used for JWT signing
        /// </summary>
        /// <param name="subject">The subject (user Id) which is used as sub</param>
        /// <param name="deviceId">A deviceId that will be added to the deviceId claim</param>
        /// <param name="id">An ID that can be used to identity the Identity</param>
        /// <returns>A ClaimIdentity</returns>
        ClaimsIdentity GenerateClaimsIdentity(string subject, string deviceId, string id);

        /// <summary>
        /// Method for generating a <see cref="JwtResponse"/> that contains information about the user that tries to sign-in or
        /// refreshes its access_token.
        /// </summary>
        /// <param name="identity">The Identity for the JWT token</param>
        /// <param name="subject">The DeviceGuid used by the identity</param>
        /// <param name="jwtOptions">Some options for the JWT token</param>
        /// <param name="refreshToken">A refresh token string that can be used to recreate JWT tokens</param>
        /// <returns>A JwtResponse containing all the information needed for authorization</returns>
        Task<T> GenerateJwt<T>(ClaimsIdentity identity, string subject, JwtIssuerOptions jwtOptions, string refreshToken = null)
            where T : JwtResponse, new();
    }
}