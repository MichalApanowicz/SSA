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
    class ListStatusRepository : IRepository<ListStatus>
    {
        private readonly SQLiteConnection _db;

        public ListStatusRepository(SQLiteConnection db)
        {
            _db = db;
            _db.CreateTable<ListStatus>();
        }

        public ListStatus Save(ListStatus ListStatus)
        {
            _db.Insert(ListStatus);
            return ListStatus;
        }

        public ListStatus Get(int id)
        {
            return _db.Get<ListStatus>(p => p.ListStatusId == id);
        }

        public List<ListStatus> GetFromList(int ListStatusId)
        {
            return _db.Query<ListStatus>("SELECT * From ListStatus WHERE ListStatusId = " + ListStatusId);
        }

        public int Delete(int id)
        {
            return _db.Delete<ListStatus>(id);
        }

        public List<ListStatus> GetAll()
        {
            List<ListStatus> result = new List<ListStatus>();
            var ids = _db.Query<ListStatus>("Select ItemId From ListStatus").Select(x => x.ListStatusId);
            foreach (var id in ids)
            {
                result.Add(_db.Get<ListStatus>(id));
            }
            return result;
        }
    }
}