using System.Collections.Generic;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Excalibur.Tests.Cross.Core.ViewModels;
using Excalibur.Tests.Cross.Uwp.Controls;
using MvvmCross.WindowsUWP.Views;

namespace Excalibur.Tests.Cross.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
	[MvxRegion("MenuContent")]
    public sealed partial class MenuView
    {
        public MenuView()
        {
            this.InitializeComponent();
        }

        public new MenuViewModel ViewModel
        {
            get { return (MenuViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavMenuList.ItemsSource = new List<NavMenuItem>(
                new[]
                {
                    new NavMenuItem
                    {
                        Symbol = Symbol.Home,
                        Label = "Dashboard",
                        Command = ViewModel.ShowDashboardCommand
                    },
                    new NavMenuItem
                    {
                        Symbol = Symbol.AlignCenter,
                        Label = "Users",
                        Command = ViewModel.ShowUsersCommand
                    }
                });
        }

        /// <summary>
        /// Navigate to the Page for the selected <paramref name="listViewItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="listViewItem"></param>
        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem)
        {
            var item = (NavMenuItem)((NavMenuListView)sender).ItemFromContainer(listViewItem);
            item?.Command?.Execute(item.Parameters);
        }

        /// <summary>
        /// Enable accessibility on each nav menu item by setting the AutomationProperties.Name on each container
        /// using the associated Label of each item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item != null && args.Item is NavMenuItem)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((NavMenuItem)args.Item).Label);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }
    }
}
