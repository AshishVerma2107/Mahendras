using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ImageSlider.Model;

namespace ImageSlider.Adapter
{
    class StudyMaterial_ListView_Adapter : BaseAdapter<StudyMaterialData>
    {

        List<StudyMaterialData> study_data;


        private Context context;

        public event EventHandler<int> Itemclick;

        public StudyMaterial_ListView_Adapter(Context context, List<StudyMaterialData> StudyMaterial_List)
        {
            this.context = context;
            this.study_data = StudyMaterial_List;

        }

        public override StudyMaterialData this[int position]
        {
            get
            {
                return study_data[position];
            }
        }
        public override int Count
        {
            get
            {
                return study_data.Count;
            }
        }

        public Action<object, int> ItemClick { get; internal set; }

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

            var local = new LocalOnClickListener();

            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if (view == null)
            {
                holder = new ViewHolder();

                view = inflater.Inflate(Resource.Layout.StudyMaterial_ListView_Adapter_Layout, null);


                holder.txtview = (TextView)view.FindViewById(Resource.Id.text_SM);

                TextView txtviewurl = (TextView)view.FindViewById(Resource.Id.text_SM_url);

                ImageView imgview = view.FindViewById<ImageView>(Resource.Id.image_SM);

                ImageView imgviewTitle = view.FindViewById<ImageView>(Resource.Id.image_SMTitle);


               



                holder.txtview.Text = study_data[position].content_id;

                txtviewurl.Text = study_data[position].click_url;

                // imgview.SetImageResource(gridViewImage[position]);
                //  imgview.SetImageResource(thumbIds[position]);
                imgview.SetImageResource(Resource.Drawable.aboutexam);

                var imageBitmap = GetImageBitmapFromUrl(study_data[position].content_title);
                imgviewTitle.SetImageBitmap(imageBitmap);


                view.Click += (sender, e) => OnClick(position);

                view.Tag = holder;


                
            }
            else
            {
                holder = (ViewHolder)view.Tag;
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
        Resource.Drawable.aboutexam,



    };

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

    }
    public class ViewHolderForStudyMaterial : Java.Lang.Object
    {
        internal TextView txtview;

        internal TextView txtviewurl;
        // internal ImageView imgviewTitle;

        public ImageView imgviewTitle { get; set; }
        public TextView Study_URL { get; set; }
        // public TextView Department { get; set; }
    }
}