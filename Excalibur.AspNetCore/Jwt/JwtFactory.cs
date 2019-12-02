using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Excalibur.Avalon.Jwt;
using Microsoft.Extensions.Options;

namespace Excalibur.AspNetCore.Jwt
{
    /// todo
    /// refresh token fixen
    /// obsolete toelie weg
    /// claims change to list of items
    /// refresh expiration
    ///  
    /// <inheritdoc cref="IJwtFactory"/>
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        /// <inheritdoc cref="IJwtFactory"/>
        public async Task<string> GenerateEncodedToken(string subject, ClaimsIdentity identity)
        {
            // All the claims for the user
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst(JwtConstants.Strings.JwtClaimIdentifiers.Rol),
                identity.FindFirst(JwtConstants.Strings.JwtClaimIdentifiers.Id),
                identity.FindFirst(JwtConstants.Strings.JwtClaimIdentifiers.DeviceId),
                identity.FindFirst(JwtConstants.Strings.JwtClaimIdentifiers.Email)
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        /// <inheritdoc cref="IJwtFactory"/>
        public ClaimsIdentity GenerateClaimsIdentity(string subject, string deviceId, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(subject, JwtConstants.Strings.JwtClaimIdentifiers.Token), new[]
            {
                new Claim(JwtConstants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(JwtConstants.Strings.JwtClaimIdentifiers.DeviceId, deviceId),
                new Claim(JwtConstants.Strings.JwtClaimIdentifiers.Rol, JwtConstants.Strings.JwtClaims.ApiAccess)
            });
        }

        /// <inheritdoc cref="IJwtFactory"/>
        public async Task<T> GenerateJwt<T>(ClaimsIdentity identity, string subject, JwtIssuerOptions jwtOptions, string refreshToken = null)
            where T : JwtResponse, new()
        {
            var response = new T
            {
                Id = identity.Claims.Single(c => c.Type == JwtConstants.Strings.JwtClaimIdentifiers.Id).Value,
                AccessToken = await GenerateEncodedToken(subject, identity),
                RefreshToken = refreshToken,
                ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return response;
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}