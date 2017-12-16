using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SSA.Droid.Activities;
using SSA.Droid.Models;

namespace SSA.Droid.Adapters
{
    public class AllItemsAdapter : BaseAdapter<string>
    {
        private readonly List<ItemModel> _items;
        private readonly Activity _context;

        public AllItemsAdapter(Activity context, List<ItemModel> items) : base()
        {
            _context = context;
            _items = items;
        }

        public override long GetItemId(int position) => _items.ToArray()[position].ItemId;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ItemOnAllItemsList, null);

            var linearLayout = view.FindViewById<LinearLayout>(Resource.Id.linearLayout3);
            var checkBox = view.FindViewById<CheckBox>(Resource.Id.checkBox1);
            var text1 = view.FindViewById<TextView>(Resource.Id.textView1);
            var text2 = view.FindViewById<TextView>(Resource.Id.textView2);

            text1.Text = $"{item.Name} [id: {checkBox.Id}, tag: {checkBox.Tag}, itemId: {item.ItemId}]";
            text2.Text = $"{item.Description}";

            var available = item.ItemStatusId == (int)ItemStatusEnum.Available;
            if (available)
            {
               // view.FindViewById<TextView>(Resource.Id.textView3).Visibility = ViewStates.Invisible;
            }
            else
            {
                //view.FindViewById<TextView>(Resource.Id.textView3).Text = $"{item.Status.Name}";
                checkBox.Visibility = ViewStates.Invisible;
            }
           

            if (convertView == null)
            {
                linearLayout.Click += (sender, e) =>
                {
                    var intent = new Intent(_context, typeof(ItemDetailsActivity));
                    intent.PutExtra("Item", JsonConvert.SerializeObject(item));

                    _context.StartActivity(intent);
                };
            }

            return view;
        }

        public override int Count => _items.Count;

        public override string this[int position] => _items.ToArray()[position].Name;
    }
}