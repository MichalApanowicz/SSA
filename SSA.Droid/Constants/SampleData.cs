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
    public static class SampleData
    {

        static readonly MainRepository Repository = new MainRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        
        private static List<ItemStatus> _itemStatus = new List<ItemStatus>()
        {
            new ItemStatus
            {
                ItemStatusId = (int)ItemStatusEnum.Available,
                Name = "Available",
            },
            new ItemStatus
            {
                ItemStatusId = (int)ItemStatusEnum.Unavailable,
                Name = "Unavailable",
            },
            new ItemStatus
            {
                ItemStatusId = (int)ItemStatusEnum.Reserved,
                Name = "Reserved",
            }
         };
        private static readonly List<ListStatus> ListStatus = new List<ListStatus>()
        {
            new ListStatus
            {
                ListStatusId = (int)ListStatusEnum.Uncommitted,
                Name = "Uncommitted",
            },
            new ListStatus
            {
                ListStatusId = (int)ListStatusEnum.Committed,
                Name = "Committed",
            },
            new ListStatus
            {
                ListStatusId = (int)ListStatusEnum.Terminated,
                Name = "Terminated",
            }
        };
        

        

        private static readonly List<ListModel> Lists = new List<ListModel>
        {
            new ListModel()
            {
                Name = "Magazyn Główny",
                Description = "Rzeczy znajdujące się w magazynie nr 1 przy ulicy Głównej 1 w Bydgoszczy",
                CreateDate = "2017-12-14", Person = "Michał Apanowicz",
                ListStatusId = 1,
                Items = new List<ItemModel>
                {

                },
                Status = ListStatus[0]
            },
            new ListModel()
            {
                Name = "Lista obozowa 1",
                Description = "Rzeczy zabrane na kwaterkę",
                CreateDate = "2017-12-14", Person = "Michał Apanowicz", ListStatusId = 1,
                Items = new List<ItemModel>
                {

                },
                Status = ListStatus[0]
            },
            new ListModel()
            {
                Name = "Lista Jastrzębi",
                Description = "Rzeczy zabrane na zbiórkę",
                CreateDate = "2017-12-14", Person = "Michał Apanowicz", ListStatusId = 2,
                Items = new List<ItemModel>
                {

                },
                Status = ListStatus[1]
            }
        };

        private static readonly List<ItemModel> Items = new List<ItemModel>
        {
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Reserved),
                KodEAN = "AB 1234",
                Name = "Młotek",
                Description = "500g żółty",
                Lists = new List<ListModel>()
                {
                    Lists[1]
                }
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "AB 1235",
                Name = "Siekiera",
                Description = "kuta ręcznie",
                Lists = new List<ListModel>()
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "AB 1236",
                Name = "Gwoździe calowae",
                Description = "5kg",
                Lists = new List<ListModel>()
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "AB 1237",
                Name = "Młotek",
                Description = "250g żółty",
                Lists = new List<ListModel>()
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "AB 1238",
                Name = "Łopata",
                Description = "Fiskars",
                Lists = new List<ListModel>()
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "AB 1239",
                Name = "Dołownik",
                Description = "Romanik",
                Lists = new List<ListModel>()
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "AB 1240",
                Name = "Plandeka",
                Description = "9m^2",
                Lists = new List<ListModel>()
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "AB 1241",
                Name = "Plandeka",
                Description = "3m^2",
                Lists = new List<ListModel>()
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "AB 1242",
                Name = "Plandeka",
                Description = "2m^2",
                Lists = new List<ListModel>()
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "AB 1242",
                Name = "Latarka",
                Description = "duża",
                Lists = new List<ListModel>()
                {
                    Lists[0]
                }
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "AB 1242",
                Name = "Skrzynka narzędziowa",
                Description = "niebieska",
                Lists = new List<ListModel>()
                {
                    Lists[1]
                }
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Reserved),
                KodEAN = "AB 1242",
                Name = "Apteczka",
                Description = "mała nr 2",
                Lists = new List<ListModel>()
                {
                    Lists[2]
                }
            },

        };

        public static List<ItemStatus> ItemStatus { get => _itemStatus; set => _itemStatus = value; }

        public static void AddData()
        {
            foreach (var status in ListStatus)
            {
                Repository.Save<ListStatus>(status);
            }
            foreach (var status in ItemStatus)
            {
                Repository.Save<ItemStatus>(status);
            }
            foreach (var list in Lists)
            {
                Repository.Save<ListModel>(list);
            }
            foreach (var item in Items)
            {
                Repository.Save<ItemModel>(item);
            }
        }
        public static void DropData()
        {
            Repository.DeleteAll<ItemStatus>();
            Repository.DeleteAll<ListStatus>();
            Repository.DeleteAll<ItemModel>();
            Repository.DeleteAll<ListModel>();
        }
    }
}