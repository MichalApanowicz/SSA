﻿using System;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace SSA.Droid.Adapters
{
    class MainActivityFragmentAdapter : FragmentPagerAdapter
    {
        private readonly Fragment[] _fragments;
        private readonly string[] _tabNames;

        public MainActivityFragmentAdapter(FragmentManager fm, Fragment[] fragments, string[] tabNames)
                : base(fm)
        {
            _fragments = fragments;
            _tabNames = tabNames;
        }

        public override int Count => _fragments.Length;

        public override Fragment GetItem(int position)
        {
            return _fragments[position];
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(_tabNames[position]);
        }
    }
}
