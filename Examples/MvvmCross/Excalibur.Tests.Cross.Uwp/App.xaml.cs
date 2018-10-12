using MvvmCross.Platforms.Uap.Views;

namespace Excalibur.Tests.Cross.Uwp
{
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }
    }

    // Fix for plugin register
    public abstract class ExUwpApp : MvxApplication<Setup, Core.App>
    {

    }
}
