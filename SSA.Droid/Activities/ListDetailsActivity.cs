using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SSA.Droid.Models;

namespace SSA.Droid.Activities
{
    [Activity(Label = "ListDetailsActivity")]
    public class ListDetailsActivity : ListActivity
    {
        private ListModel _list;
        private List<ItemModel> _items;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListDetailsActivity);
            var text = Intent.GetStringExtra("List");
            _list = JsonConvert.DeserializeObject<ListModel>(text) ?? new ListModel();
            _items = _list.Items;

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = _list.Name;
            toolbar.InflateMenu(Resource.Menu.top_menu);
            SetActionBar(toolbar);

            
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked,
                objects: _items.Select(x => x.Name).ToArray());

            ListAdapter = adapter;
        }
    }
}