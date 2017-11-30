using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities.MainActivityFragments
{
    public class AllItemsFragment : Android.Support.V4.App.Fragment
    {
        private ListView _listView;

        private ItemRepository _itemRepository;

        private AllItemsFragment() { }

        public static AllItemsFragment NewInstance(ItemRepository itemRepository)
        {
            AllItemsFragment fragment = new AllItemsFragment { _itemRepository = itemRepository };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AllItems, null);
            _listView = view.FindViewById<ListView>(Resource.Id.listView1);

            var items = _itemRepository.GetAll();
            var adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleListItem1,
                objects: items.Select(x => x.Name).ToArray());

            _listView.Adapter = adapter;

            return view;
        }
    }
}