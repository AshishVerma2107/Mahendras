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
    class Videos_GridView_Adapter : BaseAdapter<VideoData>
    {

        List<VideoData> video_data;


        private Context context;


        public Videos_GridView_Adapter(Context context, List<VideoData> Videos_List)
        {
            this.context = context;
            this.video_data = Videos_List;

        }

        public override VideoData this[int position]
        {
            get
            {
                return video_data[position];
            }
        }
        public override int Count
        {
            get
            {
                return video_data.Count;
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
            ViewHolderForVideos holder;

            var local = new LocalOnClickListener();

            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if (view == null)
            {
                holder = new ViewHolderForVideos();

                view = inflater.Inflate(Resource.Layout.Videos_GridView_Adapter, null);


                holder.videostext = (TextView)view.FindViewById(Resource.Id.textView_videos);

                holder.videosimage = view.FindViewById<ImageView>(Resource.Id.imageView_videos);

                holder.videos_URL = view.FindViewById<TextView>(Resource.Id.textView_videos_url);

                holder.videostext.Text = video_data[position].title;
                holder.videos_URL.Text = video_data[position].videoURL;

                var imageBitmap = GetImageBitmapFromUrl(video_data[position].thumb);
                holder.videosimage.SetImageBitmap(imageBitmap);

              

                view.Tag = holder;



                //holder.videosimage.Click += delegate
                //{
                //    Intent taskIntentActivity5 = new Intent(context, typeof(YouTube_Activity));

                //    context.StartActivity(taskIntentActivity5);
                //};

            }
            else
            {
                holder = (ViewHolderForVideos)view.Tag;
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

    public class ViewHolderForVideos : Java.Lang.Object
    {
        internal TextView videostext;

        public ImageView videosimage { get; set; }
        public TextView videos_URL { get; set; }
       // public TextView Department { get; set; }
    }
}