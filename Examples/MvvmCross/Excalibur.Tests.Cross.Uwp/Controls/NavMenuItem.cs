using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Excalibur.Shared.Presentation;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Uwp.Controls
{
    public abstract class MenuItemBase
    {
        public ICommand Command { get; set; }
        public object Parameters { get; set; }
        
    }

    /// <summary>
    /// Data to represent an item in the nav menu.
    /// </summary>
    public class NavMenuItem : MenuItemBase
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }

        public char SymbolAsChar
        {
            get { return (char) Symbol; }
        }
    }

    /// <summary>
    /// Data to represent an item in the nav menu.
    /// </summary>
    public class CurrentUserMenuItem : MenuItemBase
    {
        public CurrentUserMenuItem()
        {
            var userPresentation = Resolver.Resolve<ISinglePresentation<int, Core.Observable.LoggedInUser>>();
            CurrentUserObservable = userPresentation.SelectedObservable;
        }

        public Core.Observable.LoggedInUser CurrentUserObservable { get; set; }
    }
}