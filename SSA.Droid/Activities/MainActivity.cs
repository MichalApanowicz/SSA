﻿using System;
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
using SSA.Droid.Activities.MainActivityFragments;
using SSA.Droid.Models;
using SSA.Droid.Repositories;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Newtonsoft.Json;
using SQLite;
using SSA.Droid.Adapters;
using AlertDialog = Android.Support.V7.App.AlertDialog;


namespace SSA.Droid
{

    [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : FragmentActivity
    {
        private readonly MainRepository _repository =
            new MainRepository(new SQLiteConnection(Constants.DatabasePath));

        private Android.Support.V4.App.Fragment[] _fragments;

        private ViewPager _viewPager;
        private Toolbar _toolbar;
        private ProgressBar _headerProgress;
        private LinearLayout _mainContent;

        private string[] _tabNames;
        private PersonModel _loggedUser;

        public override void OnBackPressed()
        {


            AlertDialog.Builder builder2 = new AlertDialog.Builder(this);
            builder2.SetTitle("Wyjście");
            builder2.SetMessage("Na pewno chcesz opuścić aplikację?");
            builder2.SetPositiveButton("Tak", (s, e) => { base.OnBackPressed(); });
            builder2.SetNegativeButton("Nie", (s, e) => { });
            builder2.Show();
        }

        protected override void OnResume()
        {
            base.OnResume();

            //RefreshData();
            //var currentItem = _viewPager.CurrentItem;

            //_viewPager = FindViewById<ViewPager>(Resource.Id.mainviewpager);
            //_viewPager.Adapter =
            //    new MainActivityFragmentAdapter(SupportFragmentManager, _fragments, _tabNames);

            //_viewPager.SetCurrentItem(currentItem, false);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            SampleData.DropData();
            SampleData.AddData();


            _fragments = new Android.Support.V4.App.Fragment[]
            {
                AllListsFragment.NewInstance(),
                AllItemsFragment.NewInstance(),
                SettingsFragment.NewInstance(_repository)
            };

            _tabNames = new[]
            {
                "Listy",
                "Wszystkie przedmioty",
                "Ustawienia"
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
                    if (!Configuration.Online)
                    {
                        DialogWithAskForConnect();
                        break;
                    }
                    RefreshData();
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }

            return base.OnOptionsItemSelected(item);
        }

        private bool UserOk()
        {
            if (_loggedUser == null)
            {
                Toast.MakeText(this,
                        "Aplikacja nie jest w stanie zweryfikować użytkownika. Udstępnij aplikacji kontakty", ToastLength.Long)
                    .Show();
                return false;
            }
            return true;
        }

        private async void CreateNewList()
        {
            if (!UserOk()) return;

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
                    RunOnUiThread(() =>
                    {
                        View dialogView = LayoutInflater.Inflate(Resource.Layout.NewListDialog, null);
                        EditText name = dialogView.FindViewById<EditText>(Resource.Id.editNameNewList);
                        EditText description = dialogView.FindViewById<EditText>(Resource.Id.editDescNewList);
                        AlertDialog.Builder builder = new AlertDialog.Builder(this);
                        builder.SetTitle("Nowa lista");
                        builder.SetView(dialogView);
                        builder.SetPositiveButton("Zapisz", (s, e) =>
                        {
                            var list = new ListModel()
                            {
                                Name = name.Text,
                                Description = description.Text,
                                ListStatusId = 1,
                                Status = _repository.GetListStatus(ListStatusEnum.Uncommitted),
                                Items = selectedItems,
                                PersonId = _loggedUser.PersonId,
                                Person = _loggedUser,
                                CreateDate = DateTime.Now.ToLongDateString()
                            };
                            DataProvider.AddNewListLocal(list);
                            Toast.MakeText(this, $"Utworzono listę z {selectedItems.Count} przedmiotami",
                                ToastLength.Short).Show();
                            RefreshData();
                        });
                        builder.SetNegativeButton("Anuluj", (s, e) =>
                        {

                        });
                        builder.Show();
                    });
                }
                catch (Exception e)
                {
                    Log.Debug("MainActivity", e.ToString());
                    Toast.MakeText(this, $"Wystąpił problem podczas dodawania listy! Zgłoś problem administratorowi.",
                        ToastLength.Short).Show();
                }
            });
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



            try
            {

                await Task.Run(() =>
                {
                    try
                    {
                        ((AllListsFragment)_fragments[0]).UpdateLists();

                        ((AllItemsFragment)_fragments[1]).UpdateItems();
                    }
                    catch (Exception ex)
                    {
                        ShowAlertWithException(ex);
                    }
                    RunOnUiThread(() =>
                    {
                        _mainContent.Visibility = ViewStates.Visible;
                        _headerProgress.Visibility = ViewStates.Gone;
                    });
                });
            }
            catch (Exception ex)
            {
                ShowAlertWithException(ex);
            }
        }

        void ShowAlertWithException(Exception ex)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Wystąpił błąd");
            builder.SetMessage("");
            builder.SetPositiveButton("Ok", (s, e) => { });
            builder.SetNegativeButton("Szczegóły", (s2, e2) =>
            {
                AlertDialog.Builder builder2 = new AlertDialog.Builder(this);
                builder2.SetTitle(ex.GetType().Name);
                builder2.SetMessage(ex.ToString());
                builder2.SetNegativeButton("Ok", (s, e) =>
                {
                    RunOnUiThread(() =>
                    {
                        _mainContent.Visibility = ViewStates.Visible;
                        _headerProgress.Visibility = ViewStates.Gone;
                    });
                });
                builder2.Show();
            });
            builder.Show();
        }

        public void DialogWithAskForConnect()
        {
            if (Configuration.Online) return;

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Uwaga!");
            builder.SetMessage(
                "Możesz wykonać tę akcję tylko gdy jesteś połączony z siecią magazynu. \nCzy zaznaczyć, że jesteś połączony?");
            builder.SetPositiveButton("Tak", (s, e) => { Configuration.Online = true; });
            builder.SetNegativeButton("Anuluj", (s, e) => { });
            builder.Show();
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
                SaveUser();
                return;
            }

            Snackbar.Make(_mainContent, "Aplikacja potrzebuje pewnych uprawnień w celu identyfikacji użytkownika.", Snackbar.LengthIndefinite)
                .SetAction("OK", v =>
                {
                    RequestPermissions(PermissionsLocation, RequestLocationId);
                })
                .Show();
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
                            SaveUser();
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
            return "brak nazwy";
        }

        private void SaveUser()
        {
            var name = GetUserName();
            var person = DataProvider.GetPersonLocal(name) ??
                DataProvider.SavePersonLocal(new PersonModel
                {
                    Name = name,
                    Description = "Nowy uzytkownik",
                });
            _loggedUser = person;
            _toolbar.Title = _loggedUser.Name;
            SetActionBar(_toolbar);
        }
    }
}
