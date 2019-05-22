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
using Android.Widget;
using ImageSlider.Model;

namespace ImageSlider.MyTest
{
    class TestInstructionAdapter : RecyclerView.Adapter
    {
        String[] name = { };
        int[] colorcode = { };
        int[] imagename = { };
        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        List<TestInfoListRecord> alltestinfolist;
        RecyclerView mRecyclerView;

        public TestInstructionAdapter(Activity ac, RecyclerView mRecyclerView, List<TestInfoListRecord> alltestinfolist)
        {
            this.alltestinfolist = alltestinfolist;
            this.ac = ac;
            this.mRecyclerView = mRecyclerView;

        }
        public override int ItemCount
        {
            get { return alltestinfolist.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TestInfoViewHolder onlinetestViewHolder = holder as TestInfoViewHolder;
            TestInfoListRecord objmodel = alltestinfolist[position];

            onlinetestViewHolder.title.Text = objmodel.SubjectTitle;
            onlinetestViewHolder.positive.Text = "+"+objmodel.CorrectMarks+"";
            onlinetestViewHolder.negative.Text = "-"+objmodel.NegativeMarks+"";
            onlinetestViewHolder.totalmarks.Text = "Total Marks " + objmodel.TotalMarks;
            onlinetestViewHolder.totalquestion.Text = "Total Question " + objmodel.TotalQuestion;
           
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sectionpattern_row, parent, false);
            TestInfoViewHolder vh = new TestInfoViewHolder(itemView, OnClick, mRecyclerView);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
    public class TestInfoViewHolder : RecyclerView.ViewHolder
    {

        public TextView title { get; set; }
        public TextView negative { get; set; }
        public TextView positive { get; set; }
        public TextView totalquestion { get; set; }
        public TextView totalmarks { get; set; }



        public TestInfoViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            //Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);
            title = itemview.FindViewById<TextView>(Resource.Id.sectiontitle);
            positive = itemview.FindViewById<TextView>(Resource.Id.postivemarks);
            negative = itemview.FindViewById<TextView>(Resource.Id.negativemarks);
            totalmarks = itemview.FindViewById<TextView>(Resource.Id.paperwisetotalmarks);
            totalquestion = itemview.FindViewById<TextView>(Resource.Id.paperwisetotalquestion);

            // itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}