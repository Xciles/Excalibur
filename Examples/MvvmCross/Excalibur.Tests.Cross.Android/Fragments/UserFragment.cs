using Android.OS;
using Android.Runtime;
using Android.Views;
using Excalibur.Tests.Cross.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace Excalibur.Tests.Cross.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("Excalibur.Tests.Cross.Droid.Fragments.UserFragment")]
    public class UserFragment : BaseFragment<UserViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ShowHamburgerMenu = true;
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        protected override int FragmentId => Resource.Layout.fragment_user;
    }
}