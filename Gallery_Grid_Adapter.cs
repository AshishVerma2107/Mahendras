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
    class Gallery_Grid_Adapter : BaseAdapter<GalleryData>
    {
        List<GalleryData> gallerydata;


        private Context context;
       

        public Gallery_Grid_Adapter(Context context, List<GalleryData> Gallery)
        {
            this.context = context;
            this.gallerydata = Gallery;

        }

        public override GalleryData this[int position]
        {
            get
            {
                return gallerydata[position];
            }
        }
        public override int Count
        {
            get
            {
                return gallerydata.Count;
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
            ViewHolderForGallery holder;

            var local = new LocalOnClickListener();

            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if (view == null)
            {
                holder = new ViewHolderForGallery();

                view = inflater.Inflate(Resource.Layout.Gallery_GridView_Adapter, null);


                holder.gallerytext = (TextView)view.FindViewById(Resource.Id.textViewgallery);

                holder.GalleryImage = view.FindViewById<ImageView>(Resource.Id.imageViewgallery);



                holder.gallerytext.Text = gallerydata[position].gallery_name;

                var imageBitmap = GetImageBitmapFromUrl(gallerydata[position].gallery_url);
                holder.GalleryImage.SetImageBitmap(imageBitmap);

                // Android.Net.Uri url = Android.Net.Uri.Parse(gallerydata[position].gallery_url);
                // holder.GalleryImage.SetImageURI(url);

                view.Tag = holder;


                // imgview.SetImageResource(gridViewImage[position]);
                //  imgview.SetImageResource(thumbIds[position]);
                // imgview.SetImageResource(Resource.Drawable.aboutexam);

                //string glr = gallerydata[position].gallery_url;

                //int igallery = Convert.ToInt32(glr);

                //imgview.SetImageResource(igallery);


                //BitmapFactory.Options bmOptions = new BitmapFactory.Options();
                //bmOptions.InJustDecodeBounds = false;
                //bmOptions.InSampleSize = 10;
                //bmOptions.InPurgeable = true;
                //Bitmap bitmap = BitmapFactory.DecodeFile(gallerydata[position].gallery_url, bmOptions);
                //imgview.SetImageBitmap(bitmap);


            }
            else
            {
                holder = (ViewHolderForGallery)view.Tag;
            }
            return view;
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

    public class ViewHolderForGallery : Java.Lang.Object
    {
        internal TextView gallerytext;

        public ImageView GalleryImage { get; set; }
        public TextView Name { get; set; }
        public TextView Department { get; set; }
    }
}