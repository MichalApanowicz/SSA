using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SSA.Droid.Adapters;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities.MainActivityFragments
{
    public class AllItemsFragment : Android.Support.V4.App.ListFragment
    {
        private static List<ItemModel> _items;
        public List<ItemModel> SelectedItems;
        private AllItemsAdapter _adapter;
        private List<int> _selectedIds;

        private AllItemsFragment() { }

        public static AllItemsFragment NewInstance()
        {
            var fragment = new AllItemsFragment();
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _items = DataProvider.GetItems();
            _adapter = new AllItemsAdapter(Activity, _items);
            ListAdapter = _adapter;
        }

        public override void OnResume()
        {
            base.OnResume();
            UpdateItems();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AllItems, null);
            var lv = view.FindViewById<ListView>(Android.Resource.Id.List);
            SelectedItems = new List<ItemModel>();

            _items = DataProvider.GetItems();
            _adapter = new AllItemsAdapter(Activity, _items, _selectedIds);
            ListAdapter = _adapter;
            lv.ChoiceMode = ChoiceMode.None;

            return view;
        }


        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            SelectedItems.Add(DataProvider.GetItemFromLocal((int)id));
            Log.Debug("Fragment", $"_selectedItems[{SelectedItems.Count}]: {SelectedItems.ToArray()}");
        }

        public List<ItemModel> GetSelectedItems()
        {
            SelectedItems.Clear();

            var adapter = ((AllItemsAdapter)ListAdapter) ?? _adapter;
            _selectedIds = adapter.GetSelectedRows();
            foreach (var i in _selectedIds)
            {
                SelectedItems.Add(_items.First(x => x.ItemId == i));
            }
            Activity.RunOnUiThread(() => _adapter.NotifyDataSetChanged());
            Log.Debug("Fragment", $"_selectedItems[{SelectedItems.Count}]: {JsonConvert.SerializeObject(SelectedItems, Formatting.Indented)}");

            return SelectedItems;
        }

        public void UpdateItems()
        {
            _selectedIds = _adapter?.GetSelectedRows();
            _items = DataProvider.GetItems();
            _adapter = new AllItemsAdapter(Activity, _items, _selectedIds);
            _adapter.NotifyDataSetChanged();
            ListAdapter = _adapter;
        }
    }
}