﻿using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
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
    public class AllListsFragment : Android.Support.V4.App.ListFragment
    {
        private MainRepository _repository;
        private List<ListModel> _lists;

        private AllListsFragment() { }

        public static AllListsFragment NewInstance(MainRepository repository)
        {
            var fragment = new AllListsFragment { _repository = repository };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.AllLists, null);

            _lists = _repository.GetAllListsWithCildren();

            ListAdapter = new AllListsAdapter(Activity, _lists);

            return view;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var list = _lists.Find(x => x.ListId == id);
            base.OnListItemClick(l, v, position, id);
            var intent = new Intent(Context, typeof(ListDetailsActivity));
            intent.PutExtra("List", JsonConvert.SerializeObject(list, new JsonSerializerSettings(){MaxDepth = 1}));

            StartActivity(intent);
        }

        //public void UpdateLists()
        //{
        //    _lists = _repository.GetAllListsWithCildren();
        //    ListAdapter = new AllListsAdapter(Activity, _lists);
        //}
    }
}