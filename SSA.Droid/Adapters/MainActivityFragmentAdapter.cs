using System;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace SSA.Droid.Adapters
{
    class MainActivityFragmentAdapter : FragmentPagerAdapter
    {
        private Android.Support.V4.App.Fragment[] _fragments;
        private string[] _tabNames;

        public MainActivityFragmentAdapter(Android.Support.V4.App.FragmentManager fm, Android.Support.V4.App.Fragment[] fragments, string[] tabNames)
                : base(fm)
        {
            _fragments = fragments;
            _tabNames = tabNames;
        }

        public override int Count => _fragments.Length;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return _fragments[position];
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(_tabNames[position]);
        }
    }
}
