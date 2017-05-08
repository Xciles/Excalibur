using System.Windows.Input;
using Excalibur.Cross.Language;
using Excalibur.Shared.Presentation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using XLabs.Ioc;

namespace Excalibur.Cross.ViewModels
{
    /// <summary>
    ///    Defines the BaseViewModel type.
    /// </summary>
    public abstract class BaseViewModel : MvxViewModel
    {
        public IMvxLanguageBinder TextSource
        {
            get { return new MvxLanguageBinder(ExTextProvider.GeneralNamespace, GetType().Name); }
        }

        public IMvxLanguageBinder SharedTextSource
        {
            get { return new MvxLanguageBinder(ExTextProvider.GeneralNamespace, ExTextProvider.SharedNamespace); }
        }

        /// <summary>
        /// Gets the almanac go back command.
        /// </summary>
        /// <value>
        /// The almanac go back command.
        /// </value>
        public virtual ICommand GoBackCommand
        {
            get
            {
                return new MvxCommand(() => Close(this));
            }
        }
    }

    public abstract class ListViewModel : BaseViewModel
    {

        public ListViewModel()
        {
            //var presentation = Resolver.Resolve<IPresentation<TDomain, T>>();
        }



    }

    public abstract class DetailViewModel : BaseViewModel
    {

    }
}
