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


    class YouTubeAdapter : BaseAdapter
    {
        public event EventHandler<int> Itemclickedyoutube;
        private Context context;
        

        

        public YouTubeAdapter(Context context)
        {
            this.context = context;

        }
        public override int Count
        {
            get
            {
              //return  gridViewString5.Length;

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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view;
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if (convertView == null)
            {
                view = new View(context);


                view = inflater.Inflate(Resource.Layout.YouTubeAdapterLayout, null);
               

                ImageView imgview = view.FindViewById<ImageView>(Resource.Id.videoyoutube);

              //  TextView txtview = view.FindViewById<TextView>(Resource.Id.textyou);


               // txtview.Text = gridViewString5[position];

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



            if (Itemclickedyoutube != null)
            {
                Itemclickedyoutube(this, obj);
            }
        }
        int[] thumbIds = {
        Resource.Drawable.youtube,
      


    };
        string[] gridViewString5 =
        {
            "",
            


        };


    }
}