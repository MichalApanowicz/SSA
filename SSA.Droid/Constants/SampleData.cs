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
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid
{ 
    static class SampleData
    {

        static ItemRepository _itemRepository = new ItemRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        static ListRepository _listRepository = new ListRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        static ItemStatusRepository _itemRepositorytatus = new ItemStatusRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        static ListStatusRepository _listRepositorytatus = new ListStatusRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private static ItemStatus itemStatus1 = new ItemStatus
        {
            ItemStatusId = 1,
            Name = "Available",
        };

        private static ListStatus listStatus1 = new ListStatus
        {
            ListStatusId = 1,
            Name = "Available",
        };

        private static List<ItemModel> items = new List<ItemModel>
        {
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1234",
                Name = "Młotek",
                Description = "500g żółty",
                ListId = 1
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1235",
                Name = "Siekiera",
                Description = "kuta ręcznie",
                ListId = 1
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1236",
                Name = "Gwoździe calowae",
                Description = "5kg",
                ListId = 1
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1237",
                Name = "Młotek",
                Description = "250g żółty",
                ListId = 1
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1238",
                Name = "Łopata",
                Description = "Fiskars",
                ListId = 1
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1238",
                Name = "Łopata",
                Description = "Fiskars",
                ListId = 2
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1239",
                Name = "Dołownik",
                Description = "Romanik",
                ListId = 2
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1240",
                Name = "Plandeka",
                Description = "9m^2",
                ListId = 2
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1241",
                Name = "Plandeka",
                Description = "3m^2",
                ListId = 2
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1242",
                Name = "Plandeka",
                Description = "2m^2",
                ListId = 1
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1242",
                Name = "Latarka",
                Description = "duża",
                ListId = 2
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1242",
                Name = "Skrzynka narzędziowa",
                Description = "niebieska",
                ListId = 1
            },
            new ItemModel()
            {
                Status = itemStatus1,
                KodEAN = "AB 1242",
                Name = "Apteczka",
                Description = "mała nr 2",
                ListId = 3
            },

        };

        private static List<ListModel> lists = new List<ListModel>
        {
            new ListModel()
            {
                Name = "Magazyn Główny",
                Description = "Rzeczy znajdujące się w magazynie nr 1 przy ulicy Głównej 1 w Bydgoszczy",
                ListStatusId = 1,
                Items = new List<ItemModel>
                {

                }
            },
            new ListModel()
            {
                Name = "Lista obozowa 1",
                Description = "Rzeczy zabrane na kwaterkę",
                ListStatusId = 1,
                Items = new List<ItemModel>
                {

                }
            },
            new ListModel()
            {
                Name = "Lista Jastrzębi",
                Description = "Rzeczy zabrane na zbiórkę",
                Status = listStatus1,
                Items = new List<ItemModel> 
                {

                }
            }
        };

        public static void AddData()
        {
            _itemRepositorytatus.Save(itemStatus1);
            _listRepositorytatus.Save(listStatus1);

            foreach (var list in lists)
            {
                _listRepository.Save(list);
            }
            foreach (var item in items)
            {
                _itemRepository.Save(item);
            }
        }
        public static void DropData()
        {
            _itemRepositorytatus.DeleteAll();
            _listRepositorytatus.DeleteAll();
            _listRepository.DeleteAll();
            _itemRepository.DeleteAll();
        }
    }
}