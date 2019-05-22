using System;
using System.Collections.Generic;

using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using ImageSlider.Model;


namespace ImageSlider.Adapter
{
    class ExamAlertAdapter : RecyclerView.Adapter
    {

        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        RecyclerView mRecyclerView;
        List<ExamAlert_Model> examalert_list;


        public ExamAlertAdapter(Activity ac, List<ExamAlert_Model> examalert_list, RecyclerView mRecyclerView)
        {
            this.ac = ac;
            this.mRecyclerView = mRecyclerView;
            this.examalert_list = examalert_list;

        }

        public override int ItemCount
        {
            get { return examalert_list.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ExamAlertViewHolder photoViewHolder1 = holder as ExamAlertViewHolder;
            photoViewHolder1.Caption.Text = examalert_list[position].content_title;
            Animation animation1 = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up1 : Resource.Animation.slide_down1);
            photoViewHolder1.ItemView.StartAnimation(animation1);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ExamAlert_Adapter_Layout, parent, false);
            ExamAlertViewHolder vh = new ExamAlertViewHolder(itemView, OnClick, mRecyclerView);
            return vh;
        }

        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }
    }

    public class ExamAlertViewHolder : RecyclerView.ViewHolder
    {

        public TextView Caption { get; set; }

        public ExamAlertViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            Caption = itemview.FindViewById<TextView>(Resource.Id.E_Title);
            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}



    