using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using SSA.Droid.Models;

namespace SSA.Droid.Repositories
{

    public class DataProvider
    {
        public static MainRepository LocalData = new MainRepository(new SQLiteConnection(Constants.DatabasePath));
        private static bool _needRefresh = true;

        private static List<ListModel> _unsavedLists = new List<ListModel>();

        public static void UpdateItemsAndLists()
        {
            var items = GetItems();
            var lists = GetLists();

            LocalData.DeleteAll<ItemModel>();
            LocalData.DeleteAll<ListModel>();

            LocalData.SaveAll(items);
            LocalData.SaveAll(lists);
        }

        public static void UpdateItem(ItemModel item)
        {
            LocalData.Update(item);
            ServerRepository.UpdateItem(item);
        }

        public static void UpdateList(ListModel list)
        {
            LocalData.Update(list);
            ServerRepository.UpdateList(list);
        }

        public static ItemModel GetItem(int itemId)
        {
            var item = ServerRepository.GetItem(itemId);

            item.Category = LocalData.GetCategory(item.CategoryId);
            item.Localization = LocalData.GetLocalization(item.LocalizationId);
            item.Status = LocalData.GetItemStatus(item.ItemStatusId);

            return item;
        }

        public static ItemModel GetItemFromLocal(int itemId)
        {
            var item = LocalData.GetItem(itemId);

            item.Category = LocalData.GetCategory(item.CategoryId);
            item.Localization = LocalData.GetLocalization(item.LocalizationId);
            item.Status = LocalData.GetItemStatus(item.ItemStatusId);

            return item;
        }

        public static List<ItemModel> GetItems()
        {
            if (Configuration.Online)
            {
                var items = ServerRepository.GetItems();
                foreach (var item in items)
                {
                    item.Category = LocalData.GetCategory(item.CategoryId);
                    item.Localization = LocalData.GetLocalization(item.LocalizationId);
                    item.Status = LocalData.GetItemStatus(item.ItemStatusId);
                }
                LocalData.DeleteAll<ItemModel>();
                LocalData.SaveAll(items);

                return items;
            }
            else
            {
                return GetItemsFromLocal();
            }
        }

        private static List<ItemModel> GetItemsFromLocal()
        {
            return LocalData.GetAllItemsWithCildren();
        }

        private static List<ListModel> GetListsFromLocal()
        {
            var lists = LocalData.GetAllLists();
            var items = LocalData.GetAllItemsWithCildren();
            foreach (var list in lists)
            {
                list.Person = GetPersonLocal(list.PersonId);
                list.Status = LocalData.GetListStatus(list.ListStatusId);
                list.Items = items.Where(i => i.ListId == list.ListId).ToList();
            }
            return lists;
        }

        public static void AddItemInList(ItemModel item, ListModel list)
        {
            item.ListId = list.ListId;

            if (!_unsavedLists.First(l => l.ListId == list.ListId)
                .Items.Exists(i => i.ItemId == item.ItemId))
            {
                _unsavedLists.First(l => l.ListId == list.ListId).Items.Add(item);
            }
            
            LocalData.Update(item);
        }

        public static void GetItemInList(ItemModel item, ListModel list)
        {
            item.Status = LocalData.GetItemStatus(ItemStatusEnum.Unavailable);

            LocalData.Update(item);
            ServerRepository.UpdateItem(item);
        }

        public static void RemoveItemInList(ItemModel item, ListModel list)
        {
            var items = _unsavedLists.First(l => l.ListId == list.ListId)
                .Items;
            try
            {
                _unsavedLists.First(l => l.ListId == list.ListId)
                    .Items.Remove(items.First(i => i.ItemId == item.ItemId));
            } catch { }

            item.ListId = 0;
            LocalData.Update(item);
        }

        public static void ReturnItemInList(ItemModel item, ListModel list)
        {
            item.Status = LocalData.GetItemStatus(ItemStatusEnum.Reserved);

            LocalData.Update(item);
            ServerRepository.UpdateItem(item);
        }

        public static void AddNewListLocal(ListModel list)
        {
            list = LocalData.Save(list);
            _unsavedLists.Add(list);
        }

        public static List<ItemModel> GetItemsFromList(int listId)
        {
            if (Configuration.Online)
            {
                var items = new List<ItemModel>();
                if (_unsavedLists.Exists(l => l.ListId == listId))
                {
                    items = LocalData.GetList(listId).Items;
                }
                else
                {
                    items = ServerRepository.GetItemsFromList(listId);
                }
                foreach (var item in items)
                {
                    item.Category = LocalData.GetCategory(item.CategoryId);
                    item.Localization = LocalData.GetLocalization(item.LocalizationId);
                    item.Status = LocalData.GetItemStatus(item.ItemStatusId);
                }
                return items;
            }
            else
            {
                var items = LocalData.GetAllItemsWithCildren().Where(i => i.ListId == listId).ToList();
                foreach (var item in items)
                {
                    item.Category = LocalData.GetCategory(item.CategoryId);
                    item.Localization = LocalData.GetLocalization(item.LocalizationId);
                    item.Status = LocalData.GetItemStatus(item.ItemStatusId);
                }
                return items;
            }
            
        }

        public static List<ListModel> GetLists()
        {
            if (Configuration.Online)
            {
                var lists = ServerRepository.GetLists();
                var items = ServerRepository.GetItems();
                foreach (var list in lists)
                {
                    list.Person = GetPerson(list.PersonId);
                    list.Status = LocalData.GetListStatus(list.ListStatusId);
                    list.Items = items.Where(l => l.ListId == list.ListId).ToList();
                    if (_unsavedLists.Exists(l => l.ListId == list.ListId))
                    {
                        list.Items = LocalData.GetList(list.ListId).Items;
                    }
                }
                lists.AddRange(_unsavedLists);

                LocalData.DeleteAll<ListModel>();
                LocalData.SaveAll(lists);

                return lists;
            }
            else
            {
                return GetListsFromLocal();
            }
        }

        public static bool AddNewList(ListModel list)
        {
            try
            {
                ServerRepository.AddNewList(list);
                foreach (var item in list.Items)
                {
                    item.ListId = list.ListId;
                    UpdateItem(item);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static void CommitList(ListModel list)
        {
            var person = LocalData.GetPerson(list.PersonId);
            var remotePerson = ServerRepository.GetPerson(person.Name) ?? ServerRepository.SavePerson(person);
            list.PersonId = remotePerson.PersonId;

            var result = AddNewList(list);
            try
            {
                var check = _unsavedLists.Remove(_unsavedLists.First(l => l.ListId == list.ListId));
            }
            catch { }

            ServerRepository.CommitList(list);
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
            return LocalData.GetPerson(name);
        }

        public static PersonModel SavePerson(PersonModel person)
        {
            return ServerRepository.SavePerson(person);
        }

        public static PersonModel GetPersonLocal(int personId)
        {
            return LocalData.GetPerson(personId);
        }

        public static PersonModel GetPersonLocal(string name)
        {
            return LocalData.GetPerson(name);
        }

        public static PersonModel SavePersonLocal(PersonModel person)
        {
            return LocalData.Save(person);
        }
    }
}