﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Interop;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Activities.MainActivityFragments;
using SSA.Droid.Adapters;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities
{
    [Activity(Label = "ListDetailsActivity", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class ListDetailsActivity : ListActivity
    {
        private readonly MainRepository _repository =
            new MainRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private ListModel _list;
        private List<ItemModel> _items;

        private EditText _eanCodeText;
        private RadioButton _getItemRadioButton, _deleteItemRadioButton;
        private Toolbar _toolbar;
  
        private ArrayAdapter _adapter;

        private List<ItemModel> _selectedItems;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListDetailsActivity);
            var text = Intent.GetStringExtra("List");
            _list = JsonConvert.DeserializeObject<ListModel>(text) ?? new ListModel();
            _items = _repository.GetItemsFromList(_list.ListId);

            _getItemRadioButton = FindViewById<RadioButton>(Resource.Id.getItemRadioButton);
            _deleteItemRadioButton = FindViewById<RadioButton>(Resource.Id.deleteItemRadioButton);
            _eanCodeText = FindViewById<EditText>(Resource.Id.eanCodeEditText);
            _eanCodeText.TextChanged += EanTextChanged;

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _toolbar.Title = _list.Name;
            _toolbar.InflateMenu(Resource.Menu.listDetails_top_menu);
            SetActionBar(_toolbar);

            var selected = new List<int>();
            foreach (var item in _items)
            {
                if (item.Status.ItemStatusId == (int)ItemStatusEnum.Unavailable)
                {
                    selected.Add(item.ItemId);
                }
            }
            ListAdapter = new AllItemsAdapter(this, _items, selected);
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

            foreach (var item in _items)
            {
                if (item.Status.ItemStatusId == (int)ItemStatusEnum.Available)
                {
                    item.Status = _repository.GetItemStatus(ItemStatusEnum.Reserved);
                    _repository.Update(item);
                }
            }

            _list.Status = _repository.GetListStatus(ListStatusEnum.Committed);
            _list.Items = _items;
            _repository.Update(_list);

            UpdateItemList();
        }

        private void UpdateItemList()
        {
            _items = _repository.GetItemsFromList(_list.ListId);
            ListAdapter = new AllItemsAdapter(this, _items);
            ((BaseAdapter)ListAdapter).NotifyDataSetChanged();
        }

        private List<ItemModel> GetSelectedItems()
        {
            _selectedItems.Clear();

            var selected = ((AllItemsAdapter)ListAdapter).GetSelectedRows();
            foreach (var i in selected)
            {
                _selectedItems.Add(_items.First(x => x.ItemId == i));
            }
            _adapter.NotifyDataSetChanged();
            Log.Debug("ListDetailsActivity", $"_selectedItems[{_selectedItems.Count}]: {JsonConvert.SerializeObject(_selectedItems, Formatting.Indented)}");

            return _selectedItems;
        }

        private void EanTextChanged(object s, TextChangedEventArgs e)
        {
            try
            {
                var typedEan = _eanCodeText.Text;
                if (typedEan.Length == 8)
                {
                    var item = _repository.GetItemByEanCode(typedEan);
                    if (_getItemRadioButton.Checked)
                    {
                        var actionString = "";
                        if (_items.Select(x => x.ItemId).Contains(item.ItemId))
                        {
                            item.Status = _repository.GetItemStatus(ItemStatusEnum.Unavailable);
                            actionString = "Pobrano: ";
                        }
                        else
                        {
                            item.ListId = _list.ListId;
                            actionString = "Dodano: ";
                        }
                        _repository.Update(item);
                        Toast.MakeText(this, actionString + item.Name, ToastLength.Long).Show();
                    }
                    else if (_deleteItemRadioButton.Checked)
                    {
                        item.ListId = 0;
                        _repository.Update(item);
                        Toast.MakeText(this, "Usunięto: " + item.Name, ToastLength.Long).Show();
                    }
                    _eanCodeText.Text = "";
                    UpdateItemList();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Nie znleziono przedmotu o tym kodzie", ToastLength.Long).Show();
            }
        }
    }
}