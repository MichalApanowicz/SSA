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
using SQLiteNetExtensions.Extensions;
using SSA.Droid.Models;

namespace SSA.Droid.Repositories
{
    class ItemStatusRepository : IRepository<ItemStatus>
    {
        private readonly SQLiteConnection _db;

        public ItemStatusRepository(SQLiteConnection db)
        {
            _db = db;
            _db.CreateTable<ItemStatus>();
        }

        public ItemStatus Save(ItemStatus itemStatus)
        {
            _db.Insert(itemStatus);
            return itemStatus;
        }

        public ItemStatus Get(int id)
        {
            return _db.Get<ItemStatus>(p => p.ItemStatusId == id);
        }

        public List<ItemStatus> GetFromList(int itemStatusId)
        {
            return _db.Query<ItemStatus>("SELECT * From ItemStatus WHERE ItemStatusId = " + itemStatusId);
        }

        public int Delete(int id)
        {
            return _db.Delete<ItemStatus>(id);
        }

        public List<ItemStatus> GetAll()
        {
            List<ItemStatus> result = new List<ItemStatus>();
            var ids = _db.Query<ItemStatus>("Select ItemId From ItemStatus").Select(x => x.ItemStatusId);
            foreach (var id in ids)
            {
                result.Add(_db.Get<ItemStatus>(id));
            }
            return result;
        }
    }
}