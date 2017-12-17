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
using SSA.Droid.Models;

namespace SSA.Droid.Adapters
{
    public class AllListsAdapter : BaseAdapter<string>
    {

        private readonly List<ListModel> _lists;
        private readonly Activity _context;

        public AllListsAdapter(Activity context, List<ListModel> lists)
        {
            _context = context;
            _lists = lists;
        }

        public override string this[int position] => _lists[position].Name;

        public override long GetItemId(int position) => _lists[position].ListId;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            AllListsAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as AllListsAdapterViewHolder;

            if (holder == null)
            {
                holder = new AllListsAdapterViewHolder();
                view = _context.LayoutInflater.Inflate(Resource.Layout.ListOnAllListsList, parent, false);
                holder.Name = view.FindViewById<TextView>(Resource.Id.ListNameText);
                holder.Status = view.FindViewById<TextView>(Resource.Id.ListStatusText);
                holder.Count = view.FindViewById<TextView>(Resource.Id.ListCountText);
                view.Tag = holder;
            }

            var list = _lists[position];
            holder.Name.Text = list.Name;
            holder.Status.Text = list.Status.Name;
            holder.Count.Text = $"[{list.Items.Count}]";

            return view;
        }
        public override int Count => _lists.Count;
    }

    internal class AllListsAdapterViewHolder : Java.Lang.Object
    {
        public LinearLayout LinearLayout { get; set; }
        public TextView Name { get; set; }
        public TextView Status { get; set; }
        public TextView Count { get; set; }
    }
}