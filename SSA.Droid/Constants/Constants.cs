﻿using System;
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
<<<<<<< HEAD

        public static string ApiPath = "http://192.168.0.13:3000/";
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
    }
}