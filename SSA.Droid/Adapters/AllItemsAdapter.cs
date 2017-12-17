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
    public class AllItemsAdapter : BaseAdapter<string>
    {
        private readonly List<ItemModel> _items;
        private readonly Activity _context;
        private readonly List<int> _selected = new List<int>();

        public AllItemsAdapter(Activity context, List<ItemModel> items) : base()
        {
            _context = context;
            _items = items;
        }

        public override long GetItemId(int position) => _items.ToArray()[position].ItemId;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ItemOnListViewHolder holder = null;
            var view = convertView;

            if (view != null)
                holder = view.Tag as ItemOnListViewHolder;

            var item = _items[position];

            if (holder == null)
            {
                holder = new ItemOnListViewHolder();
                view = _context.LayoutInflater.Inflate(Resource.Layout.ItemOnAllItemsList, null);
                holder.LinearLayout = view.FindViewById<LinearLayout>(Resource.Id.linearLayout3);
                holder.Name = view.FindViewById<TextView>(Resource.Id.textView1);
                holder.Description = view.FindViewById<TextView>(Resource.Id.textView2);
                holder.CheckBox = view.FindViewById<CheckBox>(Resource.Id.checkBox1);

                holder.CheckBox.Click += (s, e) =>
                {
                    if (holder.CheckBox.Checked) _selected.Add(item.ItemId);
                    else _selected.Remove(item.ItemId);
                    Log.Debug("Adapter", $"_selected: {JsonConvert.SerializeObject(_selected, Formatting.Indented)}");
                };
                holder.LinearLayout.Click += (sender, e) =>
                {
                    var intent = new Intent(_context, typeof(ItemDetailsActivity));
                    intent.PutExtra("Item", JsonConvert.SerializeObject(item));

                    _context.StartActivity(intent);
                };
                view.Tag = holder;
            }

            holder.Name.Text = $"{item.Name} [{item.ItemId}]";
            holder.Description.Text = item.Description;
            holder.CheckBox.Checked = _selected.Contains(item.ItemId);
            return view;
        }

        public override int Count => _items.Count;

        public override string this[int position] => _items.ToArray()[position].Name;

        public List<int> GetSelectedRows()
        {
            var result = _selected.Select(x => x).ToList();// Select(item => item.Clone()).ToList(); ;
            _selected.Clear();
            return result;
        }
    }

    internal class ItemOnListViewHolder : Java.Lang.Object
    {
        public LinearLayout LinearLayout { get; set; }
        public TextView Name { get; set; }
        public TextView Description { get; set; }
        public CheckBox CheckBox { get; set; }
    }
}

