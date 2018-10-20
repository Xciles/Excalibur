using System.Collections.Generic;

namespace Excalibur.MvvmCross.Plugin.RootChecker.Platforms.Android
{
    internal class RootCheckerConstants
    {
        /// <summary>
        /// The known Root Apps packages
        /// </summary>
        public static readonly List<string> KnownRootAppsPackages = new List<string>{
            "com.noshufou.android.su",
            "com.noshufou.android.su.elite",
            "eu.chainfire.supersu",
            "com.koushikdutta.superuser",
            "com.thirdparty.superuser",
            "com.yellowes.su"
        };

        /// <summary>
        /// The known dangerous apps packages.
        /// </summary>
        public static readonly List<string> KnownDangerousAppsPackages = new List<string> {
            "com.koushikdutta.rommanager",
            "com.dimonvideo.luckypatcher",
            "com.chelpus.lackypatch",
            "com.ramdroid.appquarantine"
        };

        /// <summary>
        /// The known root cloaking packages.
        /// </summary>
        public static readonly List<string> KnownRootCloakingPackages = new List<string>{
            "com.devadvance.rootcloak",
            "de.robv.android.xposed.installer",
            "com.saurik.substrate",
            "com.devadvance.rootcloakplus",
            "com.zachspong.temprootremovejb",
            "com.amphoras.hidemyroot",
            "com.formyhm.hideroot"
        };


        /// <summary>
        /// The su paths.
        /// </summary>
        public static readonly List<string> SuPaths = new List<string>{
            "/data/local/",
            "/data/local/bin/",
            "/data/local/xbin/",
            "/sbin/",
            "/system/bin/",
            "/system/bin/.ext/",
            "/system/bin/failsafe/",
            "/system/sd/xbin/",
            "/system/usr/we-need-root/",
            "/system/xbin/"
        };

        /// <summary>
        /// The paths that should not be writeable.
        /// </summary>
        public static readonly List<string> PathsThatShouldNotBeWriteable = new List<string>{
            "/system",
            "/system/bin",
            "/system/sbin",
            "/system/xbin",
            "/vendor/bin",
            "/sbin",
            "/etc"
        };
    }
}