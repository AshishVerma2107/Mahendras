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
    class CurrentAffairAdapter : RecyclerView.Adapter
    {

        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        RecyclerView mRecyclerView;
        List<Current_Affairs_Model> current_list;


        public CurrentAffairAdapter(Activity ac, List<Current_Affairs_Model> current_list, RecyclerView mRecyclerView)
        {
            this.ac = ac;
            this.mRecyclerView = mRecyclerView;
            this.current_list = current_list;
        }

        public override int ItemCount
        {
            get { return current_list.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CurrentViewHolder photoViewHolder = holder as CurrentViewHolder;
            photoViewHolder.Caption.Text = current_list[position].content_title;
            Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up1 : Resource.Animation.slide_down1);
            photoViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CurrentAffair_AdapterLayout, parent, false);
            CurrentViewHolder vh = new CurrentViewHolder(itemView, OnClick, mRecyclerView);
            return vh;
        }
        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }
    }

    public class CurrentViewHolder : RecyclerView.ViewHolder
    {

        public TextView Caption { get; set; }

        public CurrentViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            Caption = itemview.FindViewById<TextView>(Resource.Id.c_Title);
            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}

