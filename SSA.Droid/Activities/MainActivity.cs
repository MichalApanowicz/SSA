using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
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
using Android.Support.V7.App;
using Android.Views;
using SSA.Droid.Adapters;


namespace SSA.Droid
{

    [Activity(
        MainLauncher = true)]
    public class MainActivity : FragmentActivity
    {
        private Android.Support.V4.App.Fragment[] _fragments;
        private string[] TabNames { get; set; }
        private readonly ListRepository _listRepository =
            new ListRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        private readonly ItemRepository _itemRepository =
            new ItemRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            SampleData.DropData();
            SampleData.AddData();

            _fragments = new Android.Support.V4.App.Fragment[]
            {
                AllListsFragment.NewInstance(_listRepository),
                AllItemsFragment.NewInstance(_itemRepository),
                TestFragment.NewInstance(_listRepository, _itemRepository),
            };

            TabNames = new[]
            {
                "Listy",
                "Wszystkie przedmioty",
                "Test"
            };

            var adapter = new MainActivityFragmentAdapter(SupportFragmentManager, _fragments, TabNames);

            var viewPager = FindViewById<ViewPager>(Resource.Id.mainviewpager);
            viewPager.Adapter = adapter;

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "SSA";
            toolbar.InflateMenu(Resource.Menu.top_menu);
            SetActionBar(toolbar);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_createNewList:
                    if (AllItemsFragment.SelectedItems == null) return true;
                    var x = "";
                    try
                    {
                        x += _listRepository.Save(new ListModel()
                        {
                            Name = "Nowa",
                            Description = "Opis",
                            ListStatusId = 1,
                            Items = AllItemsFragment.SelectedItems
                        }).ToString() + System.Environment.NewLine;
                    }
                    catch (Exception ex)
                    {
                        x += ex.Message;
                    }
                    
                    Log.Info("list", x.ToString());
                    return true;

                case Resource.Id.menu_save:
                    Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                        ToastLength.Short).Show();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

