using System;
using System.Collections.Generic;
using Android.App;
using Android.Nfc;
using Android.Widget;
using Android.OS;
using Android.Util;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Activities.MainActivityFragments;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid
{
    [Activity(Label = "SSA.Droid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Fragment[] _fragments;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _fragments = new Fragment[]
            {
                new AllListsFragment(),
                new AllItemsFragment(),
                new TestFragment(),
            };

            InitializeTabNavigation();
            SetContentView(Resource.Layout.MainActivity);
        }

        private void InitializeTabNavigation()
        {
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTabToActionBar(Resource.String.main_tab_all_lists);
            AddTabToActionBar(Resource.String.main_tab_all_items);
            AddTabToActionBar(Resource.String.main_tab_test);
        }

        void AddTabToActionBar(int labelResourceId)
        {
            ActionBar.Tab tab = ActionBar.NewTab()
                .SetText(labelResourceId);
            tab.TabSelected += TabOnTabSelected;
            ActionBar.AddTab(tab);
        }

        void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        {
            ActionBar.Tab tab = (ActionBar.Tab)sender;

            Log.Debug("The tab {0} has been selected.", tab.Text);
            Fragment frag = _fragments[tab.Position];
            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }
    }
}

