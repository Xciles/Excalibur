using Windows.UI.Xaml;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels.Menu;

namespace Excalibur.Tests.Encrypted.Cross.Uwp.Controls
{
    public class TestDataTemplateSelector : Windows.UI.Xaml.Controls.DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate UserItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is CurrentUserMenuItem)
            {
                return UserItemTemplate;
            }

            return DefaultTemplate;
        }
    }

    public class MenuDataTemplateSelector : Windows.UI.Xaml.Controls.DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate UserItemTemplate { get; set; }
        public DataTemplate HeaderItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is CurrentUserMenuItem)
            {
                return UserItemTemplate;
            }
            else if (item is HeaderMenuItem)
            {
                return HeaderItemTemplate;
            }

            return DefaultTemplate;
        }
    }

    public class MenuContainerStyleSelector : Windows.UI.Xaml.Controls.StyleSelector
    {
        public Style DefaultStyle { get; set; }
        public Style UserItemStyle { get; set; }
        public Style HeaderItemStyle { get; set; }

        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            if (item is CurrentUserMenuItem)
            {
                return UserItemStyle;
            }
            else if (item is HeaderMenuItem)
            {
                return HeaderItemStyle;
            }

            return DefaultStyle;
        }
    }
}