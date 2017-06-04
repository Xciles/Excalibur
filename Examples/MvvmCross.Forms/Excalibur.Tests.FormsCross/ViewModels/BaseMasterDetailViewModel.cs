using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Excalibur.Cross.Language;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;

namespace Excalibur.Tests.FormsCross.ViewModels
{
    public class BaseMasterDetailViewModel : MvxMasterDetailViewModel
    {
        protected IMvxNavigationService NavigationService { get; } = Mvx.Resolve<IMvxNavigationService>();

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
