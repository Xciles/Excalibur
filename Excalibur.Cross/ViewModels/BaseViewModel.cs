using System.Windows.Input;
using Excalibur.Cross.Language;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;

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
}
