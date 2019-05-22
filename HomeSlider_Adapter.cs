using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;

namespace ImageSlider.Adapter
{
    public class HomeSlider_Adapter : RecyclerView.Adapter
    {

        public event EventHandler<int> ItemClick;

        public PhotoAlbum mPhotoAlbum;

        public HomeSlider_Adapter(PhotoAlbum photoAlbum)
        {
            mPhotoAlbum = photoAlbum;
        }
        public override int ItemCount
        {
            get { return mPhotoAlbum.numPhoto; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder vh = holder as PhotoViewHolder;
            vh.Image.SetImageResource(mPhotoAlbum[position].mPhotoID);
            vh.Caption.Text = mPhotoAlbum[position].mCaption;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.HomeSlider_adapterRecyclerView, parent, false);
            PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }

    public class Photo
    {
        public int mPhotoID { get; set; }
        public string mCaption { get; set; }
    }
    public class PhotoAlbum
    {
        static Photo[] listPhoto =
        {
            new Photo() {mPhotoID = Resource.Drawable.home_icon, mCaption = "Home"},
            new Photo() {mPhotoID = Resource.Drawable.st_portal_21, mCaption = "ST Portal 2"},
            new Photo() {mPhotoID = Resource.Drawable.shop_21, mCaption = "My Shop"},
            new Photo() {mPhotoID = Resource.Drawable.about_21, mCaption = "About Us"},



        };
        private Photo[] photos;
      //  Random random;
        public PhotoAlbum()
        {
            this.photos = listPhoto;
          //  random = new Random();
        }
        public int numPhoto
        {
            get
            {
                return photos.Length;
            }
        }
        public Photo this[int i]
        {
            get { return photos[i]; }
        }
    }

    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public TextView Caption { get; set; }

        public PhotoViewHolder(View itemview, Action<int> listener) : base(itemview)
        {
            Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemview.FindViewById<TextView>(Resource.Id.textView);
            itemview.Click += (sender, e) => listener(base.Position);
        }
    }
}  
