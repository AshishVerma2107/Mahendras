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
    class GridView_VideoLecture : BaseAdapter
    {

        private Context context;


        public event EventHandler<int> Itemclickedguru;

        public GridView_VideoLecture(Context context)
        {
            this.context = context;

        }
        public override int Count
        {
            get
            {
                return gridViewString8.Length;
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


                view = inflater.Inflate(Resource.Layout.VideoLecture_Layout, null);
                TextView txtview = view.FindViewById<TextView>(Resource.Id.videotextViewGrid);

               ImageView imgview = view.FindViewById<ImageView>(Resource.Id.videocircleimage);



                txtview.Text = gridViewString8[position];
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



            if (Itemclickedguru != null)
            {
                Itemclickedguru(this, obj);
            }
        }

        int[] thumbIds = {
        Resource.Drawable.bank_30,
        Resource.Drawable.ssc_30,
        Resource.Drawable.railway_30,
        


    };
        string[] gridViewString8 =
        {
            "Bank",
            "SSC",
            "Railway",
           

        };
    }
}