using System;
using Android.Util;
using Java.IO;
using Java.Util;
using Console = System.Console;

namespace Excalibur.MvvmCross.Plugin.RootChecker.Platforms.Android
{
    internal static class RootCheckerUtils
    {
        /// <summary>
        /// Checks if binary exists.
        /// </summary>
        /// <returns><c>true</c>, if binary exists, <c>false</c> otherwise.</returns>
        /// <param name="filename">Filename.</param>
        public static bool CheckForBinary(string filename)
        {
            bool result = false;

            foreach (var path in RootCheckerConstants.SuPaths)
            {
                var completePath = path + filename;
                var f = new File(completePath);
                var fileExists = f.Exists();
                if (fileExists)
                {
                    Log.Info("CheckForBinary", $"{completePath} binary detected!");
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Reads all android props for a given command
        /// </summary>
        /// <returns>A string[] with the result.</returns>
        public static string[] ReaderFor(string command)
        {
            System.IO.Stream inputStream = null;
            try
            {
                inputStream = Java.Lang.Runtime.GetRuntime().Exec(command).InputStream;
            }
            catch (IOException ex)
            {
                Log.Error("ReaderFor", $"Unable to retrieve inputStream {ex.Message}");
            }

            // If input steam is null, we can't read the file, so return null
            if (inputStream == null) return new string[] { };

            var propval = "";
            try
            {
                propval = new Scanner(inputStream).UseDelimiter("\\A").Next();
            }
            catch (NoSuchElementException ex)
            {
                Log.Error("ReaderFor", $"NoSuchElementException: {ex.Message}");
            }

            try
            {
                inputStream.Close();
                inputStream.Dispose();
            }
            catch (Exception ex)
            {
                Log.Error("ReaderFor", $"Closing: {ex.Message}");
            }


            return propval.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}