using System;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace SSA.Droid.Adapters
{
    class MainActivityFragmentAdapter : FragmentPagerAdapter
    {
        private Android.Support.V4.App.Fragment[] _fragments; 

        public MainActivityFragmentAdapter(Android.Support.V4.App.FragmentManager fm, Android.Support.V4.App.Fragment[] fragments)
                : base(fm)
        {
            _fragments = fragments;
        }

        public override int Count => _fragments.Length;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return _fragments[position];
        }
    }
}
