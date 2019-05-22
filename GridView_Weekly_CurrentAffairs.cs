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
    class GridView_Weekly_CurrentAffairs : BaseAdapter
    {

        public event EventHandler<int> Itemclicked;
        private Context context;




        public GridView_Weekly_CurrentAffairs(Context context)
        {
            this.context = context;


        }
        public override int Count
        {
            get
            {
                return gridViewString2.Length;
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


                view = inflater.Inflate(Resource.Layout.Weekly_GridView_AdapterLayout, null);



                TextView txtview = view.FindViewById<TextView>(Resource.Id.textViewweekly);

                ImageView imgview = view.FindViewById<ImageView>(Resource.Id.weeklyimage);

                

                txtview.Text = gridViewString2[position];

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



            if (Itemclicked != null)
            {
                Itemclicked(this, obj);
            }
        }



        int[] thumbIds = {
        Resource.Drawable.weekly,
        Resource.Drawable.S_Mater,
        Resource.Drawable.Video_Lecture,
       




    };
        string[] gridViewString2 =
        {
            "Weekly C.Affairs",
            "Vocabulary",
            "SpotLight",
           

        };

        // private object SupportFragmentManager;

        private void LoadFragmentfornotice(object sender, int e)
        {
            int photoNum = e + 1;
            Toast.MakeText(context, "This is photo number " + photoNum, ToastLength.Short).Show();
        }



    }
}