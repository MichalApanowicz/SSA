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
        private ItemRepository _itemRepository;
        private List<ItemModel> _items;
        public static List<ItemModel> SelectedItems;

        private AllItemsFragment() { }

        public static AllItemsFragment NewInstance(ItemRepository itemRepository)
        {
            var fragment = new AllItemsFragment { _itemRepository = itemRepository };
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AllItems, null);
            var lv = view.FindViewById<ListView>(Android.Resource.Id.List);
            SelectedItems = new List<ItemModel>();

            _items = _itemRepository.GetAllWithCildren();

            ListAdapter = new AllItemsAdapter(Activity, _items);
            lv.ChoiceMode = ChoiceMode.Multiple;
            lv.ItemSelected += (sender, e) =>
            {
                e.View.SetBackgroundColor(Color.OrangeRed);
            };
            return view;
        }


        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var checkBox = v.FindViewById<CheckBox>(Resource.Id.checkBox1);
            var text1 = v.FindViewById<TextView>(Resource.Id.textView1).Text;
            var text2 = v.FindViewById<TextView>(Resource.Id.textView2).Text;

            var item = _items.FirstOrDefault(x => x.ItemId == id);
            
            if (SelectedItems.Contains(item))
            {
                SelectedItems.Remove(item);
                ListView.SetItemChecked(position, false);
                v.SetBackgroundColor(Color.Wheat);
                checkBox.Checked = false;

            }
            else
            {
                SelectedItems.Add(item);
                ListView.SetItemChecked(position, true);
                v.SetBackgroundColor(Color.OrangeRed);
                checkBox.Checked = true;
            }
            for (var i=0; i< ListView.CheckedItemPositions.Size(); i++)
            {
               // ListView.
            }
            Log.Info($"checkBox: {position}", ListView.IsItemChecked(position).ToString());
            Log.Info($"GetCheckedItemIds", ListView.GetCheckedItemIds().Length.ToString());
            Log.Info($"SelectedItemId", ListView.CheckedItemPositions.ToString());
            Log.Info($"CheckedItemCount", ListView.CheckedItemCount.ToString());

            //base.OnListItemClick(l, v, position, id);
        }
    }
}