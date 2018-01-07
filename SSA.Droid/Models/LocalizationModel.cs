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
    [Table("Localization")]
    public class Localization
    {
        [PrimaryKey, AutoIncrement]
        public int LocalizationId { get; set; }

        [NotNull]
        public string Code { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ItemModel> ItemModels { get; set; }
    }
}