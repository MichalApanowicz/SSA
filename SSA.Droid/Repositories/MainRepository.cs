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
    public class MainRepository
    {
        private readonly SQLiteConnection _db;

        public MainRepository(SQLiteConnection db)
        {
            _db = db;
            _db.CreateTable<PersonModel>();

            _db.CreateTable<ItemModel>();
            _db.CreateTable<ListModel>();

            _db.CreateTable<ItemStatus>();
            _db.CreateTable<ListStatus>();

            _db.CreateTable<Category>();
            _db.CreateTable<Localization>();
        }

        #region Common

        public T Save<T>(T obj)
        {
            _db.InsertWithChildren(obj);
            return obj;
        }

        public void Update<T>(T obj)
        {
            _db.UpdateWithChildren(obj);
        }

        public int Delete<T>(int id)
        {
            return _db.Delete<T>(id);
        }

        public int DeleteAll<T>()
        {
            _db.DropTable<T>();
            return _db.CreateTable<T>();
        }

        #endregion

        #region Items

        public ItemModel GetItem(int id)
        {
            return _db.GetWithChildren<ItemModel>(id);
        }

        public ItemModel GetItemByEanCode(string eanCode)
        {
            var itemId = _db.Find<ItemModel>(x => x.KodEAN == eanCode).ItemId;
            return GetItem(itemId);
        }

        public List<ItemModel> GetItemsFromList(int listId)
        {
            var itemsIds = GetList(listId).Items.Select(l => l.ItemId);
            var result = new List<ItemModel>();
            foreach (var id in itemsIds)
            {
                result.Add(GetItem(id));
            }
            return result;
        }

        public List<ItemModel> GetAllItemsWithCildren()
        {
            var result = _db.GetAllWithChildren<ItemModel>();

            return result;
        }

        #endregion

        #region Items Status

        public ItemStatus GetItemStatus(ItemStatusEnum status)
        {
            return GetItemStatus((int)status);
        }

        public ItemStatus GetItemStatus(int id)
        {
            return _db.Get<ItemStatus>(id);
        }

        public List<ItemStatus> GetAllItemsStatus()
        {
            var ids = _db.Query<ItemStatus>("Select ItemId From ItemStatus").Select(x => x.ItemStatusId);
            return ids.Select(id => _db.Get<ItemStatus>(id)).ToList();
        }
        #endregion

        #region Lists

        public ListModel GetList(int id)
        {
            var xa = _db.GetWithChildren<ListModel>(id);
            var xb = _db.Get<ListModel>(id);
            return xa;
        }

        public List<ListModel> GetAllLists()
        {
            var ids = _db.Query<ListModel>("Select ListId From ListModel").Select(x => x.ListId);

            return ids.Select(id => _db.GetWithChildren<ListModel>(id, true)).ToList();
        }

        public List<ListModel> GetAllListsWithCildren()
        {
            var result = _db.GetAllWithChildren<ListModel>();

            return result;
        }

        #endregion

        #region Lists Status

        public ListStatus GetListStatus(ListStatusEnum status)
        {
            return GetListStatus((int)status);
        }

        public ListStatus GetListStatus(int id)
        {
            return _db.Get<ListStatus>(id);
        }

        public List<ListStatus> GetAllListStatus()
        {
            var ids = _db.Query<ListStatus>("Select ListId From ListStatus").Select(x => x.ListStatusId);
            return ids.Select(id => _db.Get<ListStatus>(id)).ToList();
        }

        #endregion

        #region Persons

        public PersonModel GetPerson(int id)
        {
            return _db.Get<PersonModel>(id);
        }

        public PersonModel GetPerson(string name)
        {
            return _db.Find<PersonModel>(x => x.Name == name);
        }

        #endregion
    }
}