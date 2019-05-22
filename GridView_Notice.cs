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
    class GridView_Notice : BaseAdapter
    {
       

        public event EventHandler<int> Itemclick;
        private Context context;




        public GridView_Notice(Context context)
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
                

                view = inflater.Inflate(Resource.Layout.Notice_GridView_Adapter_Layout, null);

                

                TextView txtview = view.FindViewById<TextView>(Resource.Id.textViewnotice);

                ImageView imgview = view.FindViewById<ImageView>(Resource.Id.noticeimage);



                txtview.Text = gridViewString1[position];
               
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


            Resource.Drawable.join_21,
             Resource.Drawable.faq_21,
              Resource.Drawable.faci_21,
               Resource.Drawable.vac_21,
                Resource.Drawable.career_21,
                 Resource.Drawable.telegram_21,

        //Resource.Drawable.A_NoticeBoard,
        //Resource.Drawable.B_Career,
        //Resource.Drawable.A_Telegram,
        //Resource.Drawable.A_Facil,
        //Resource.Drawable.A_Admission,
        // Resource.Drawable.A_FAQ,
        // Resource.Drawable.A_ClassRoom,
        // Resource.Drawable.currentaffair,
        // Resource.Drawable.examalert,




    };
        string[] gridViewString1 =
        {
            "Join Classroom",
            "FAQ",
            "Facilities",
            "Vacancy",
            "Career",
            "Join Telegram"


            //"Notice Board",
            //"Career",
            //"Join Telegram",
            //"Facilities",
            //"Admission",
            //"FAQ",
            //"Join ClassRoom",
            //"Current Affairs",
            //"Exam Alert"


        };

       // private object SupportFragmentManager;

        private void LoadFragmentfornotice(object sender, int e)
        {
            int photoNum = e + 1;
            Toast.MakeText(context, "This is photo number " + photoNum, ToastLength.Short).Show();
        }



    }
}