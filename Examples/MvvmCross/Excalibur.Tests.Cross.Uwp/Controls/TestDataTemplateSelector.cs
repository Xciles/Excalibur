using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Excalibur.Tests.Cross.Uwp.Controls
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
}