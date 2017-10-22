using Android.OS;
using Android.Runtime;
using Android.Views;
using Excalibur.Tests.Cross.Core.ViewModels;
using MvvmCross.Droid.Views.Attributes;

namespace Excalibur.Tests.Cross.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class UserDetailsFragment : BaseFragment<UserDetailViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ShowHamburgerMenu = true;
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        protected override int FragmentId => Resource.Layout.fragment_user_details;
    }
}