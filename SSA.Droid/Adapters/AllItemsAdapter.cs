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
        public List<int> Selected { get; set; }

        public AllItemsAdapter(Activity context, List<ItemModel> items, List<int> selected = null) : base()
        {
            _context = context;
            _items = items;
            Selected = selected ?? new List<int>();
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
<<<<<<< HEAD
                holder.LinearLayout = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutHeader);
=======
                holder.LinearLayout = view.FindViewById<LinearLayout>(Resource.Id.linearLayout3);
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
                holder.Name = view.FindViewById<TextView>(Resource.Id.textView1);
                holder.Description = view.FindViewById<TextView>(Resource.Id.textView2);
                holder.CheckBox = view.FindViewById<CheckBox>(Resource.Id.checkBox1);
                holder.Status = view.FindViewById<TextView>(Resource.Id.textView3);
<<<<<<< HEAD
                holder.ColorHandler = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutColor);
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
                holder.Item = item;

                holder.CheckBox.Click += (s, e) =>
                {
                    if (holder.CheckBox.Checked) Selected.Add(holder.Item.ItemId);
                    else Selected.Remove(holder.Item.ItemId);
                    Log.Debug("Adapter", $"_selected: {JsonConvert.SerializeObject(Selected, Formatting.Indented)}");
                };
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
<<<<<<< HEAD
            holder.Name.Text = $"{item.Name} [{item.KodEAN}]";
            if (item.ListId != 0) holder.Name.Text += $"[Lista nr {item.ListId}]";
=======
            holder.Name.Text = $"{item.Name} [{item.ItemId}]";
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
            holder.Description.Text = item.Description;
            if (item.Status.ItemStatusId == (int)ItemStatusEnum.Available)
            {
                holder.CheckBox.Checked = Selected.Contains(item.ItemId);
                holder.CheckBox.Visibility = ViewStates.Visible;
                holder.Status.Visibility = ViewStates.Invisible;
            }
            else
            {
                holder.Status.Text = item.Status.Name;
<<<<<<< HEAD
                holder.CheckBox.Visibility = ViewStates.Invisible;
                holder.Status.Visibility = ViewStates.Visible;
            }

            var color = new Color(item.Category.ColorR, item.Category.ColorG, item.Category.ColorB);
            holder.ColorHandler.SetBackgroundColor(color);
=======
                //holder.CheckBox.Visibility = ViewStates.Invisible;
                holder.Status.Visibility = ViewStates.Visible;
            }

>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681

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

    internal class ItemOnListViewHolder : Java.Lang.Object
    {
        public ItemModel Item { get; set; }
        public LinearLayout LinearLayout { get; set; }
        public TextView Name { get; set; }
        public TextView Description { get; set; }
        public TextView Status { get; set; }
        public CheckBox CheckBox { get; set; }
<<<<<<< HEAD
        public Color Color { get; set; }
        public LinearLayout ColorHandler { get; set; }
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
    }
}

