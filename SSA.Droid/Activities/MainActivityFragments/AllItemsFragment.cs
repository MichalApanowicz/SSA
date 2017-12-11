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
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities.MainActivityFragments
{
    public class AllItemsFragment : Android.Support.V4.App.ListFragment
    {
        private ItemRepository _itemRepository;
        private List<ItemModel> _items;

        private AllItemsFragment() { }

        public static AllItemsFragment NewInstance(ItemRepository itemRepository)
        {
            AllItemsFragment fragment = new AllItemsFragment { _itemRepository = itemRepository };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AllItems, null);

            _items = _itemRepository.GetAll();
            var adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleListItem1,
                objects: _items.Select(x => x.Name).ToArray());

            ListAdapter = adapter;

            return view;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var item = _items[position];
            base.OnListItemClick(l, v, position, id);
            //Intent intent = new Intent(Context, typeof(ListDetailsActivity));
            //intent.PutExtra("Item", JsonConvert.SerializeObject(item));

            //StartActivity(intent);
        }
    }
}