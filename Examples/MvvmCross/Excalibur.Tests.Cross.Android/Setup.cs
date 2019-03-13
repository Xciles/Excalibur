using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Excalibur.Tests.Cross.Core;
using Excalibur.Tests.Cross.Droid.Utils;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.ViewModels;
using Serilog;

namespace Excalibur.Tests.Cross.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(global::Android.Support.Design.Widget.NavigationView).Assembly,
            typeof(global::Android.Support.Design.Widget.FloatingActionButton).Assembly,
            typeof(global::Android.Support.V7.Widget.Toolbar).Assembly,
            typeof(global::Android.Support.V4.Widget.DrawerLayout).Assembly,
            typeof(global::Android.Support.V4.View.ViewPager).Assembly,
        };

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxAppCompatViewPresenter(AndroidViewAssemblies);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);

            //add a presentation hint handler to listen for pop to root
            mvxFragmentsPresenter.AddPresentationHintHandler<MvxPanelPopToRootPresentationHint>(hint =>
            {
                var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
                var fragmentActivity = activity as global::Android.Support.V4.App.FragmentActivity;

                for (var i = 0; i < fragmentActivity.SupportFragmentManager.BackStackEntryCount; i++)
                {
                    fragmentActivity.SupportFragmentManager.PopBackStack();
                }
                return Task.FromResult(true);
            });
            //register the presentation hint to pop to root
            //picked up in the third view model
            Mvx.RegisterSingleton<MvxPresentationHint>(() => new MvxPanelPopToRootPresentationHint());
            return mvxFragmentsPresenter;
        }

        protected override IMvxLogProvider CreateLogProvider()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.AndroidLog()
                .CreateLogger();
            return base.CreateLogProvider();
        }
    }
}
