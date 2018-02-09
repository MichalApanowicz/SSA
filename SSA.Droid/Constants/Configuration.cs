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

namespace SSA.Droid
{
    static class Configuration
    {
        public static string ApiPath = "http://192.168.0.13:3000/";
        public static bool Online = false;
    }
}