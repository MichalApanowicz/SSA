using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Repositories;

namespace  SSA.Droid.Activities.MainActivityFragments
{
    public class AllItemsFragment : Fragment
    {
        ItemRepository _items =
            new ItemRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private ListView listView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AllItems, null);
            listView = view.FindViewById<ListView>(Resource.Id.listView1);

            var items = _items.GetAll();
            var adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleListItem1,
                objects: items.Select(x => x.Name).ToArray());

            listView.Adapter = adapter;

            return view;
        }
    }
}