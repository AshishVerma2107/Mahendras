using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ImageSlider.Adapter
{
    class GridViewAdapter1 : BaseAdapter
    {

        Context context;
        public GridViewAdapter1(Context c)
        {
            context = c;
        }
        public override int Count
        {
            get
            {
                return thumbIds.Length;
            }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
        public override long GetItemId(int position)
        {
            return 0;
        }
        // create a new ImageView for each item referenced by the Adapter   
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView imageView;
            if (convertView == null)
            { // if it's not recycled, initialize some attributes   
                imageView = new ImageView(context);
                imageView.LayoutParameters = new GridView.LayoutParams(200, 250);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.SetPadding(10, 5, 5, 5);
            }
            else
            {
                imageView = (ImageView)convertView;
            }
            imageView.SetImageResource(thumbIds[position]);
            return imageView;
        }
        // references to our images   
        int[] thumbIds = {
        
        Resource.Drawable.images11,
        Resource.Drawable.images15,
        Resource.Drawable.image4,
        Resource.Drawable.images10,

    };
    }
}