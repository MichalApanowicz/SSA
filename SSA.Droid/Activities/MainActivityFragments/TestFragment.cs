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
    public class TestFragment : Fragment
    {
        EditText outputText, nameText, descText, listIdText;
        Button addListButton, deleteListButton, addItemButton, deleteItemButton, getFromListButton, getListButton;

        ItemRepository _items = new ItemRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));
        ListRepository _lists = new ListRepository(new SQLiteConnection(new SQLitePlatformAndroid(), Constants.DatabasePath));

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.Test, null);

            addListButton = view.FindViewById<Button>(Resource.Id.addListButton);
            deleteListButton = view.FindViewById<Button>(Resource.Id.deleteListButton);
            addItemButton = view.FindViewById<Button>(Resource.Id.addItemButton);
            deleteItemButton = view.FindViewById<Button>(Resource.Id.deleteItemButton);
            getFromListButton = view.FindViewById<Button>(Resource.Id.getFromListButton);
            getListButton = view.FindViewById<Button>(Resource.Id.getListButton);


            outputText = view.FindViewById<EditText>(Resource.Id.outputText);
            nameText = view.FindViewById<EditText>(Resource.Id.editTextName);
            descText = view.FindViewById<EditText>(Resource.Id.editTextDesc);
            listIdText = view.FindViewById<EditText>(Resource.Id.editTextListId);

            try
            {
                if (_lists.GetAll().Count == 0) SampleData.AddData();
            }
            catch (Exception ex)
            {
                outputText.Text += ex.Message;
            }


            addItemButton.Click += (sender, e) =>
            {
                try
                {
                    var result =
                        _items.Save(new ItemModel()
                        {
                            Name = nameText.Text,
                            Description = descText.Text,
                            ListId = Int32.Parse(listIdText.Text)
                        }).ToString() + System.Environment.NewLine;
                    outputText.Text += result;
                }
                catch (Exception ex)
                {
                    outputText.Text += ex.Message;
                }
            };

            deleteListButton.Click += (sender, e) =>
            {
                try
                {
                    outputText.Text += _lists.Delete(Int32.Parse(listIdText.Text)) + System.Environment.NewLine;
                }
                catch (Exception ex)
                {
                    outputText.Text += ex.Message;
                }
            };

            deleteItemButton.Click += (sender, e) =>
            {
                try
                {
                    outputText.Text += _items.Delete(Int32.Parse(listIdText.Text)) + System.Environment.NewLine;
                }
                catch (Exception ex)
                {
                    outputText.Text += ex.Message;
                }
            };

            addListButton.Click += (sender, e) =>
            {
                try
                {
                    outputText.Text += _lists.Save(new ListModel() { Name = nameText.Text, Description = descText.Text }).ToString() + System.Environment.NewLine;
                }
                catch (Exception ex)
                {
                    outputText.Text += ex.Message;
                }
            };

            getListButton.Click += (sender, e) =>
            {
                try
                {
                    outputText.Text += _lists.Get(Int32.Parse(listIdText.Text)).ToString() + System.Environment.NewLine;
                }
                catch (Exception ex)
                {
                    outputText.Text += ex.Message;
                }
            };

            getFromListButton.Click += (sender, e) =>
            {
                try
                {
                    var list = _lists.Get(Int32.Parse(listIdText.Text));
                    outputText.Text = "";
                    foreach (var item in list.Items)
                    {
                        outputText.Text += item.ToString() + System.Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    outputText.Text += ex.Message;
                }
                //StartActivity(typeof(ItemListActivity));
            };
            return view;
        }
    }
}