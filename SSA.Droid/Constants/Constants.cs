using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Environment = System.Environment;

namespace SSA.Droid
{

    public class Constants
    {
        public static readonly string DatabasePath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "Database.db"
            );
    }
}