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

        static ItemStatusRepository _itemStatusRepository = new ItemStatusRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        static ListStatusRepository _listStatusRepository = new ListStatusRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));


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
                    var selectedItems = ((AllItemsFragment)_fragments[1]).GetSelectedItems();
                    if (selectedItems.Count == 0)
                    {
                        Toast.MakeText(this, "Zaznacz przedmioty do dodania",
                            ToastLength.Short).Show();
                        return true;
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
                            Log.Debug("MainActivity", $"menu_createNewList: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
                        }
                        catch (Exception ex)
                        {
                            Log.Error("MainActivity", $"{JsonConvert.SerializeObject(ex, Formatting.Indented)}");
                        }

                        return true;
                    }


                case Resource.Id.menu_save:
                    Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                        ToastLength.Short).Show();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

