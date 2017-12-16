using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
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
    public class AllListsFragment : Android.Support.V4.App.ListFragment
    {
        private ListRepository _listRepository;
        private List<ListModel> _lists;

        private AllListsFragment() { }

        public static AllListsFragment NewInstance(ListRepository listRepository)
        {
            AllListsFragment fragment = new AllListsFragment { _listRepository = listRepository };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AllLists, null);

            _lists = _listRepository.GetAll();
            var adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleListItem1,
                objects: _lists.Select(x => x.Name).ToArray());

            ListAdapter = adapter;

            return view;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var list = _lists[position];
            base.OnListItemClick(l, v, position, id);
            Intent intent = new Intent(Context, typeof(ListDetailsActivity));
            intent.PutExtra("List", JsonConvert.SerializeObject(list));

            StartActivity(intent);
        }
    }
}