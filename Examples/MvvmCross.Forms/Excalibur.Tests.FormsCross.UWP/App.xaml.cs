using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.Forms.Platforms.Uap.Views;

namespace Excalibur.Tests.FormsCross.Uwp
{
    sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }
    }

    public abstract class ExApp : MvxWindowsApplication<MvxFormsWindowsSetup<Core.App, Core.Ui.App>, Core.App, Core.Ui.App, MainPage>
    {
    }
}
