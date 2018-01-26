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
    [Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int CategoryId { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull, Default(true, 0)]
        public int ColorR { get; set; }

        [NotNull, Default(true, 0)]
        public int ColorG { get; set; }

        [NotNull, Default(true, 0)]
        public int ColorB { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ItemModel> ItemModels { get; set; }
    }
}