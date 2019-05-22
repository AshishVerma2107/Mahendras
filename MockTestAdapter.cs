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

namespace ImageSlider
{
    class MockTestAdapter : RecyclerView.Adapter
    {
        readonly String[] name = { };
        readonly int[] colorcode = { };
        readonly int[] imagename = { };
        int lastPosition = -1;
        public event EventHandler<int> ItemClick;

        readonly Activity ac;
        View itemView;
        List<MockTestBean> list = new List<MockTestBean>();
        readonly RecyclerView mRecyclerView;
        public MockTestAdapter(List<MockTestBean> list, Activity ac,RecyclerView mRecyclerView)
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
            MockTestViewHolder mocktestViewHolder = holder as MockTestViewHolder;

            Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up1 : Resource.Animation.slide_down1);
            mocktestViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.mock_test_row, parent, false);
            MockTestViewHolder vh = new MockTestViewHolder(itemView, OnClick,mRecyclerView);
            return vh;
        }
        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }
    }
    public class MockTestViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public TextView Caption { get; set; }
        public LinearLayout Cardview { get; set; }

        public MockTestViewHolder(View itemview, Action<int> listener,RecyclerView mRecyclerView) : base(itemview)
        {
            //Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);

            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}