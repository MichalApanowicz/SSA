using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Nfc;
using Android.Widget;
using Android.OS;
using Android.Provider;
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

    [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : FragmentActivity
    {
        private readonly MainRepository _repository =
            new MainRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        
        private Android.Support.V4.App.Fragment[] _fragments;

        private ViewPager _viewPager;
        private Toolbar _toolbar;
        private string[] _tabNames;
        private PersonModel _loggedUser;

        protected override void OnResume()
        {
            Log.Debug("Database", Constants.DatabasePath);
            _fragments = new Android.Support.V4.App.Fragment[]
            {
                AllListsFragment.NewInstance(_repository),
                AllItemsFragment.NewInstance(_repository),
                TestFragment.NewInstance(_repository),
            };
            var currentItem = _viewPager.CurrentItem;
            
            _viewPager = FindViewById<ViewPager>(Resource.Id.mainviewpager);
            _viewPager.Adapter =
                new MainActivityFragmentAdapter(SupportFragmentManager, _fragments, _tabNames);
            base.OnResume();
            _viewPager.SetCurrentItem(currentItem, false);

            ((AllItemsFragment)_fragments[1]).UpdateItems();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            
            SampleData.DropData();
            SampleData.AddData();
            SaveUserNameToDatabase();

            _fragments = new Android.Support.V4.App.Fragment[]
            {
                AllListsFragment.NewInstance(_repository),
                AllItemsFragment.NewInstance(_repository),
                TestFragment.NewInstance(_repository),
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
            _toolbar.Title = "SSA " + _loggedUser.Name;
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
            var selectedItems = ((AllItemsFragment)_fragments[1]).GetSelectedItems();
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
                        Status = _repository.GetListStatus(ListStatusEnum.Uncommitted),
                        Items = selectedItems,
                        Person = _loggedUser,
                        CreateDate = DateTime.Now.ToLongDateString()
                    };
                    var result = _repository.Save<ListModel>(list);

                    Log.Debug("MainActivity",
                        $"menu_createNewList: {JsonConvert.SerializeObject(result, Formatting.Indented)}");

                    var x = _fragments[0] as AllListsFragment;
                    x?.UpdateLists();

                    var y =  x?.ListAdapter as AllListsAdapter; 
                    y?.NotifyDataSetChanged();

                    Toast.MakeText(this, $"Utworzono listę z {selectedItems.Count} przedmiotami",
                        ToastLength.Short).Show();
                }
                catch (Exception ex)
                {
                    Log.Error("MainActivity", $"{JsonConvert.SerializeObject(ex, Formatting.Indented)}");
                }
            }
        }

        private string GetUserName()
        {
            var uri = ContactsContract.Profile.ContentUri;

            string[] projection =
            {
                ContactsContract.Contacts.InterfaceConsts.DisplayName
            };

            var cursor = ContentResolver.Query(uri, projection, null, null, null);

            if (cursor.MoveToFirst())
            {
                return (cursor.GetString(cursor.GetColumnIndex(projection[0])));

            }
            return null;
        }

        private void SaveUserNameToDatabase()
        {
            var name = GetUserName();
            var person = _repository.GetPerson(name);
            if (person == null)
            {
                person = new PersonModel
                {
                    Name = name,
                    Description = "Nowy uzytkownik",
                    Lists = new List<ListModel>()
                };
                _repository.Save(person);
            }
            _loggedUser = person;
        }
    }
}
