using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using SSA.Droid.Models;

namespace SSA.Droid.Adapters
{
    public class AllItemsAdapter : BaseAdapter<string>
    {
        private readonly List<ItemModel> _items;
        private readonly LayoutInflater _layoutInflater;

        public AllItemsAdapter(LayoutInflater layoutInflater, List<ItemModel> items)
        {
            _layoutInflater = layoutInflater;
            _items = items;
        }

        public override long GetItemId(int position) => _items.ToArray()[position].ItemId;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items.ToArray()[position];

            var view = convertView ?? _layoutInflater.Inflate(Resource.Layout.ItemOnAllItemsList, null);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = $"{item.Name}";
            view.FindViewById<TextView>(Resource.Id.textView2).Text = $"{item.Description}";

            var available = item.ItemStatusId == (int)ItemStatusEnum.Available;
            if (available)
            {
               // view.FindViewById<TextView>(Resource.Id.textView3).Visibility = ViewStates.Invisible;
            }
            else
            {
                //view.FindViewById<TextView>(Resource.Id.textView3).Text = $"{item.Status.Name}";
                view.FindViewById<CheckBox>(Resource.Id.checkBox1).Visibility = ViewStates.Invisible;
            }
            
            
            return view;
        }

        public override int Count => _items.Count;

        public override string this[int position] => _items.ToArray()[position].Name;
    }
}