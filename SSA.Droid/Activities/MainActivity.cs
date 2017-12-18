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
using Newtonsoft.Json;
using SSA.Droid.Adapters;


namespace SSA.Droid
{

    [Activity(MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : FragmentActivity
    {
        private readonly ListRepository _listRepository =
            new ListRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private readonly ItemRepository _itemRepository =
            new ItemRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private readonly ItemStatusRepository _itemStatusRepository =
            new ItemStatusRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private readonly ListStatusRepository _listStatusRepository =
            new ListStatusRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private Android.Support.V4.App.Fragment[] _fragments;

        private ViewPager _viewPager;
        private Toolbar _toolbar;
        private string[] _tabNames;

        protected override void OnResume()
        {
            Toast.MakeText(this, $"OnResume",
                ToastLength.Long).Show();
            _fragments = new Android.Support.V4.App.Fragment[]
            {
                AllListsFragment.NewInstance(_listRepository),
                AllItemsFragment.NewInstance(_itemRepository),
                TestFragment.NewInstance(_listRepository, _itemRepository, _itemStatusRepository,
                    _listStatusRepository),
            };
            var currentItem = _viewPager.CurrentItem;
            
            _viewPager = FindViewById<ViewPager>(Resource.Id.mainviewpager);
            _viewPager.Adapter =
                new MainActivityFragmentAdapter(SupportFragmentManager, _fragments, _tabNames);
            base.OnResume();
            _viewPager.SetCurrentItem(currentItem, false);
        }

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
                TestFragment.NewInstance(_listRepository, _itemRepository, _itemStatusRepository,
                    _listStatusRepository),
            };

            _tabNames = new[]
            {
                "Listy",
                "Wszystkie przedmioty",
                "Test"
            };

            _viewPager = FindViewById<ViewPager>(Resource.Id.mainviewpager);
            _viewPager.Adapter =
                new MainActivityFragmentAdapter(SupportFragmentManager, _fragments, _tabNames);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _toolbar.Title = "SSA";
            _toolbar.InflateMenu(Resource.Menu.top_menu);
            SetActionBar(_toolbar);
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
                    CreateNewList();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void CreateNewList()
        {
            var selectedItems = ((AllItemsFragment) _fragments[1]).GetSelectedItems();
            if (selectedItems.Count == 0)
            {
                Toast.MakeText(this, "Zaznacz przedmioty do dodania",
                    ToastLength.Short).Show();
            }
            else
            {
                try
                {
                    var list = new ListModel()
                    {
                        Name = "Nowa",
                        Description = "Opis",
                        ListStatusId = 1,
                        Status = _listStatusRepository.Get(ListStatusEnum.Uncommitted),
                        Items = selectedItems,
                        Person = "Michał Apanowicz",
                        CreateDate = DateTime.Now.ToLongDateString()
                    };
                    var result = _listRepository.Save(list);
                    Log.Debug("MainActivity",
                        $"menu_createNewList: {JsonConvert.SerializeObject(result, Formatting.Indented)}");

                    var x = _fragments[0] as AllListsFragment;
                    x?.UpdateLists();

                    var y =  x?.ListAdapter as AllListsAdapter; 
                    y?.NotifyDataSetChanged();
                }
                catch (Exception ex)
                {
                    Log.Error("MainActivity", $"{JsonConvert.SerializeObject(ex, Formatting.Indented)}");
                }
            }
        }
    }
}
