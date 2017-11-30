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
    public class AllListsFragment : Android.Support.V4.App.Fragment
    {
        private ListRepository _listRepository;
 
        private AllListsFragment() { }

        public static AllListsFragment NewInstance(ListRepository listRepository)
        {
            AllListsFragment fragment = new AllListsFragment { _listRepository = listRepository };
            return fragment;
        }

        private ListView listView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AllLists, null);
            listView = view.FindViewById<ListView>(Resource.Id.listView1);

            var lists = _listRepository.GetAll();
            var adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleListItem1, objects: lists.Select(x => x.Name).ToArray());

            listView.Adapter = adapter;

            return view;
        }
    }
}