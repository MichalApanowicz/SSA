using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SSA.Droid.Activities;
using SSA.Droid.Models;

namespace SSA.Droid.Adapters
{
    public class ItemsOnListDetailsAdapter : BaseAdapter<string>
    {
        private readonly List<ItemModel> _items;
        private readonly Activity _context;
        public List<int> Selected { get; set; }

        public ItemsOnListDetailsAdapter(Activity context, List<ItemModel> items, List<int> selected = null) : base()
        {
            _context = context;
            _items = items;
            Selected = selected ?? new List<int>();
        }

        public override long GetItemId(int position) => _items.ToArray()[position].ItemId;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ItemOnListDetailsViewHolder holder = null;
            var view = convertView;

            if (view != null)
                holder = view.Tag as ItemOnListDetailsViewHolder;

            var item = _items[position];

            if (holder == null)
            {
                holder = new ItemOnListDetailsViewHolder();
                view = _context.LayoutInflater.Inflate(Resource.Layout.ItemOnListDetailsList, null);
                holder.LinearLayout = view.FindViewById<LinearLayout>(Resource.Id.linearLayout3);
                holder.Name = view.FindViewById<TextView>(Resource.Id.textView1);
                holder.Description = view.FindViewById<TextView>(Resource.Id.textView2);
                holder.CheckBox = view.FindViewById<CheckBox>(Resource.Id.checkBox1);
                holder.Status = view.FindViewById<TextView>(Resource.Id.textView3);
                holder.Item = item;

                holder.LinearLayout.Click += (sender, args) =>
                {
                    var intent = new Intent(_context, typeof(ItemDetailsActivity));
                    intent.PutExtra("Item", JsonConvert.SerializeObject(holder.Item));

                    Log.Debug("Adapter", $"item: {JsonConvert.SerializeObject(holder.Item, Formatting.Indented)}");
                    _context.StartActivity(intent);
                };
                view.Tag = holder;
            }

            holder.Item = item;
            holder.Name.Text = $"{item.Name} [{item.KodEAN}]";
            if (item.ListId != 0) holder.Name.Text += $"[Lista nr {item.ListId}]";
            holder.Description.Text = item.Description;
            holder.Status.Text = item.Status.Name;

            return view;
        }

        public override int Count => _items.Count;

        public override string this[int position] => _items.ToArray()[position].Name;

        public List<int> GetSelectedRows()
        {
            var result = Selected.Select(x => x).ToList();// Select(item => item.Clone()).ToList(); ;
            Selected.Clear();
            return result;
        }
    }

    internal class ItemOnListDetailsViewHolder : Java.Lang.Object
    {
        public ItemModel Item { get; set; }
        public LinearLayout LinearLayout { get; set; }
        public TextView Name { get; set; }
        public TextView Description { get; set; }
        public TextView Status { get; set; }
        public CheckBox CheckBox { get; set; }
    }
}

