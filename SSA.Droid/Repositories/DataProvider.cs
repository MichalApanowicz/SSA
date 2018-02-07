using System;
using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Models;

namespace SSA.Droid.Repositories
{

    public class DataProvider
    {
        public static MainRepository LocalData = new MainRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        private static bool _needRefresh = true;


        public static void UpdateItemsAndLists()
        {
                var items = GetItems();
                var lists = GetLists();

                LocalData.DeleteAll<ItemModel>();
                LocalData.DeleteAll<ListModel>();

                LocalData.SaveAll(items);
                LocalData.SaveAll(lists);
        }

        public static ItemModel GetItem(int itemId)
        {
            var item = ServerRepository.GetItem(itemId);

            item.Category = LocalData.GetCategory(item.CategoryId);
            item.Localization = LocalData.GetLocalization(item.LocalizationId);
            item.Status = LocalData.GetItemStatus(item.ItemStatusId);

            return item;
        }

        public static List<ItemModel> GetItems()
        {
            var items = ServerRepository.GetItems();
            foreach (var item in items)
            {
                item.Category = LocalData.GetCategory(item.CategoryId);
                item.Localization = LocalData.GetLocalization(item.LocalizationId);
                item.Status = LocalData.GetItemStatus(item.ItemStatusId);
            }

            return items;
        }

        public static List<ItemModel> GetItemsFromLocal()
        {
            return LocalData.GetAllItemsWithCildren();
        }

        public static List<ListModel> GetListsFromLocal()
        {
            var lists = LocalData.GetAllLists();
            foreach (var list in lists)
            {
                list.Person = GetPerson(list.PersonId);
            }
            return lists;
        }

        public static bool AddItemToList(ItemModel item, ListModel list)
        {
            ServerRepository.AddItemToList(item, list);
            LocalData.Update(item);
            _needRefresh = true;
            return true;
        }

        public static bool AddNewList(ListModel list)
        {
            try
            {
                list.ListId = ServerRepository.AddNewList(list).ListId;
                foreach (var item in list.Items)
                {
                    AddItemToList(item, list);
                }
                LocalData.Save(list);
                _needRefresh = true;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static List<ItemModel> GetItemsFromList(int listId)
        {
            var items = ServerRepository.GetItemsFromList(listId);
            foreach (var item in items)
            {
                item.Category = LocalData.GetCategory(item.CategoryId);
                item.Localization = LocalData.GetLocalization(item.LocalizationId);
                item.Status = LocalData.GetItemStatus(item.ItemStatusId);
            }
            return items;
        }

        public static List<ListModel> GetLists()
        {
            var lists = ServerRepository.GetLists();
            foreach (var list in lists)
            {
                list.Person = GetPerson(list.PersonId);
                list.Status = LocalData.GetListStatus(list.ListStatusId);
                list.Items = GetItemsFromList(list.ListId);
            }

            return lists;
        }

        public static bool TerminateList(ListModel list)
        {
            try
            {
                ServerRepository.TerminateList(list);
            }
            catch (Exception e)
            {
                return false;
            }
            LocalData.DeleteAll<ListModel>();
            LocalData.SaveAll(GetLists());
            return true;
        }

        public static PersonModel GetPerson(int personId)
        {
            return ServerRepository.GetPerson(personId);
        }

        public static PersonModel GetPerson(string name)
        {
            return ServerRepository.GetPerson(name);
        }

        public static PersonModel SavePerson(PersonModel person)
        {
            LocalData.Save(person);
            return ServerRepository.SavePerson(person);
        }
    }
}