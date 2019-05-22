using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Frag = Android.Support.V4.App;
using Android.Views;
using Android.Widget;

using Android.Support.V4.App;
using ImageSlider.Fragments;

namespace ImageSlider.Adapter
{
    public class FragStateSupport : FragmentStatePagerAdapter
    {
        private int _count;
        private List<int> items;

        public FragStateSupport(Frag.FragmentManager fm, List<int> data) : base(fm)
        {
            this.items = data;
            _count = data.Count;

        }

        public override int Count
        {
            get { return _count; }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {

            var _itemPosition = items[position];
            TestFragment f = new TestFragment();
            f.setImageList(_itemPosition,position);
            return f;
        }


    }
}