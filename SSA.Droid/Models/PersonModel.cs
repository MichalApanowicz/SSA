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
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace SSA.Droid.Models
{
    [Table("PersonModel")]
    public class PersonModel
    {
        [PrimaryKey, AutoIncrement]
        public int PersonId { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        [OneToMany]
        public List<ListModel> Lists { get; set; }

        public override string ToString()
        {
            return $"[Person: PersonId={PersonId}, Name={Name}, Lists[{Lists.Count}]: {Lists.ToArray()}]";
        }
    }
}