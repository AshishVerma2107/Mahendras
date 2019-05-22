using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace ImageSlider
{
    class DashboardAdapter : RecyclerView.Adapter
    {
        String[] name = { };
        int[] colorcode = { };
        int[] imagename = { };
        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        RecyclerView mRecyclerView;
        public DashboardAdapter(String[] name,int [] colorcode,int [] imagename,Activity ac,RecyclerView mRecyclerView)
        {
            this.name = name;
            this.ac = ac;
            this.colorcode = colorcode;
            this.imagename = imagename;
            this.mRecyclerView = mRecyclerView;
        }
        public override int ItemCount
        {
            get { return name.Length; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder photoViewHolder = holder as PhotoViewHolder;
            photoViewHolder.Caption.Text = name[position];
            //int color = Resource.Color.abc_background_cache_hint_selector_material_dark;
            var color = new Color(ContextCompat.GetColor(ac, colorcode[position]));
            photoViewHolder.cardview.SetBackgroundColor(color);
            photoViewHolder.Image.SetBackgroundResource(imagename[position]);
            Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up : Resource.Animation.slide_up);
            photoViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.dashboard_row, parent, false);
            PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick,mRecyclerView);
            return vh;
        }
        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }
    }
    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public TextView Caption { get; set; }
        public LinearLayout cardview { get; set; }

    public PhotoViewHolder(View itemview, Action<int> listener,RecyclerView mRecyclerView) : base(itemview)
        {
            //Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemview.FindViewById<TextView>(Resource.Id.imagename);
            cardview = itemview.FindViewById<LinearLayout>(Resource.Id.cardviewfordashboard);
            Image = itemview.FindViewById<ImageView>(Resource.Id.dashobardimage);
           
            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}