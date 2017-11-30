using System;
using System.Collections.Generic;
using Android.App;
using Android.Nfc;
using Android.Widget;
using Android.OS;
using Android.Util;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Activities.MainActivityFragments;
using SSA.Droid.Models;
using SSA.Droid.Repositories;
using Android.Support.V4.View;
using Android.Support.V4.App;
using SSA.Droid.Adapters;


namespace SSA.Droid
{
    [Activity(Label = "SSA.Droid", MainLauncher = true)]
    public class MainActivity : FragmentActivity
    {
        Android.Support.V4.App.Fragment[] _fragments;
        private ListRepository _listRepository = new ListRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        private ItemRepository _itemRepository = new ItemRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
          
            SetContentView(Resource.Layout.Main);

            _fragments = new Android.Support.V4.App.Fragment[]
            {
                AllListsFragment.NewInstance(_listRepository),
                AllItemsFragment.NewInstance(_itemRepository),
                TestFragment.NewInstance(_listRepository, _itemRepository),
            };
            

            MainActivityFragmentAdapter adapter = new MainActivityFragmentAdapter(SupportFragmentManager, _fragments);

            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.mainviewpager);
            viewPager.Adapter = adapter;
        }
    }
}

