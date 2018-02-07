using System;
using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Models;


namespace SSA.Droid.Repositories
{
    public class DataEditor
    {
        public static MainRepository LocalData = new MainRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        public static bool AddItemToList(ItemModel item, ListModel list)
        {

            throw new NotImplementedException();
        }

        public static bool RemoveItemToList(ItemModel item, ListModel list)
        {

            throw new NotImplementedException();
        }

        public static bool AddNewList()
        {

            throw new NotImplementedException();
        }

        public static bool CommitList()
        {

            throw new NotImplementedException();
        }

        public static bool TerminateList(ListModel list)
        {
            throw new NotImplementedException();
        }
    }
}