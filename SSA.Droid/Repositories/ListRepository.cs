using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    public class ListRepository : IRepository<ListModel>
    {
        private readonly SQLiteConnection _db;

        public ListRepository(SQLiteConnection db)
        {
            _db = db;
            _db.CreateTable<ListModel>();
        }

        public ListModel Save(ListModel listModel)
        {
            _db.InsertWithChildren(listModel);
            return listModel;
        }

        public ListModel Get(int id)
        {
            var xa = _db.GetWithChildren<ListModel>(id);
            var xb = _db.Get<ListModel>(id);
            return xa;
        }

        public int Delete(int id)
        {
            return _db.Delete<ListModel>(id);
        }

        public int DeleteAll()
        {
            _db.DropTable<ListModel>();
            return _db.CreateTable<ListModel>();
        }

        public List<ListModel> GetAll()
        {
            List<ListModel> result = new List<ListModel>();
            var ids = _db.Query<ListModel>("Select ListId From ListModel").Select(x => x.ListId);

            foreach (var id in ids)
            {
                result.Add(_db.GetWithChildren<ListModel>(id, true));
            }
            return result;
        }
    }
}