// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;

namespace Excalibur.Tests.FormsCross.Droid
{
    [Activity(
        Label = "Excalibur Forms"
        , MainLauncher = true
        , Icon = "@drawable/icon_square"
        , Theme = "@style/MainTheme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxFormsSplashScreenActivity<Setup, Core.App, Core.Ui.App>
    {
        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(AndroidEntryActivity));
            return Task.CompletedTask;
        }
    }
}
