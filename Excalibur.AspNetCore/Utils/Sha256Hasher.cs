using System;
using System.Security.Cryptography;
using System.Text;

namespace Excalibur.AspNetCore.Utils
{
    /// <summary>
    /// Helper for generating Sha256 Hashes
    /// </summary>
    public static class Sha256Hasher
    {
        /// <summary>
        /// Hashes a string with SHA256
        /// </summary>
        /// <param name="str">The to be hashed string</param>
        /// <returns><see cref="str"/> hashed</returns>
        public static string Hash(string str)
        {
            using var sha256 = SHA256.Create();

            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}