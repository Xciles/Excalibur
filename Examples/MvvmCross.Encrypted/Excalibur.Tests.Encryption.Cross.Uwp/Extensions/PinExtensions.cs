using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Windows.System;
using Excalibur.Tests.Encrypted.Cross.Core.Utils;

namespace Excalibur.Tests.Encrypted.Cross.Uwp.Extensions
{
    public static class PinExtensions
    {
        public static bool IsNumber(this VirtualKey key)
        {
            try
            {
                return !Regex.IsMatch(((Char)key).ToString(), ExampleConstants.PinRequirements.RegEx) ||
                       (key >= VirtualKey.NumberPad0 && key <= VirtualKey.NumberPad9) ||
                       (key >= VirtualKey.Number0 && key <= VirtualKey.Number9);
            }
            catch (InvalidCastException ex)
            {
                Debug.WriteLine($"Couldn't cast the key to a char, {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"VirtualKey IsNumber check failed, {ex.Message}");
            }

            return false;
        }

        public static string SanitizePin(this string input)
        {
            return Regex.Replace(input, ExampleConstants.PinRequirements.RegEx, "");
        }
    }
}
