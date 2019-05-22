using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace ImageSlider.MyClassSchedule
{
    class MyClassScheduleAdapter : RecyclerView.Adapter
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
        public MyClassScheduleAdapter(List<MyResultandSelectionBean> list, Activity ac, RecyclerView mRecyclerView)
        {
            this.list = list;
            this.ac = ac;
            this.mRecyclerView = mRecyclerView;
        }
        public override int ItemCount
        {
            get { return 20; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyClassScheduleViewHolder onlinetestViewHolder = holder as MyClassScheduleViewHolder;

            Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up1 : Resource.Animation.slide_down1);
            onlinetestViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ClassSchedule_row, parent, false);
            MyClassScheduleViewHolder vh = new MyClassScheduleViewHolder(itemView, OnClick, mRecyclerView);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
    public class MyClassScheduleViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public TextView Caption { get; set; }
        public LinearLayout cardview { get; set; }

        public MyClassScheduleViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            //Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);

            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}