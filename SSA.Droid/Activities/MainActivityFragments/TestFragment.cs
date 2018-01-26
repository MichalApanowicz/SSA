using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities.MainActivityFragments
{
    public class TestFragment : Android.Support.V4.App.Fragment
    {
        private EditText _outputText, _nameText, _descText, _listIdText, _eanText, _statusText, _apiUrl;
        private Button _addListButton, _deleteListButton, _addItemButton, _deleteItemButton, _getFromListButton, _getListButton, _apiCallButton;

        private MainRepository _repository;
      
        private TestFragment() { }

        public static TestFragment NewInstance(MainRepository repository)
        {
            var fragment = new TestFragment
            {
                _repository = repository
            };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.Test, null);

            _addListButton = view.FindViewById<Button>(Resource.Id.addListButton);
            _deleteListButton = view.FindViewById<Button>(Resource.Id.deleteListButton);
            _addItemButton = view.FindViewById<Button>(Resource.Id.addItemButton);
            _deleteItemButton = view.FindViewById<Button>(Resource.Id.deleteItemButton);
            _getFromListButton = view.FindViewById<Button>(Resource.Id.getFromListButton);
            _getListButton = view.FindViewById<Button>(Resource.Id.getListButton);
            _apiCallButton = view.FindViewById<Button>(Resource.Id.callApi);

            _outputText = view.FindViewById<EditText>(Resource.Id.outputText);
            _nameText = view.FindViewById<EditText>(Resource.Id.editTextName);
            _descText = view.FindViewById<EditText>(Resource.Id.editTextDesc);
            _listIdText = view.FindViewById<EditText>(Resource.Id.editTextListId);
            _statusText = view.FindViewById<EditText>(Resource.Id.editTextStatus);
            _eanText = view.FindViewById<EditText>(Resource.Id.editTextEan);
            _apiUrl = view.FindViewById<EditText>(Resource.Id.editApiUrl);

            try
            {
                if (_repository.GetAllItemsWithCildren().Count == 0) SampleData.AddData();
            }
            catch (Exception ex)
            {
                _outputText.Text += ex.Message;
            }

            _apiCallButton.Click += (sender, e) =>
            {
                try
                {
                    var url = _apiUrl.Text;
                    var x = new List<ItemModel>();
                    var json = "";

                    var request = (HttpWebRequest) WebRequest.Create(new Uri(url));
                   // request.ContentType = "application/json";
                    request.Method = "GET";
                    Log.Debug("ApiCall", $"Request: {request}");
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            json = JsonValue.Load(stream).ToString();
                            _outputText.Text += json + System.Environment.NewLine;
                            Log.Debug("ApiCall", $"Response: {json}");
                        }
                    }

                    x = JsonConvert.DeserializeObject<List<ItemModel>>(json);
                }
                catch (Exception ex)
                {
                    Log.Debug("ApiCall", $"Exception: {ex}");

                    _outputText.Text += ex.ToString();
                }
            };

            _addItemButton.Click += (sender, e) =>
            {
                try
                {
                    var result =
                        _repository.Save<ItemModel>(new ItemModel()
                        {
                            Name = _nameText.Text,
                            Description = _descText.Text,
                            ListId = int.Parse(_listIdText.Text),
                            KodEAN = _eanText.Text,
                            Status = _repository.GetItemStatus(int.Parse(_statusText.Text)),
                            ItemStatusId = int.Parse(_statusText.Text),
                        }).ToString() + System.Environment.NewLine;
                    _outputText.Text += result;
                }
                catch (Exception ex)
                {
                    _outputText.Text += ex.Message;
                }
            };

            _deleteListButton.Click += (sender, e) =>
            {
                try
                {
                    _outputText.Text += _repository.Delete<ListModel>(int.Parse(_listIdText.Text)) + System.Environment.NewLine;
                }
                catch (Exception ex)
                {
                    _outputText.Text += ex.Message;
                }
            };

            _deleteItemButton.Click += (sender, e) =>
            {
                try
                {
                    _outputText.Text += _repository.Delete<ItemModel>(int.Parse(_listIdText.Text)) + System.Environment.NewLine;
                }
                catch (Exception ex)
                {
                    _outputText.Text += ex.Message;
                }
            };

            _addListButton.Click += (sender, e) =>
            {
                try
                {
                    _outputText.Text += _repository.Save<ListModel>(new ListModel()
                    {
                        Name = _nameText.Text,
                        Description = _descText.Text,
                        ListStatusId = int.Parse(_statusText.Text),
                        Status = _repository.GetListStatus(int.Parse(_statusText.Text)),
                        Items = new List<ItemModel>(),
                        CreateDate = DateTime.Now.ToLongDateString(),
                        PersonId = 1
                    }).ToString() + System.Environment.NewLine;
                }
                catch (Exception ex)
                {
                    _outputText.Text += ex.Message;
                }
            };

            _getListButton.Click += (sender, e) =>
            {
                try
                {
                    _outputText.Text += _repository.GetList(int.Parse(_listIdText.Text)).ToString() + System.Environment.NewLine;
                }
                catch (Exception ex)
                {
                    _outputText.Text += ex.Message;
                }
            };

            _getFromListButton.Click += (sender, e) =>
            {
                try
                {
                    var list = _repository.GetItemsFromList(int.Parse(_listIdText.Text));
                    _outputText.Text = "";
                    foreach (var item in list)
                    {
                        _outputText.Text += item.ToString() + System.Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    _outputText.Text += ex.Message;
                }
                //StartActivity(typeof(ItemListActivity));
            };
            return view;
        }
    }
}