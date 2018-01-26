using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
<<<<<<< HEAD
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Interop;
=======
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SSA.Droid.Activities.MainActivityFragments;
using SSA.Droid.Adapters;
using SSA.Droid.Models;
using SSA.Droid.Repositories;

namespace SSA.Droid.Activities
{
    [Activity(Label = "ListDetailsActivity", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class ListDetailsActivity : ListActivity
    {
        private readonly MainRepository _repository =
            new MainRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        private ListModel _list;
        private List<ItemModel> _items;

<<<<<<< HEAD
        private EditText _eanCodeText;
        private RadioButton _getItemRadioButton, _deleteItemRadioButton;
        private Toolbar _toolbar, _secondToolbar;
        private RadioGroup _toolbarRadioGroup;

=======
        private Toolbar _toolbar;
        //private TextView _person, _createDate;
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        private ArrayAdapter _adapter;

        private List<ItemModel> _selectedItems;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListDetailsActivity);
            var text = Intent.GetStringExtra("List");
            _list = JsonConvert.DeserializeObject<ListModel>(text) ?? new ListModel();
            _items = _repository.GetItemsFromList(_list.ListId);

<<<<<<< HEAD
            _getItemRadioButton = FindViewById<RadioButton>(Resource.Id.getItemRadioButton);
            _deleteItemRadioButton = FindViewById<RadioButton>(Resource.Id.deleteItemRadioButton);
            _eanCodeText = FindViewById<EditText>(Resource.Id.eanCodeEditText);
            _eanCodeText.TextChanged += EanTextChanged;
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _toolbar.Title = _list.Name;
            _toolbar.InflateMenu(Resource.Menu.listDetails_top_menu);
            SetActionBar(_toolbar);

<<<<<<< HEAD
            _secondToolbar = FindViewById<Toolbar>(Resource.Id.radioToolbar);
            _toolbarRadioGroup = FindViewById<RadioGroup>(Resource.Id.listDetailsRadioGroup);
            _toolbar.SetBackgroundColor(Color.DarkGreen);
            _secondToolbar.SetBackgroundColor(Color.DarkGreen);
            _toolbarRadioGroup.CheckedChange += (sender, e) =>
            {
                if (_getItemRadioButton.Checked)
                {
                    _secondToolbar.SetBackgroundColor(Color.DarkGreen);
                    _toolbar.SetBackgroundColor(Color.DarkGreen);
                }
                else if (_deleteItemRadioButton.Checked)
                {
                    _secondToolbar.SetBackgroundColor(Color.OrangeRed);
                    _toolbar.SetBackgroundColor(Color.OrangeRed);
                }
            };
            _secondToolbar.Click += (sender, e) =>
            {
                if (((RadioButton)_toolbarRadioGroup.GetChildAt(0)).Checked)
                {
                    _toolbarRadioGroup.ClearCheck();
                    _toolbarRadioGroup.Check(((RadioButton)_toolbarRadioGroup.GetChildAt(1)).Id);
                }
                else
                {
                    _toolbarRadioGroup.ClearCheck();
                    _toolbarRadioGroup.Check(((RadioButton)_toolbarRadioGroup.GetChildAt(0)).Id);
                }
            };

=======
            //_person = FindViewById<TextView>(Resource.Id.ListPersonText);
            //_createDate = FindViewById<TextView>(Resource.Id.ListDateText);

            //_person.Text = _list.Person;
            //_createDate.Text = DateTime.Parse(_list.CreateDate).ToLongDateString();

            //_adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked,
            //  objects: _items.Select(x => x.Name).ToArray());

            //ListAdapter = _adapter;
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
            var selected = new List<int>();
            foreach (var item in _items)
            {
                if (item.Status.ItemStatusId == (int)ItemStatusEnum.Unavailable)
                {
                    selected.Add(item.ItemId);
                }
            }
<<<<<<< HEAD
            ListAdapter = new ItemsOnListDetailsAdapter(this, _items, selected);
=======
            ListAdapter = new AllItemsAdapter(this, _items, selected);
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.listDetails_top_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_commitList:
                    CommitList();
                    Toast.MakeText(this, $"Zatwierdzono '{_list.Name}'",
                        ToastLength.Short).Show();
                    return true;

                case Resource.Id.menu_terminateList:
<<<<<<< HEAD
                    Toast.MakeText(this, $"{TerminateList()}",
=======
                    Toast.MakeText(this, $"Action selected: {item.TitleFormatted}",
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
                        ToastLength.Short).Show();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void CommitList()
        {
<<<<<<< HEAD
            foreach (var item in _items)
            {
                if (item.Status.ItemStatusId == (int)ItemStatusEnum.Available)
                {
                    item.Status = _repository.GetItemStatus(ItemStatusEnum.Reserved);
                    _repository.Update(item);
                }
            }

            _list.Status = _repository.GetListStatus(ListStatusEnum.Committed);
            _list.Items = _items;
            _repository.Update(_list);

            UpdateItemList();
        }

        private string TerminateList()
        {
            foreach (var item in _items)
            {
                if (item.Status.ItemStatusId != (int)ItemStatusEnum.Reserved)
                {
                    return "Zwróć wszystkie przedmioty!";
                }
            }
            foreach (var item in _items)
            {

                item.Status = _repository.GetItemStatus(ItemStatusEnum.Available);
                _repository.Update(item);
            }

            _list.Status = _repository.GetListStatus(ListStatusEnum.Terminated);
=======

            foreach (var item in _items)
            {
                    if (item.Status.ItemStatusId == (int) ItemStatusEnum.Available)
                    {
                        item.Status = _repository.GetItemStatus(ItemStatusEnum.Reserved);
                        _repository.Update(item);
                    }
            }

            _list.Status = _repository.GetListStatus(ListStatusEnum.Committed);
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
            _list.Items = _items;
            _repository.Update(_list);

            UpdateItemList();
<<<<<<< HEAD
            return "Rozwiązano listę: " + _list.Name;
=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        }

        private void UpdateItemList()
        {
            _items = _repository.GetItemsFromList(_list.ListId);
<<<<<<< HEAD
            ListAdapter = new ItemsOnListDetailsAdapter(this, _items);
            ((BaseAdapter)ListAdapter).NotifyDataSetChanged();
        }

        private void EanTextChanged(object s, TextChangedEventArgs e)
        {
            try
            {
                var typedEan = _eanCodeText.Text;
                if (typedEan.Length == 8)
                {
                    var actionString = "";
                    var item = _repository.GetItemByEanCode(typedEan);
                    if (_getItemRadioButton.Checked)
                    {
                        if (_list.Status.ListStatusId == (int)ListStatusEnum.Uncommitted)
                        {
                            if (!_items.Select(x => x.ItemId).Contains(item.ItemId))
                            {
                                item.ListId = _list.ListId;
                                actionString = "Dodano: ";
                            }
                            _repository.Update(item);
                        }
                        else if (_list.Status.ListStatusId == (int)ListStatusEnum.Committed)
                        {
                            if (_items.Select(x => x.ItemId).Contains(item.ItemId))
                            {
                                item.Status = _repository.GetItemStatus(ItemStatusEnum.Unavailable);
                                actionString = "Pobrano: ";
                            }
                        }
                    }
                    else if (_deleteItemRadioButton.Checked)
                    {
                        if (_list.Status.ListStatusId == (int)ListStatusEnum.Uncommitted)
                        {
                            if (item.Status.ItemStatusId ==
                                _repository.GetItemStatus(ItemStatusEnum.Available).ItemStatusId)
                            {
                                item.ListId = 0;
                                actionString = "Usunięto: ";
                            }
                        }
                        else if (_list.Status.ListStatusId == (int)ListStatusEnum.Committed)
                        {
                            if (item.Status.ItemStatusId ==
                                _repository.GetItemStatus(ItemStatusEnum.Unavailable).ItemStatusId)
                            {
                                item.Status = _repository.GetItemStatus(ItemStatusEnum.Reserved);

                                actionString = "Oddano: ";
                            }
                        }
                    }
                    _repository.Update(item);
                    UpdateItemList();
                    Toast.MakeText(this, actionString + item.Name, ToastLength.Short).Show();
                    _eanCodeText.ClearFocus();
                    _eanCodeText.SelectAll();
                    _eanCodeText.FocusedByDefault = true;
                    
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Nie znleziono przedmotu o tym kodzie", ToastLength.Long).Show();
            }
=======
            ListAdapter = new AllItemsAdapter(this, _items);
            ((BaseAdapter)ListAdapter).NotifyDataSetChanged();
        }

        private List<ItemModel> GetSelectedItems()
        {
            _selectedItems.Clear();

            var selected = ((AllItemsAdapter)ListAdapter).GetSelectedRows();
            foreach (var i in selected)
            {
                _selectedItems.Add(_items.First(x => x.ItemId == i));
            }
            _adapter.NotifyDataSetChanged();
            Log.Debug("ListDetailsActivity", $"_selectedItems[{_selectedItems.Count}]: {JsonConvert.SerializeObject(_selectedItems, Formatting.Indented)}");

            return _selectedItems;
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        }
    }
}