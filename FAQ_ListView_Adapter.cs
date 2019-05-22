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
    class FAQ_ListView_Adapter : BaseAdapter<FAQ_Data>
    {
        List<FAQ_Data>  faq_data;


        private Context context;


        public FAQ_ListView_Adapter(Context context, List<FAQ_Data> FAQ_List)
        {
            this.context = context;
            this.faq_data = FAQ_List;

        }

        public override FAQ_Data this[int position]
        {
            get
            {
                return faq_data[position];
            }
        }
        public override int Count
        {
            get
            {
                return faq_data.Count;
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
            ViewHolderForFAQ holder;

            var local = new LocalOnClickListener();

            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if (view == null)
            {
                holder = new ViewHolderForFAQ();

                view = inflater.Inflate(Resource.Layout.FAQ_ListView_Adapter_Layout, null);


                holder.faq_title = (TextView)view.FindViewById(Resource.Id.textView_faq_title);

                //holder.noticeimage = view.FindViewById<ImageView>(Resource.Id.imageView_notice_image);

                holder.faq_text = view.FindViewById<TextView>(Resource.Id.textView_faq_text);

                holder.faq_title.Text = faq_data[position].content_title;
               // holder.faq_text.Text = faq_data[position].content_date;

                



                view.Tag = holder;



                

            }
            else
            {
                holder = (ViewHolderForFAQ)view.Tag;
            }
            return view;
        }


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

    public class ViewHolderForFAQ : Java.Lang.Object
    {
        internal TextView faq_title;

        public ImageView noticeimage { get; set; }
        public TextView faq_text { get; set; }
       
    }
}