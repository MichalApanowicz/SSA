using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities.MainActivityFragments
{
    public class TestFragment : Android.Support.V4.App.Fragment
    {
        private EditText _outputText, _nameText, _descText, _listIdText;
        private Button _addListButton, _deleteListButton, _addItemButton, _deleteItemButton, _getFromListButton, _getListButton;

        private ItemRepository _itemRepository;
        private ListRepository _listRepository;

        private TestFragment() { }

        public static TestFragment NewInstance(ListRepository listRepository, ItemRepository itemRepository)
        {
            var fragment = new TestFragment
            {
                _listRepository = listRepository,
                _itemRepository = itemRepository
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


            _outputText = view.FindViewById<EditText>(Resource.Id.outputText);
            _nameText = view.FindViewById<EditText>(Resource.Id.editTextName);
            _descText = view.FindViewById<EditText>(Resource.Id.editTextDesc);
            _listIdText = view.FindViewById<EditText>(Resource.Id.editTextListId);

            try
            {
                if (_listRepository.GetAll().Count == 0) SampleData.AddData();
            }
            catch (Exception ex)
            {
                _outputText.Text += ex.Message;
            }


            _addItemButton.Click += (sender, e) =>
            {
                try
                {
                    var result =
                        _itemRepository.Save(new ItemModel()
                        {
                            Name = _nameText.Text,
                            Description = _descText.Text,
                            ListId = Int32.Parse(_listIdText.Text)
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
                    _outputText.Text += _listRepository.Delete(Int32.Parse(_listIdText.Text)) + System.Environment.NewLine;
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
                    _outputText.Text += _itemRepository.Delete(Int32.Parse(_listIdText.Text)) + System.Environment.NewLine;
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
                    _outputText.Text += _listRepository.Save(new ListModel() { Name = _nameText.Text, Description = _descText.Text }).ToString() + System.Environment.NewLine;
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
                    _outputText.Text += _listRepository.Get(Int32.Parse(_listIdText.Text)).ToString() + System.Environment.NewLine;
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
                    var list = _listRepository.Get(Int32.Parse(_listIdText.Text));
                    _outputText.Text = "";
                    foreach (var item in list.Items)
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