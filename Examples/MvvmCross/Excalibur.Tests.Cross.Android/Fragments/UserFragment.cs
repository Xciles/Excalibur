using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Excalibur.Tests.Cross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views.Attributes;

namespace Excalibur.Tests.Cross.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class UserFragment : BaseFragment<UserViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ShowHamburgerMenu = true;
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_user);
            if (recyclerView != null)
            {
                recyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(Activity);
                recyclerView.SetLayoutManager(layoutManager);
            }

            // todo This is here just for show.
            // if you want to respond to something you can
            //_itemSelectedToken = ViewModel.SelectedObservable.WeakSubscribe(() => ViewModel.SelectedObservable.Email,
            //    (sender, args) =>
            //    {
            //        if (ViewModel.SelectedObservable != null)
            //            Toast.MakeText(Activity,
            //                string.Format("Selected: {0}", ViewModel.SelectedObservable.Email),
            //                ToastLength.Short).Show();
            //    });

            var swipeToRefresh = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.recycler_user_refresh);
            var appBar = Activity.FindViewById<AppBarLayout>(Resource.Id.appbar);
            if (appBar != null)
                appBar.OffsetChanged += (sender, args) => swipeToRefresh.Enabled = args.VerticalOffset == 0;

            return view;
        }

        protected override int FragmentId => Resource.Layout.fragment_user;
    }
}