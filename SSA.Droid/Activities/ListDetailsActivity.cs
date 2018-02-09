using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Interop;
using Newtonsoft.Json;
using SQLite;
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
            new MainRepository(new SQLiteConnection(Constants.DatabasePath));

        private ListModel _list;
        private List<ItemModel> _items;

        private EditText _eanCodeText;
        private RadioButton _getItemRadioButton, _deleteItemRadioButton;
        private Toolbar _toolbar, _secondToolbar;
        private RadioGroup _toolbarRadioGroup;
        private ArrayAdapter _adapter;

        private List<ItemModel> _selectedItems;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListDetailsActivity);
            var text = Intent.GetStringExtra("List");
            _list = JsonConvert.DeserializeObject<ListModel>(text) ?? new ListModel();
            _items = DataProvider.GetItemsFromList(_list.ListId);

            _getItemRadioButton = FindViewById<RadioButton>(Resource.Id.getItemRadioButton);
            _deleteItemRadioButton = FindViewById<RadioButton>(Resource.Id.deleteItemRadioButton);
            _getItemRadioButton.TextSize = 20;
            _deleteItemRadioButton.TextSize = 15;

            _eanCodeText = FindViewById<EditText>(Resource.Id.eanCodeEditText);
            _eanCodeText.TextChanged += EanTextChanged;

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _toolbar.Title = _list.Name;
            _toolbar.InflateMenu(Resource.Menu.listDetails_top_menu);
            SetActionBar(_toolbar);

            _secondToolbar = FindViewById<Toolbar>(Resource.Id.radioToolbar);
            _toolbarRadioGroup = FindViewById<RadioGroup>(Resource.Id.listDetailsRadioGroup);
            _toolbar.SetBackgroundColor(Color.DarkGreen);
            _secondToolbar.SetBackgroundColor(Color.DarkGreen);
            _toolbarRadioGroup.CheckedChange += (sender, e) =>
            {
                if (_getItemRadioButton.Checked)
                {
                    _getItemRadioButton.TextSize = 20;
                    _deleteItemRadioButton.TextSize = 15;
                    _secondToolbar.SetBackgroundColor(Color.DarkGreen);
                    _toolbar.SetBackgroundColor(Color.DarkGreen);
                }
                else if (_deleteItemRadioButton.Checked)
                {
                    _getItemRadioButton.TextSize = 15;
                    _deleteItemRadioButton.TextSize = 20;
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

            var selected = new List<int>();
            foreach (var item in _items)
            {
                if (item.Status.ItemStatusId == (int)ItemStatusEnum.Unavailable)
                {
                    selected.Add(item.ItemId);
                }
            }
            ListAdapter = new ItemsOnListDetailsAdapter(this, _items, selected);
            SetViewByListStatus();
        }

        private void SetViewByListStatus()
        {
            if (_list.ListStatusId == (int)ListStatusEnum.Uncommitted)
            {
                _getItemRadioButton.Text = "Dodaj";
                _deleteItemRadioButton.Text = "Usuń";
            }
            else if (_list.ListStatusId == (int)ListStatusEnum.Committed)
            {
                _getItemRadioButton.Text = "Pobierz";
                _deleteItemRadioButton.Text = "Zwróć";
            }
            else if (_list.ListStatusId == (int)ListStatusEnum.Terminated)
            {
                _eanCodeText.Visibility = ViewStates.Gone;
                _secondToolbar.Visibility = ViewStates.Gone;
                _toolbar.SetBackgroundColor(Color.DarkGray);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.listDetails_top_menu, menu);
            if (_list.ListStatusId == (int)ListStatusEnum.Terminated)
            {
                menu.FindItem(Resource.Id.menu_commitList).SetVisible(false);
                menu.FindItem(Resource.Id.menu_terminateList).SetVisible(false);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_commitList:
                    CommitList();
                    return true;

                case Resource.Id.menu_terminateList:
                    Toast.MakeText(this, $"{TerminateList()}",
                        ToastLength.Short).Show();
                    return true;
            }
            SetViewByListStatus();
            return base.OnOptionsItemSelected(item);
        }

        public void DialogWithAskForConnect()
        {
            if (Configuration.Online) return;

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Uwaga!");
            builder.SetMessage(
                "Możesz wykonać tę akcję tylko gdy jesteś połączony z siecią magazynu. \nCzy zaznaczyć że jesteś połączony?");
            builder.SetPositiveButton("Tak", (s, e) => { Configuration.Online = true; });
            builder.SetNegativeButton("Anuluj", (s, e) => { });
            builder.Show();
        }

        private void CommitList()
        {
            if (_list.ListStatusId == (int)ListStatusEnum.Uncommitted)
            {
                DialogWithAskForConnect();
                if (Configuration.Online)
                {
                    DataProvider.CommitList(_list);
                    Toast.MakeText(this, $"Zatwierdzono '{_list.Name}'",
                        ToastLength.Short).Show();
                    UpdateItemList();
                }

                //foreach (var item in _items)
                //{
                //    if (item.Status.ItemStatusId == (int) ItemStatusEnum.Available)
                //    {
                //        item.Status = _repository.GetItemStatus(ItemStatusEnum.Reserved);
                //        item.ItemStatusId = (int) ItemStatusEnum.Reserved;
                //    }
                //    else
                //    {
                //        item.ListId = 0;
                //    }
                //    DataProvider.UpdateItem(item);
                //}

                //_list.Status = _repository.GetListStatus(ListStatusEnum.Committed);
                //_list.ListStatusId = (int) ListStatusEnum.Committed;
                //_list.Items = _items;
                //DataProvider.CommitList(_list);
                //Toast.MakeText(this, $"Zatwierdzono '{_list.Name}'",
                //    ToastLength.Short).Show();
                
            }
            else if (_list.ListStatusId == (int)ListStatusEnum.Committed)
            {
                Toast.MakeText(this, "Lista jest już zatwierdzona!", ToastLength.Short).Show();
            }
            else if (_list.ListStatusId == (int)ListStatusEnum.Terminated)
            {
                Toast.MakeText(this, "Lista jest rozwiązana!", ToastLength.Short).Show();
            }
        }

        private string TerminateList()
        {


            //foreach (var item in _items)
            //{
            //    if (item.Status.ItemStatusId != (int)ItemStatusEnum.Reserved)
            //    {
            //        return "Zwróć wszystkie przedmioty!";
            //    }
            //}
            //foreach (var item in _items)
            //{

            //    item.Status = _repository.GetItemStatus(ItemStatusEnum.Available);
            //    _repository.Update(item);
            //}

            //_list.Status = _repository.GetListStatus(ListStatusEnum.Terminated);
            //_list.Items = _items;
            //_repository.Update(_list);

            //UpdateItemList();

            var url = Configuration.ApiPath + "lists/terminate/" + _list.ListId;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //_repository.Save<ListModel>(list);
            }


            return "Rozwiązano listę: " + _list.Name;
        }

        public void RefreshItems()
        {
            _items = DataProvider.GetItemsFromList(_list.ListId);
            foreach (var item in _items)
            {
                item.Category = _repository.GetCategory(item.CategoryId);
                item.Localization = _repository.GetLocalization(item.LocalizationId);
                item.Status = _repository.GetItemStatus(item.ItemStatusId);
            }
        }

        private void UpdateItemList()
        {
            RefreshItems();

            ListAdapter = new ItemsOnListDetailsAdapter(this, _items);
            ((BaseAdapter)ListAdapter).NotifyDataSetChanged();
        }

        private void EanTextChanged(object s, TextChangedEventArgs e)
        {
            try
            {
                var typedEan = _eanCodeText.Text;
                if (typedEan.Length >= 8)
                {
                    typedEan = typedEan.Substring(0, 8);
                    _eanCodeText.Text = typedEan;
                    var actionString = "";
                    var item = _repository.GetItemByEanCode(typedEan);
                    if (_getItemRadioButton.Checked)
                    {
                        if (_list.Status.ListStatusId == (int)ListStatusEnum.Uncommitted)
                        {
                            if (!_items.Select(x => x.ItemId).Contains(item.ItemId))
                            {
                                DataProvider.AddItemInList(item, _list);
                                actionString = "Dodano: ";
                            }
                        }
                        else if (_list.Status.ListStatusId == (int)ListStatusEnum.Committed)
                        {
                            if (_items.Select(x => x.ItemId).Contains(item.ItemId))
                            {
                                DataProvider.GetItemInList(item, _list);
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
                                DataProvider.RemoveItemInList(item, _list);
                                actionString = "Usunięto: ";
                            }
                        }
                        else if (_list.Status.ListStatusId == (int)ListStatusEnum.Committed)
                        {
                            if (item.Status.ItemStatusId ==
                                _repository.GetItemStatus(ItemStatusEnum.Unavailable).ItemStatusId)
                            {

                                DataProvider.ReturnItemInList(item, _list);
                                actionString = "Oddano: ";
                            }
                        }
                    }
                    _eanCodeText.ClearFocus();
                    _eanCodeText.SelectAll();

                    UpdateItemList();

                    Toast.MakeText(this, actionString + item.Name, ToastLength.Short).Show();
                }

            }
            catch (Exception ex)
            {
                _eanCodeText.ClearFocus();
                _eanCodeText.SelectAll();
                Toast.MakeText(this, "Nie znleziono przedmotu o tym kodzie", ToastLength.Short).Show();
            }
        }
    }
}