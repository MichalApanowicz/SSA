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
    public class AllListsFragment : Fragment
    {
        ListRepository _lists = new ListRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private ListView listView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AllLists, null);
            listView = view.FindViewById<ListView>(Resource.Id.listView1);

            var lists = _lists.GetAll();
            var adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleListItem1, objects: lists.Select(x => x.Name).ToArray());

            listView.Adapter = adapter;

            return view;
        }
    }
}