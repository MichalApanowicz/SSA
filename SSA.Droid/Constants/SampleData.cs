using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
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

        

        private static readonly List<PersonModel> Persons = new List<PersonModel>()
        {
            new PersonModel()
            {
                Name = "Michal Apanowicz",
                Description = "Administrator",
                Lists = new List<ListModel>()
            },
            new PersonModel()
            {
                Name = "GetUserName()",
                Description = "Pobrane dane",
                Lists = new List<ListModel>()
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
                Name = "Na biwak",
                Description = "Rzeczy do zabrania na biwak w Ostromecku",
                CreateDate = DateTime.Parse(DateTime.Parse("2017-12-14").ToLongDateString()).ToLongDateString(),
                Person = Persons[0],
                Status = ListStatus[0],
                Items = new List<ItemModel>
                {

                },
            },
            new ListModel()
            {
                Name = "Lista obozowa 1",
                Description = "Rzeczy zabrane na kwaterkę",
                CreateDate = DateTime.Parse("2017-12-14").ToLongDateString(),
                Person = Persons[0],
                Status = ListStatus[0],
                Items = new List<ItemModel>
                {

                },
            },
            new ListModel()
            {
                Name = "Lista Jastrzębi",
                Description = "Rzeczy zabrane na zbiórkę",
                CreateDate = DateTime.Parse("2017-12-14").ToLongDateString(),
                Person = Persons[0],
                Status = ListStatus[0],
                Items = new List<ItemModel>
                {

                },
            }
        };

        public static List<ItemStatus> ItemStatus = new List<ItemStatus>()
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

        private static readonly List<ItemModel> Items = new List<ItemModel>
        {
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000017",
                Name = "Młotek",
                Description = "500g żółty",
                ListId = 1
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000024",
                Name = "Siekiera",
                Description = "kuta ręcznie",
                
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000031",
                Name = "Gwoździe calowe",
                Description = "5kg",
                ListId = 1
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000048",
                Name = "Młotek",
                Description = "250g żółty",
                ListId = 1
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000055",
                Name = "Łopata",
                Description = "Fiskars",
                
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000079",
                Name = "Dołownik",
                Description = "Romanik",
                
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000093",
                Name = "Plandeka",
                Description = "9m^2",
                
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000109",
                Name = "Plandeka",
                Description = "3m^2",
                
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000116",
                Name = "Plandeka",
                Description = "2m^2",
                
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000123",
                Name = "Latarka",
                Description = "duża",
                },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000130",
                Name = "Skrzynka narzędziowa",
                Description = "niebieska",
              
            },
            new ItemModel()
            {
                Status = ItemStatus.First(x => x.ItemStatusId == (int)ItemStatusEnum.Available),
                KodEAN = "00000147",
                Name = "Apteczka",
                Description = "mała nr 2",
               
            },

        };

       

        public static void AddData()
        {
            foreach (var person in Persons)
            {
                Repository.Save<PersonModel>(person);
            }
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
            Repository.DeleteAll<PersonModel>();
            Repository.DeleteAll<ItemStatus>();
            Repository.DeleteAll<ListStatus>();
            Repository.DeleteAll<ItemModel>();
            Repository.DeleteAll<ListModel>();
        }
    }
}