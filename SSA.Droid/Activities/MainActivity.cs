using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Nfc;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
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
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            SampleData.DropData();
            SampleData.AddData();


            _fragments = new Android.Support.V4.App.Fragment[]
            {
                AllListsFragment.NewInstance(_repository),
                AllItemsFragment.NewInstance(_repository),
                SettingsFragment.NewInstance(_repository),
                TestFragment.NewInstance(_repository),
            };

            _tabNames = new[]
            {
                "Listy",
                "Wszystkie przedmioty",
                "Ustawienia",
                "Test"
            };

            _viewPager = FindViewById<ViewPager>(Resource.Id.mainviewpager);
            _viewPager.Adapter =
                new MainActivityFragmentAdapter(SupportFragmentManager, _fragments, _tabNames);



            _headerProgress = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            _mainContent = FindViewById<LinearLayout>(Resource.Id.main_content);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _toolbar.InflateMenu(Resource.Menu.top_menu);
            SetActionBar(_toolbar);

            CheckPremissionAndTryGetUserName();
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
            if (_loggedUser == null)
            {
                Toast.MakeText(this,
                    "Aplikacja nie jest w stanie zweryfikować użytkownika. Udstępnij aplikacji kontakty", ToastLength.Long)
                    .Show();
                return;
            }
            RunOnUiThread(() =>
            {
                _mainContent.Visibility = ViewStates.Gone;
                _headerProgress.Visibility = ViewStates.Visible;
            });
            var selectedItems = ((AllItemsFragment)_fragments[1]).GetSelectedItems();
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    if (selectedItems.Count == 0)
                    {
                        Toast.MakeText(this, "Zaznacz przedmioty do dodania",
                            ToastLength.Short).Show();
                    }
                    else
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


                        var url = Constants.ApiPath + "new/list";
                        var request = (HttpWebRequest)WebRequest.Create(url);
                        request.ContentType = "application/json";
                        request.Method = "POST";

                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            string json = JsonConvert.SerializeObject(list);

                            streamWriter.Write(json);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }

                        var response = (HttpWebResponse)request.GetResponse();
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            _repository.Save<ListModel>(list);
                        }
                        Toast.MakeText(this, $"Utworzono listę z {selectedItems.Count} przedmiotami",
                            ToastLength.Short).Show();
                    }
                }
                catch (Exception ex)
                {

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
                    try
                    {
                        list.Person = _repository.GetPerson(1);
                        list.Status = _repository.GetListStatus(1);
                    }
                    catch
                    {
                        list.Person = new PersonModel();
                        list.Status = new ListStatus();
                    }
                    _repository.Save(list);
                }
                var x = _repository.GetAllLists();
            }
            catch (Exception ex)
            {
                Log.Debug("ApiCall", $"Exception: {ex}");
            }
        }


        readonly string[] PermissionsLocation =
        {
            Manifest.Permission.ReadContacts,
            Manifest.Permission.WriteContacts,
            Manifest.Permission.ReadProfile,
            Manifest.Permission.WriteProfile
        };

        const int RequestLocationId = 0;

        private void CheckPremissionAndTryGetUserName()
        {
            const string permission = Manifest.Permission.ReadContacts;
            if (CheckSelfPermission(permission) == (int)Permission.Granted)
            {
                SaveUserNameToDatabase();
                return;
            }

            if (true)
            {
                //Explain to the user why we need to read the contacts
                Snackbar.Make(_mainContent, "Aplikacja potrzebuje pewnych uprawnień w celu identyfikacji użytkownika.", Snackbar.LengthIndefinite)
                    .SetAction("OK", v =>
                    {
                        RequestPermissions(PermissionsLocation, RequestLocationId);
                    })
                    .Show();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            Snackbar.Make(_mainContent, "Kontakty są dostępne. Pobieram użytkownika", Snackbar.LengthShort)
                                    .Show();
                            SaveUserNameToDatabase();
                        }
                        else
                        {
                            Snackbar.Make(_mainContent, "Kontakty nie są dostęne. Nie można pobrać użytkownika", Snackbar.LengthShort)
                                    .Show();

                        }
                    }
                    break;
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
            _toolbar.Title = _loggedUser.Name;
            SetActionBar(_toolbar);
        }
    }
}
