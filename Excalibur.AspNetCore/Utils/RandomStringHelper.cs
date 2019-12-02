using System;
using System.Linq;

namespace Excalibur.AspNetCore.Utils
{
    /// Todo RandomStringHelper

    /// <summary>
    /// Just a class to generate random strings instead of using guids
    /// </summary>
    public static class RandomStringHelper
    {
        private static readonly Random Random = new Random();

        /// <summary>
        /// Generates some random string
        /// </summary>
        /// <param name="length">The length of the to be generated string</param>
        /// <returns>The random string</returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}