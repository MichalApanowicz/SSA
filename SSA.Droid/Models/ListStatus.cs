﻿using System;
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
    [Table("ListStatus")]
    public class ListStatus
    {
        [PrimaryKey, AutoIncrement]
        public int ListStatusId { get; set; }

        [NotNull]
        public string Name { get; set; }
    }
}