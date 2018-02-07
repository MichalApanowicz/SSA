using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities.MainActivityFragments
{
    public class SettingsFragment : Android.Support.V4.App.Fragment
    {
        private EditText _apiUrl;


        private MainRepository _repository;

        private SettingsFragment()
        {
        }

        public static SettingsFragment NewInstance(MainRepository repository)
        {
            var fragment = new SettingsFragment
            {
                _repository = repository
            };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.Settings, null);

            _apiUrl = view.FindViewById<EditText>(Resource.Id.editApiUrl);
            _apiUrl.TextChanged += (s, e) =>
            {
                Constants.ApiPath = _apiUrl.Text;
            };

            return view;
        }
    }
}