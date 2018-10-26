using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Excalibur.Tests.Encrypted.Cross.Core.Utils
{
    public static class PinCodeExntensions
    {
        public static string TrimIllegalCharacters(this string input)
        {
            return Regex.Replace(input, ExampleConstants.PinRequirements.RegEx, "");
        }
    }

    public class ExampleConstants
    {
        public static class PinRequirements
        {
            public const int Length = 5;
            public const string RegEx = "[^0-9]";
        }
    }
}
