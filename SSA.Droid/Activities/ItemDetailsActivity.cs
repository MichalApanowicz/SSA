﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities
{
    [Activity(Label = "ItemDetailsActivity")]
    public class ItemDetailsActivity : Activity
    {
        private ItemModel _item;
        private TextView _ean, _name, _description, _status, _list, _category, _localization;
        private CheckBox _damaged;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ItemDetailsActivity);

            var text = Intent.GetStringExtra("Item");
            _item = JsonConvert.DeserializeObject<ItemModel>(text) ?? new ItemModel();

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = _item.Name;
            toolbar.InflateMenu(Resource.Menu.top_menu);
            toolbar.SetBackgroundColor(new Color(_item.Category.ColorR, _item.Category.ColorG,
                _item.Category.ColorB));
            SetActionBar(toolbar);

            _ean = FindViewById<TextView>(Resource.Id.textItemDetailsEAN);
            _name = FindViewById<TextView>(Resource.Id.textItemDetailsName);
            _description = FindViewById<TextView>(Resource.Id.textItemDetailsDescription);
            _status = FindViewById<TextView>(Resource.Id.textItemDetailsStatus);
            _list = FindViewById<TextView>(Resource.Id.textItemDetailsList);
            _category = FindViewById<TextView>(Resource.Id.textItemDetailsCategory);
            _localization = FindViewById<TextView>(Resource.Id.textItemDetailsLocalization);
            _damaged = FindViewById<CheckBox>(Resource.Id.checkBox1);

            _ean.Text = _item.KodEAN;
            _name.Text = _item.Name;
            _description.Text = _item.Description.Replace(", ", System.Environment.NewLine).Replace(":", ":" + System.Environment.NewLine);
            _status.Text = _item.Status.Name;
            _list.Text = JsonConvert.SerializeObject(_item.ListId);
            _category.Text = _item.Category.Name;
            _localization.Text = _item.Localization.Name;
            _damaged.Checked = _item.Damaged;

            _damaged.Click += (o, e) =>
            {
                if (_damaged.Checked != _item.Damaged)
                {
                    _item.Damaged = _damaged.Checked;
                    DataProvider.UpdateItem(_item);
                    Toast.MakeText(this, $"Oznaczyłeś {_item.Name} jako uszkodzony", ToastLength.Short).Show();
                }
                else
                    Toast.MakeText(this, $"Oznaczyłeś {_item.Name} jako nieuszkodzony", ToastLength.Short).Show();
            };

      
        }
    }
}