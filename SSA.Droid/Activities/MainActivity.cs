using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Json;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
using Android.App;
using Android.Content.PM;
using Android.Nfc;
using Android.Widget;
using Android.OS;
<<<<<<< HEAD
using Android.Provider;
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
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

<<<<<<< HEAD
    [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
=======
    [Activity(MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
    public class MainActivity : FragmentActivity
    {
        private readonly MainRepository _repository =
            new MainRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
<<<<<<< HEAD

=======
        
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        private Android.Support.V4.App.Fragment[] _fragments;

        private ViewPager _viewPager;
        private Toolbar _toolbar;
<<<<<<< HEAD
        private ProgressBar _headerProgress;
        private LinearLayout _mainContent;

        private string[] _tabNames;
        private PersonModel _loggedUser;


        protected override void OnResume()
        {
            base.OnResume();

            Log.Debug("Database", Constants.DatabasePath);
            ((AllListsFragment)_fragments[0]).UpdateLists();
            ((AllItemsFragment)_fragments[1]).UpdateItems();
            var currentItem = _viewPager.CurrentItem;

            _viewPager = FindViewById<ViewPager>(Resource.Id.mainviewpager);
            _viewPager.Adapter =
                new MainActivityFragmentAdapter(SupportFragmentManager, _fragments, _tabNames);

            _viewPager.SetCurrentItem(currentItem, false);
=======
        private string[] _tabNames;

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
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            SampleData.DropData();
            SampleData.AddData();
<<<<<<< HEAD
            SaveUserNameToDatabase();
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681

            _fragments = new Android.Support.V4.App.Fragment[]
            {
                AllListsFragment.NewInstance(_repository),
                AllItemsFragment.NewInstance(_repository),
<<<<<<< HEAD
                SettingsFragment.NewInstance(_repository),
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
                TestFragment.NewInstance(_repository),
            };

            _tabNames = new[]
            {
                "Listy",
                "Wszystkie przedmioty",
<<<<<<< HEAD
                "Ustawienia",
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
                "Test"
            };

            _viewPager = FindViewById<ViewPager>(Resource.Id.mainviewpager);
            _viewPager.Adapter =
                new MainActivityFragmentAdapter(SupportFragmentManager, _fragments, _tabNames);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
<<<<<<< HEAD
            _toolbar.Title = "SSA " + _loggedUser.Name;
            _toolbar.InflateMenu(Resource.Menu.top_menu);
            SetActionBar(_toolbar);

            _headerProgress = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            _mainContent = FindViewById<LinearLayout>(Resource.Id.main_content); 
=======
            _toolbar.Title = "SSA";
            _toolbar.InflateMenu(Resource.Menu.top_menu);
            SetActionBar(_toolbar);
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
<<<<<<< HEAD
           
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
            switch (item.ItemId)
            {
                case Resource.Id.menu_createNewList:
                    CreateNewList();
<<<<<<< HEAD
                    break;
                case Resource.Id.menu_refreshData:
                    RefreshData();
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }
           
            return base.OnOptionsItemSelected(item);
        }

        private async void CreateNewList()
        {
            RunOnUiThread(() =>
            {
                _mainContent.Visibility = ViewStates.Gone;
                _headerProgress.Visibility = ViewStates.Visible;
            });
            var selectedItems = ((AllItemsFragment)_fragments[1]).GetSelectedItems();
            await Task.Factory.StartNew(() =>
            {
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
                        _repository.Save<ListModel>(list);

                       // Log.Debug("MainActivity",
                          //  $"menu_createNewList: {JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Serialize})}");

                        Toast.MakeText(this, $"Utworzono listę z {selectedItems.Count} przedmiotami",
                            ToastLength.Short).Show();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("MainActivity", $"{ex}");
                    }
                    
                }
            });
            OnResume();
            RunOnUiThread(() =>
            {
                _mainContent.Visibility = ViewStates.Visible;
                _headerProgress.Visibility = ViewStates.Gone;
            });
        }

        private async void RefreshData()
        {
            RunOnUiThread(() =>
            {
                _mainContent.Visibility = ViewStates.Gone;
                _headerProgress.Visibility = ViewStates.Visible;
            });
            await Task.Factory.StartNew(RefreshItems);
            await Task.Factory.StartNew(RefreshLists);
            OnResume();
            RunOnUiThread(() =>
            {
                _mainContent.Visibility = ViewStates.Visible;
                _headerProgress.Visibility = ViewStates.Gone;
            });
        }

        private void RefreshItems()
        {
            try
            {
                var url = Constants.ApiPath + "items";
                var json = "";

                var request = (HttpWebRequest)WebRequest.Create(new Uri(url));

                request.Method = "GET";
                Log.Debug("ApiCall", $"Request: {request}");
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        json = JsonValue.Load(stream).ToString();

                        Log.Debug("ApiCall", $"Response: {json}");
                    }
                }
                _repository.DeleteAll<ItemModel>();
                var items = JsonConvert.DeserializeObject<List<ItemModel>>(json);

                foreach (var item in items)
                {
                    item.Localization = _repository.GetLocalization(item.LocalizationId);
                    item.Category = _repository.GetCategory(item.CategoryId);
                    item.Status = _repository.GetItemStatus(item.ItemStatusId);
                    _repository.Save(item);
                }
            }
            catch (Exception ex)
            {
                Log.Debug("ApiCall", $"Exception: {ex}");
            }
        }

        private void RefreshLists()
        {
            try
            {
                var url = Constants.ApiPath + "lists";
                var json = "";

                var request = (HttpWebRequest)WebRequest.Create(new Uri(url));

                request.Method = "GET";
                Log.Debug("ApiCall", $"Request: {request}");
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        json = JsonValue.Load(stream).ToString();

                        Log.Debug("ApiCall", $"Response: {json}");
                    }
                }
                _repository.DeleteAll<ListModel>();
                var lists = JsonConvert.DeserializeObject<List<ListModel>>(json);

                foreach (var list in lists)
                {
                    list.Person = _repository.GetPerson(list.PersonId);
                    list.Status = _repository.GetListStatus(list.ListStatusId);
                    _repository.Save(list);
                }
            }
            catch (Exception ex)
            {
                Log.Debug("ApiCall", $"Exception: {ex}");
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
                    //Lists = new List<ListModel>()
                };
                _repository.Save(person);
            }
            _loggedUser = person;
=======
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
                        Status = _repository.GetListStatus(ListStatusEnum.Uncommitted),
                        Items = selectedItems,
                        Person = "Michał Apanowicz",
                        CreateDate = DateTime.Now.ToLongDateString()
                    };
                    var result = _repository.Save<ListModel>(list);

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
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        }
    }
}
