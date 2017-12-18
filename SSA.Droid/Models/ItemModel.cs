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
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;
using SQLiteNetExtensions.Attributes;

namespace SSA.Droid.Models
{
    [Table("ItemModel")]
    public class ItemModel
    {
        [PrimaryKey, AutoIncrement]
        public int ItemId { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Description { get; set; }

        [NotNull]
        public string KodEAN { get; set; }

        [ForeignKey(typeof(ItemStatus))]
        public int ItemStatusId { get; set; }

        [ManyToOne()]
        public ItemStatus Status { get; set; }

        [ManyToMany(typeof(ItemInLists))]
        public List<ListModel> Lists { get; set; }

        public override string ToString()
        {
            return $"[Item: ItemId={ItemId}, Name={Name}, Lists.Count={Lists.Count}, Status={ItemStatusId}]";
        }
    }
}