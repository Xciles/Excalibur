using Android.OS;
using Android.Views;
using Excalibur.Tests.Cross.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Excalibur.Tests.Cross.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class CurrentUserFragment : BaseFragment<CurrentUserViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ShowHamburgerMenu = true;
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        protected override int FragmentId => Resource.Layout.fragment_current_user;
    }
}