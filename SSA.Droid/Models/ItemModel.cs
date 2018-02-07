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
    [Table("ItemModel")]
    public class ItemModel
    {
        [PrimaryKey, AutoIncrement]
        public int ItemId { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Description { get; set; }

        [NotNull, Unique]
        public string KodEAN { get; set; }

        [ForeignKey(typeof(ItemStatus))]
        public int ItemStatusId { get; set; }

        [ManyToOne()]
        public ItemStatus Status { get; set; }

        [ForeignKey(typeof(ListModel))]
        public int ListId { get; set; }

        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        [ManyToOne()]
        public Category Category { get; set; }

        [ForeignKey(typeof(Localization))]
        public int LocalizationId { get; set; }

        [ManyToOne()]
        public Localization Localization { get; set; }

        [NotNull]
        public bool Damaged { get; set; }

        public override string ToString()
        {
            return $"[Item: ItemId={ItemId}, Name={Name}, ListId={ListId}, Status={ItemStatusId}]";
        }
    }
}