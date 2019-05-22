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
    class GridViewSlideAdapter : BaseAdapter
    {
        public event EventHandler<int> Itemclick;
        private Context context;
        // private string[] gridViewString;
        // private int[] gridViewImage;

        // public GridViewAboutExam(Context context, string[] gridViewstr, int[] gridViewImage)

        public GridViewSlideAdapter(Context context)
        {
            this.context = context;

        }
        public override int Count
        {
            get
            {
                return gridViewString1.Length;
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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view;
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if (convertView == null)
            {
                view = new View(context);
               

                view = inflater.Inflate(Resource.Layout.SlideLayout_RefactorCircleView, null);
                TextView txtview = view.FindViewById<TextView>(Resource.Id.slidetextViewGrid);

                ImageView imgview = view.FindViewById<ImageView>(Resource.Id.slidecircleimage);



                txtview.Text = gridViewString1[position];
                // imgview.SetImageResource(gridViewImage[position]);
                imgview.SetImageResource(thumbIds[position]);

                view.Click += (sender, e) => OnClick(position);
            }
            else
            {
                view = (View)convertView;
            }
            return view;
        }

        private void OnClick(int obj)
        {


            if (Itemclick != null)
            {
                Itemclick(this, obj);
            }
        }

        int[] thumbIds = {
        Resource.Drawable.home_icon,
        Resource.Drawable.st_portal_21,
        Resource.Drawable.shop_21,
        Resource.Drawable.about_21,
       // Resource.Drawable.c1,
       // Resource.Drawable.ga1,
       // Resource.Drawable.ms1,
       // Resource.Drawable.sp1,
       // Resource.Drawable.a1,


    };
        string[] gridViewString1 =
        {
            "Home",
            "ST Portal",
            "My Shop", 
            "About Us",

          //  "Material",
          //  "MICA",
           // "Join Us",
           // "Contact",
           // "Gallery",
          //  "My Shop",
          //  "ST Portal",
          //  "About Us"

        };
        private object SupportFragmentManager;

        private void LoadFragment(object sender, int e)
        {
            int photoNum = e + 1;
            Toast.MakeText(context, "This is photo number " + photoNum, ToastLength.Short).Show();
        }

    }
}