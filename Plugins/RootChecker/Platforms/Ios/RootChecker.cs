using System;
using System.Collections.Generic;
using System.IO;
using Foundation;
using UIKit;

namespace Excalibur.MvvmCross.Plugin.RootChecker.Platforms.Ios
{
    public class RootChecker : IRootChecker
    {
        /// <inheritdoc cref="IRootChecker"/>
        public bool IsRooted()
        {
            if (ObjCRuntime.Runtime.Arch == "SIMULATOR")
            {
                return false;
            }

            return CheckCydia()
                   || CheckKnownPackages()
                   || TestWriteAccess();
        }

        /// <summary>
        /// Check for Cydia
        /// </summary>
        /// <returns>True when available, false if not</returns>
        private bool CheckCydia()
        {

            if (UIApplication.SharedApplication.CanOpenUrl(new NSUrl("cydia://package/com.example.package")))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks for known packages that are available on root devices
        /// </summary>
        /// <returns>True when available, false if not</returns>
        private bool CheckKnownPackages()
        {

            var packages = new List<string>()
            {
                "/Applications/Cydia.app",
                "/Library/MobileSubstrate/MobileSubstrate.dylib",
                "/bin/bash",
                "/usr/sbin/sshd",
                "/etc/apt",
                "/private/var/lib/apt/",

            };

            foreach (var package in packages)
            {
                if (NSFileManager.DefaultManager.FileExists(package)) return true;
            }

            return false;

        }

        /// <summary>
        /// Checks if we have write access to a location that we shouldn't have access to
        /// </summary>
        /// <returns>True when we have access, false if not</returns>
        private bool TestWriteAccess()
        {
            var s = "test string";
            var path = "/private/jailbreak.txt";

            try
            {
                File.WriteAllText(path, s);
            }
            catch (Exception)
            {
                return false;
            }

            if (File.Exists(path)) File.Delete(path);

            return true;
        }
    }
}
