using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Excalibur.AspNetCore.Utils
{
    /// <summary>
    /// Helper class for generating Random Passwords
    /// </summary>
    public static class PasswordHelper
    {
        private static readonly string[] RandomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            opts ??= new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            var rand = new Random(Environment.TickCount);
            var chars = new List<char>();

            if (opts.RequireUppercase)
            {
                chars.Insert(rand.Next(0, chars.Count), RandomChars[0][rand.Next(0, RandomChars[0].Length)]);
            }

            if (opts.RequireLowercase)
            {
                chars.Insert(rand.Next(0, chars.Count), RandomChars[1][rand.Next(0, RandomChars[1].Length)]);
            }

            if (opts.RequireDigit)
            {
                chars.Insert(rand.Next(0, chars.Count), RandomChars[2][rand.Next(0, RandomChars[2].Length)]);
            }

            if (opts.RequireNonAlphanumeric)
            {
                chars.Insert(rand.Next(0, chars.Count), RandomChars[3][rand.Next(0, RandomChars[3].Length)]);
            }

            for (var i = chars.Count; i < opts.RequiredLength || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                var rcs = RandomChars[rand.Next(0, RandomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count), rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
