using Excalibur.Cross.ViewModels;
using Foundation;
using MvvmCross.iOS.Views;
using UIKit;

namespace Excalibur.Tests.Cross.iOS.Views
{
    public class BaseViewController<TViewModel> : MvxViewController where TViewModel : BaseViewModel
    {
        public BaseViewController() { }
        protected BaseViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
        }

        #region Fields

        protected bool NavigationBarEnabled = false;

        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        #endregion

        #region Public Methods

        public override void ViewDidLoad()
        {
            EdgesForExtendedLayout = UIRectEdge.None;
            View.BackgroundColor = UIColor.White;

            base.ViewDidLoad();
        }

        #endregion
    }
}