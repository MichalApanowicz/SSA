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
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Activities.MainActivityFragments;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities
{
    [Activity(Label = "ListDetailsActivity", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class ListDetailsActivity : ListActivity
    {
        private readonly ListRepository _listRepository =
            new ListRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private readonly ItemRepository _itemRepository =
            new ItemRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private readonly ItemStatusRepository _itemStatusRepository =
            new ItemStatusRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private readonly ListStatusRepository _listStatusRepository =
            new ListStatusRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private ListModel _list;
        private List<ItemModel> _items;

        private Toolbar _toolbar;
        //private TextView _person, _createDate;
        private ArrayAdapter _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListDetailsActivity);
            var text = Intent.GetStringExtra("List");
            _list = JsonConvert.DeserializeObject<ListModel>(text) ?? new ListModel();
            _items = _list.Items;


            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _toolbar.Title = _list.Name;
            _toolbar.InflateMenu(Resource.Menu.listDetails_top_menu);
            SetActionBar(_toolbar);

            //_person = FindViewById<TextView>(Resource.Id.ListPersonText);
            //_createDate = FindViewById<TextView>(Resource.Id.ListDateText);

            //_person.Text = _list.Person;
            //_createDate.Text = DateTime.Parse(_list.CreateDate).ToLongDateString();

            _adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked,
                objects: _items.Select(x => x.Name).ToArray());

            ListAdapter = _adapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.listDetails_top_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_commitList:
                    CommitList();
                    Toast.MakeText(this, $"Zatwierdzono '{_list.Name}'",
                        ToastLength.Short).Show();
                    return true;

                case Resource.Id.menu_terminateList:
                    Toast.MakeText(this, $"Action selected: {item.TitleFormatted}",
                        ToastLength.Short).Show();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void CommitList()
        {
            _listRepository.Delete(_list.ListId);
            _list.Status = _listStatusRepository.Get(ListStatusEnum.Committed);
            _listRepository.Save(_list);
            foreach (var i in _items)
            {
                var x = _itemRepository.Delete(i.ItemId);
                Log.Debug($"CommitList _itemRepository.Delete({i.ItemId})", x.ToString());
                i.Status = _itemStatusRepository.Get(ItemStatusEnum.Reserved);
                _itemRepository.Save(i);
            }
        }
    }
}