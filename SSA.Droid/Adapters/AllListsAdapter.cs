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
<<<<<<< HEAD
            _lists = lists.OrderByDescending(x => DateTime.Parse(x.CreateDate)).ToList();
=======
            _lists = lists;
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
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
<<<<<<< HEAD
                holder.Person = view.FindViewById<TextView>(Resource.Id.ListPersonText);
                holder.Date = view.FindViewById<TextView>(Resource.Id.ListDateText);
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
                view.Tag = holder;
            }

            var list = _lists[position];
            holder.Name.Text = list.Name;
            holder.Status.Text = list.Status.Name;
            holder.Count.Text = $"[{list.Items.Count}]";
<<<<<<< HEAD
            holder.Person.Text = list.Person.Name;
            holder.Date.Text = list.CreateDate;
=======

>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
            return view;
        }
        public override int Count => _lists.Count;
    }

    internal class AllListsAdapterViewHolder : Java.Lang.Object
    {
        public LinearLayout LinearLayout { get; set; }
        public TextView Name { get; set; }
        public TextView Status { get; set; }
<<<<<<< HEAD
        public TextView Person { get; set; }
        public TextView Date { get; set; }
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        public TextView Count { get; set; }
    }
}