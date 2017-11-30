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
using SQLite;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using SSA.Droid.Models;

namespace SSA.Droid.Repositories
{
    public class ItemRepository : IRepository<ItemModel>
    {
        private readonly SQLiteConnection _db;

        public ItemRepository(SQLiteConnection db)
        {
            _db = db;
            _db.CreateTable<ItemModel>();
        }

        public ItemModel Save(ItemModel itemModel)
        {
            _db.InsertWithChildren(itemModel);
            return itemModel;
        }

        public ItemModel Get(int id)
        {
            return _db.GetWithChildren<ItemModel>(id);
        }

        public List<ItemModel> GetFromList(int listId)
        {
            return _db.Query<ItemModel>("SELECT * From ItemModel WHERE ListId = "+listId);
        }

        public int Delete(int id)
        {
            return _db.Delete<ItemModel>(id);
        }

        public void DeleteItem(int listId, int itemId)
        {
            var list = _db.GetWithChildren<ListModel>(listId);
            var item = list.Items.FirstOrDefault(x => x.ItemId == itemId);
            list.Items.Remove(item);
            _db.UpdateWithChildren(list);
        }

        public List<ItemModel> GetAll()
        {
            List<ItemModel> result = new List<ItemModel>();
            var ids = _db.Query<ItemModel>("Select ItemId From ItemModel").Select(x => x.ItemId);
            foreach (var id in ids)
            {
                result.Add(_db.Get<ItemModel>(id));
            }
            return result;
        }
    }
}