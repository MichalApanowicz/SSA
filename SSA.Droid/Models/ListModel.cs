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
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SSA.Droid.Models
{
    [Table("ListModel")]
    public class ListModel
    {
        [PrimaryKey, AutoIncrement]
        public int ListId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey(typeof(PersonModel))]
        public int PersonId { get; set; }

        [ManyToOne]
        public PersonModel Person { get; set; }

        public string CreateDate { get; set; }

        [ForeignKey(typeof(ListStatus))]
        public int ListStatusId { get; set; }

        [ManyToOne]
        public ListStatus Status { get; set; }

        [OneToMany]
        public List<ItemModel> Items { get; set; }

        public override string ToString()
        {
            return $"[List: ListId={ListId}, Name={Name}, Status={ListStatusId}, Items[{Items.Count}]: {Items.ToArray()}]";
        }
    }
}