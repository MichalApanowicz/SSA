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
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;
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
<<<<<<< HEAD
=======

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ListModel> ListModels { get; set; }
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
    }
}