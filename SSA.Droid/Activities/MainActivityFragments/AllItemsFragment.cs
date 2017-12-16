using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
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

        public override void OnStart()
        {
            //ListView.ChoiceMode = ChoiceMode.Multiple;
            base.OnStart();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AllItems, null);
            
            SelectedItems = new List<ItemModel>();

            _items = _itemRepository.GetAllWithCildren();

            ListAdapter = new AllItemsAdapter(inflater, _items);
            

            return view;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            //ListView.SetItemChecked(position, true);
            var item = _items[position];
            if (v.FindViewById<CheckBox>(Resource.Id.checkBox1).Checked =
                !v.FindViewById<CheckBox>(Resource.Id.checkBox1).Checked)
            {
                SelectedItems.Add(item);
            }
            else
            {
                SelectedItems.Remove(item);
            }
            base.OnListItemClick(l, v, position, id);
        }
    }
}
