using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities.MainActivityFragments
{
    public class MainActivityFragment : FragmentManager
    {
        private ListRepository _listRepository = new ListRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        private ItemRepository _itemRepository = new ItemRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        private Android.Support.V4.App.Fragment _childFragment;
        public MainActivityFragment(Android.Support.V4.App.Fragment childFragment)
        {
            _childFragment = childFragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.MainActivityFragment, null);
            var x = view.FindViewById<FrameLayout>(Resource.Id.frameLayout1);
            var fr = FragmentManager.BeginTransaction();
            fr.Add(Resource.Id.frameLayout1, _childFragment);
            fr.Commit();
            FragmentTransaction transaction = GetSupportFragmentManager().beginTransaction();

            // Replace whatever is in the fragment_container view with this fragment,
            // and add the transaction to the back stack
            transaction.replace(R.id.fragment_container, newFragment);
            transaction.addToBackStack(null);

            // Commit the transaction
            transaction.commit();

            return x;
        }
    }
}