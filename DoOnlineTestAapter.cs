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
using ImageSlider.Model;

namespace ImageSlider.MyTest
{
    class DoOnlineTestAapter : RecyclerView.Adapter
    {
        String[] name = { };
        int[] colorcode = { };
        int[] imagename = { };
        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        List<AllTestModelData> alltestlist;


        RecyclerView mRecyclerView;
        
        public DoOnlineTestAapter(Activity ac, RecyclerView mRecyclerView,List<AllTestModelData> alltestlist)
        {
            this.alltestlist = alltestlist;
            this.ac = ac;
            this.mRecyclerView = mRecyclerView;
            
        }
        public override int ItemCount
        {
            get { return alltestlist.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            DoOnlineTestViewHolder onlinetestViewHolder = holder as DoOnlineTestViewHolder;
            AllTestModelData objmodel = alltestlist[position];

            onlinetestViewHolder.title.Text = objmodel.Title;
            onlinetestViewHolder.duration.Text = objmodel.Duration + " Mins";
            onlinetestViewHolder.date.Text = "Date "+objmodel.TestDate.Split("T")[0];
            onlinetestViewHolder.starttext.Text = objmodel.Text;
            onlinetestViewHolder.starttext.SetBackgroundResource(objmodel.background);
            Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up1 : Resource.Animation.slide_down1);
            onlinetestViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.DoOnlineTestFragmentRow, parent, false);
            DoOnlineTestViewHolder vh = new DoOnlineTestViewHolder(itemView, OnClick, mRecyclerView);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
    public class DoOnlineTestViewHolder : RecyclerView.ViewHolder
    {
        
        public TextView title { get; set; }
        public TextView duration { get; set; }
        public TextView time  { get; set; }
        public TextView date { get; set; }
        public TextView starttext { get; set; }
        

        public DoOnlineTestViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            //Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);
            title = itemview.FindViewById<TextView>(Resource.Id.onlinetesttitle);
            duration = itemview.FindViewById<TextView>(Resource.Id.duration);
            time = itemview.FindViewById<TextView>(Resource.Id.testtime);
            date = itemview.FindViewById<TextView>(Resource.Id.testdate);
            starttext = itemview.FindViewById<TextView>(Resource.Id.starttest);
            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}