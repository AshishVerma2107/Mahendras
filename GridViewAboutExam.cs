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
    class GridViewAboutExam : BaseAdapter
    {

        public event EventHandler<int> Itemclick;
        private Context context;

       
       

        public GridViewAboutExam(Context context)
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
                //view = inflater.Inflate(Resource.Layout.gridviewaboutexam_layout, null);
                //TextView txtview = view.FindViewById<TextView>(Resource.Id.textViewGrid);
                //ImageView imgview = view.FindViewById<ImageView>(Resource.Id.imageViewGrid);

                view = inflater.Inflate(Resource.Layout.aboutexam_refactorcircleview, null);
                TextView txtview = view.FindViewById<TextView>(Resource.Id.textViewGrid);

                ImageView imgview = view.FindViewById<ImageView>(Resource.Id.circleimage);



                txtview.Text = gridViewString1[position];
                // imgview.SetImageResource(gridViewImage[position]);
                imgview.SetImageResource(thumbIds[position]);
                
                //Action<int> listner = new Onc
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

             Resource.Drawable.v_c_21,
        Resource.Drawable.e_a_21,
        Resource.Drawable.admiss_21,
        Resource.Drawable.voc_21,
        Resource.Drawable.notice_21,
        Resource.Drawable.c_a_21,


        //Resource.Drawable.job_20,
        //Resource.Drawable.exam_20,
        //Resource.Drawable.admiss_20,
        //Resource.Drawable.vocabu_20,
        //Resource.Drawable.Video_cou_20,
        //Resource.Drawable.curr_20,


        //Resource.Drawable.F_Ab_Exam,
        //Resource.Drawable.S_Mater,
        //Resource.Drawable.F_ST,
        //Resource.Drawable.F_Vancy,
        //Resource.Drawable.F_Branch,
        //Resource.Drawable.F_Videos,

    };
        string[] gridViewString1 =
        {
            "Free Videos",
            "Exam Alert",
             "Free PDF",
           // "Admission",
            "Vocabulary",
            "Notice Board",
            "Current Affairs"


            //"About Exam",
            //"Material",
            //"Speed Test",
            //"Job Alert",
            //"Branches",
            //"Video",
        };
        private object SupportFragmentManager;

        private void LoadFragment(object sender, int e)
        {
            int photoNum = e + 1;
            Toast.MakeText(context, "This is photo number " + photoNum, ToastLength.Short).Show();
        }

        

    }
}