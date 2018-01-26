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
<<<<<<< HEAD
        public List<ItemModel> SelectedItems;
        private AllItemsAdapter _adapter;
        private List<int> _selectedIds;
=======
        private static List<ItemModel> _selectedItems;
        private AllItemsAdapter _adapter;
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681

        private AllItemsFragment() { }

        public static AllItemsFragment NewInstance(MainRepository repository)
        {
<<<<<<< HEAD
            var fragment = new AllItemsFragment { _repository = repository };
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _items = _repository.GetAllItemsWithCildren();
            _adapter = new AllItemsAdapter(Activity, _items);
            ListAdapter = _adapter;
        }

=======
            var fragment = new AllItemsFragment
            {
                _repository = repository
            };
            return fragment;
        }

>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AllItems, null);
            var lv = view.FindViewById<ListView>(Android.Resource.Id.List);
<<<<<<< HEAD
            SelectedItems = new List<ItemModel>();

            _items = _repository.GetAllItemsWithCildren();

            _adapter = new AllItemsAdapter(Activity, _items, _selectedIds);
=======
            _selectedItems = new List<ItemModel>();

            _items = _repository.GetAllItemsWithCildren();

            _adapter = new AllItemsAdapter(Activity, _items);
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
            ListAdapter = _adapter;
            lv.ChoiceMode = ChoiceMode.None;

            return view;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
<<<<<<< HEAD
            SelectedItems.Add(_repository.GetItem((int)id));
            Log.Debug("Fragment", $"_selectedItems[{SelectedItems.Count}]: {SelectedItems.ToArray()}");
=======
            //Log.Debug("Fragment", $"_selectedItems[{_selectedItems.Count}]: {_selectedItems.ToArray().ToString()}");
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        }

        public List<ItemModel> GetSelectedItems()
        {
<<<<<<< HEAD
            SelectedItems.Clear();

            var adapter = ((AllItemsAdapter) ListAdapter) ?? _adapter;
            _selectedIds = adapter.GetSelectedRows();
            foreach (var i in _selectedIds)
            {
                SelectedItems.Add(_items.First(x => x.ItemId == i));
            }
            _adapter.NotifyDataSetChanged();
            Log.Debug("Fragment", $"_selectedItems[{SelectedItems.Count}]: {JsonConvert.SerializeObject(SelectedItems, Formatting.Indented)}");
            
            return SelectedItems;
=======
            _selectedItems.Clear();
            
            var selected = ((AllItemsAdapter)ListAdapter).GetSelectedRows();
            foreach (var i in selected)
            {
                _selectedItems.Add(_items.First(x => x.ItemId == i));
            }
            _adapter.NotifyDataSetChanged();
            Log.Debug("Fragment", $"_selectedItems[{_selectedItems.Count}]: {JsonConvert.SerializeObject(_selectedItems, Formatting.Indented)}");
            
            return _selectedItems;
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        }

        public void UpdateItems()
        {
<<<<<<< HEAD
            _selectedIds = _adapter?.GetSelectedRows();
            _items = _repository.GetAllItemsWithCildren();
            _adapter = new AllItemsAdapter(Activity, _items, _selectedIds);
            _adapter.NotifyDataSetChanged();
            ListAdapter =  _adapter;
=======
            _items = _repository.GetAllItemsWithCildren();
            _adapter = new AllItemsAdapter(Activity, _items);
            _adapter.NotifyDataSetChanged();
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        }
    }
}