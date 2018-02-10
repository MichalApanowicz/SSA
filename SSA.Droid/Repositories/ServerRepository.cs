using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SQLiteNetExtensions.Extensions;
using SSA.Droid.Models;

namespace SSA.Droid.Repositories
{
    public static class ServerRepository
    {
        public static bool AddItemToList(ItemModel item, ListModel list)
        {
            var url = Configuration.ApiPath + "items/" + item.ItemId + "/addToList/" + list.ListId;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            return true;
        }

        public static bool UpdateItem(ItemModel item)
        {
            var id = item.ItemId;
            var url = Configuration.ApiPath + "items/" + id;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "PUT";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(item);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return true;
            }
        }

        public static bool UpdateList(ListModel list)
        {
            var id = list.ListId;
            var url = Configuration.ApiPath + "lists/" + id;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(list);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return true;
            }
        }

        public static ItemModel GetItem(int id)
        {
            var url = Configuration.ApiPath + "items/" + id;
            var json = "";

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));

            request.Method = "GET";
            Log.Debug("ApiCall", $"Request: {request}");
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    json = JsonValue.Load(stream).ToString();

                    Log.Debug("ApiCall", $"Response: {json}");
                }
            }
            return JsonConvert.DeserializeObject<ItemModel>(json);
        }

        public static List<ItemModel> GetItems()
        {
            var url = Configuration.ApiPath + "items";
            var json = "";

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));

            request.Method = "GET";
            Log.Debug("ApiCall", $"Request: {request}");
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    json = JsonValue.Load(stream).ToString();

                    Log.Debug("ApiCall", $"Response: {json}");
                }
            }
            return JsonConvert.DeserializeObject<List<ItemModel>>(json);
        }

        public static List<ItemModel> GetItemsFromList(int listId)
        {
            return GetItems().Where(i => i.ListId == listId).ToList();
        }

        public static ListModel AddNewList(ListModel list)
        {
            var url = Configuration.ApiPath + "lists/new";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(list);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var insertedList = JsonConvert.DeserializeObject<ListModel>(result);
                return insertedList;
            }
        }

        public static List<ListModel> GetLists()
        {
            var url = Configuration.ApiPath + "lists";
            var json = "";

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));

            request.Method = "GET";
            Log.Debug("ApiCall", $"Request: {request}");
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    json = JsonValue.Load(stream).ToString();

                    Log.Debug("ApiCall", $"Response: {json}");
                }
            }
            return JsonConvert.DeserializeObject<List<ListModel>>(json);
        }

        public static void TerminateList(ListModel list)
        {
            var url = Configuration.ApiPath + "lists/terminate/" + list.ListId;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public static void CommitList(ListModel list)
        {
            var url = Configuration.ApiPath + "lists/commit/" + list.ListId;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public static PersonModel GetPerson(int id)
        {
            var url = Configuration.ApiPath + "persons/" + id;
            var json = "";

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));

            request.Method = "GET";
            Log.Debug("ApiCall", $"Request: {request}");
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    json = JsonValue.Load(stream).ToString();

                    Log.Debug("ApiCall", $"Response: {json}");
                }
            }
            return JsonConvert.DeserializeObject<PersonModel>(json);
        }

        public static PersonModel GetPerson(string name)
        {
            var url = Configuration.ApiPath + "persons/" + name.Replace(" ", "%20");
            var json = "";

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));

            request.Method = "GET";
            Log.Debug("ApiCall", $"Request: {request}");
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    json = JsonValue.Load(stream).ToString();

                    Log.Debug("ApiCall", $"Response: {json}");
                }
            }
            return JsonConvert.DeserializeObject<PersonModel>(json);
        }

        public static PersonModel SavePerson(PersonModel person)
        {
            var url = Configuration.ApiPath + "persons/new";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(person);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var insertedPerson = JsonConvert.DeserializeObject<PersonModel>(result);
                return insertedPerson;
            }
        }
    }


}