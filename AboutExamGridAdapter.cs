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
using ImageSlider.Model;

namespace ImageSlider.Adapter
{
   public  class AboutExamGridAdapter : BaseAdapter<AboutExamData>
    {
       List<AboutExamData> aboutexam;
        

        private Context context;
        // private string[] gridViewString;
        // private int[] gridViewImage;

        // public GridViewAboutExam(Context context, string[] gridViewstr, int[] gridViewImage)

        public AboutExamGridAdapter(Context context, List<AboutExamData> AboutExam)
        {
            this.context = context;
            this.aboutexam = AboutExam;

        }

        public override AboutExamData this[int position]
        {
            get
            {
                return aboutexam[position];
            }
        }
        public override int Count
        {
            get
            {
                return aboutexam.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            ViewHolder holder;

          //  var local = new LocalOnClickListener();

            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if (view == null)
            {
                holder = new ViewHolder();

                view = inflater.Inflate(Resource.Layout.gridviewaboutexam_layout, null);


                holder.txtview = (TextView)view.FindViewById(Resource.Id.textViewGrid);

                ImageView imgview = view.FindViewById<ImageView>(Resource.Id.imageViewGrid);



                holder.txtview.Text = aboutexam[position].name;
                // imgview.SetImageResource(gridViewImage[position]);
                //  imgview.SetImageResource(thumbIds[position]);
                imgview.SetImageResource(Resource.Drawable.aboutexam);
                view.Tag = holder;
            }
            else
            {
                holder = (ViewHolder)view.Tag;
            }
            return view;
        }
        int[] thumbIds = {
        Resource.Drawable.aboutexam,
       


    };
        
    }
}