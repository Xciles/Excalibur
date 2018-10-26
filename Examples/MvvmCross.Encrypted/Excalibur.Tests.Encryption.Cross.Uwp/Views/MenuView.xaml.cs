using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels.Menu;
using Excalibur.Tests.Encrypted.Cross.Uwp.Controls;
using MvvmCross.Platforms.Uap.Presenters.Attributes;

namespace Excalibur.Tests.Encrypted.Cross.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
	[MvxRegionPresentation("MenuContent")]
    public sealed partial class MenuView
    {
        public new MenuViewModel ViewModel => (MenuViewModel)base.ViewModel;

        public MenuView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Navigate to the Page for the selected <paramref name="listViewItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="listViewItem"></param>
        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem)
        {
            var item = ((NavMenuListView)sender).ItemFromContainer(listViewItem);

            if (item is HeaderMenuItem)
            {
                return;
            }

            var menuItem = item as MenuItem;
            menuItem?.Navigate?.Execute();
        }

        /// <summary>
        /// Enable accessibility on each nav menu item by setting the AutomationProperties.Name on each container
        /// using the associated Label of each item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item != null && args.Item is MenuItem)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((MenuItem)args.Item).Title);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }
    }
}
