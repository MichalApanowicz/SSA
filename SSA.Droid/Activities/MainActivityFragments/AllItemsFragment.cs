using System;
using System.Collections.Generic;
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
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Adapters;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities.MainActivityFragments
{
    public class AllItemsFragment : Android.Support.V4.App.ListFragment
    {
        private MainRepository _repository;
        private static List<ItemModel> _items;
        private static List<ItemModel> _selectedItems;
        private AllItemsAdapter _adapter;
        private static AllItemsFragment _instance;

        private AllItemsFragment() { }

        public static AllItemsFragment NewInstance(MainRepository repository)
        {
            return _instance ?? (_instance = new AllItemsFragment
            {
                _repository = repository
            });
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AllItems, null);
            var lv = view.FindViewById<ListView>(Android.Resource.Id.List);
            _selectedItems = new List<ItemModel>();

            _items = _repository.GetAllItemsWithCildren();

            _adapter = new AllItemsAdapter(Activity, _items);
            ListAdapter = _adapter;
            lv.ChoiceMode = ChoiceMode.None;

            return view;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            Log.Debug("Fragment", $"_selectedItems[{_selectedItems.Count}]: {_selectedItems.ToArray().ToString()}");
        }

        public List<ItemModel> GetSelectedItems()
        {
            _selectedItems.Clear();

            var adapter = ((AllItemsAdapter) ListAdapter) ?? _adapter;
            var selected = adapter.GetSelectedRows();
            foreach (var i in selected)
            {
                _selectedItems.Add(_items.First(x => x.ItemId == i));
            }
            _adapter.NotifyDataSetChanged();
            Log.Debug("Fragment", $"_selectedItems[{_selectedItems.Count}]: {JsonConvert.SerializeObject(_selectedItems, Formatting.Indented)}");
            
            return _selectedItems;
        }

        public void UpdateItems()
        {
            _items = _repository.GetAllItemsWithCildren();
            _adapter = new AllItemsAdapter(Activity, _items);
            _adapter.NotifyDataSetChanged();
        }
    }
}