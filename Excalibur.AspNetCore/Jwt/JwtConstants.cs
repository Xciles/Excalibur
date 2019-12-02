using System;
using System.Collections.Generic;
using System.Text;

namespace Excalibur.AspNetCore.Jwt
{
    [Obsolete]
    public static class JwtConstants
    {
        [Obsolete]
        public static class Strings
        {
            [Obsolete]
            public static class JwtClaimIdentifiers
            {
                [Obsolete]
                public const string Rol = "rol", Id = "id", Email = "eml", DeviceId = "did";
                [Obsolete]
                public const string Token = "Token";
            }

            [Obsolete]
            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }
    }
}
