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
    class OnlineTestAapter : RecyclerView.Adapter
    {
        String[] name = { };
        int[] colorcode = { };
        int[] imagename = { };
        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        List<MyResultandSelectionBean> list = new List<MyResultandSelectionBean>();
        RecyclerView mRecyclerView;
        public OnlineTestAapter(List<MyResultandSelectionBean> list, Activity ac,RecyclerView mRecyclerView)
        {
            this.list = list;
            this.ac = ac;
            this.mRecyclerView = mRecyclerView;
        }
        public override int ItemCount
        {
            get { return list.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OnlineTestViewHolder onlinetestViewHolder = holder as OnlineTestViewHolder;
          
            Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up1 : Resource.Animation.slide_down1);
            onlinetestViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.onlinetest_row, parent, false);
            OnlineTestViewHolder vh = new OnlineTestViewHolder(itemView, OnClick,mRecyclerView);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
    public class OnlineTestViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public TextView Caption { get; set; }
        public LinearLayout cardview { get; set; }

        public OnlineTestViewHolder(View itemview, Action<int> listener,RecyclerView mRecyclerView) : base(itemview)
        {
            //Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);
           
            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}