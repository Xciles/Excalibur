using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Logging;

namespace Excalibur.Tests.FormsCross.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, Core.Ui.App>
    {
        public override MvxLogProviderType GetDefaultLogProviderType() => MvxLogProviderType.Console;
    }
}
