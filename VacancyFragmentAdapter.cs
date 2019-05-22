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
using ImageSlider.Model;

namespace ImageSlider.Adapter
{
    class VacancyFragmentAdapter : RecyclerView.Adapter
    {

        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        RecyclerView mRecyclerView;
        List<Vacancy_Model> weekly_list;
        public VacancyFragmentAdapter(Activity ac, List<Vacancy_Model> weekly_list, RecyclerView mRecyclerView)
        {
            this.ac = ac;
            this.mRecyclerView = mRecyclerView;
            this.weekly_list = weekly_list;
        }
        public override int ItemCount
        {
            get { return weekly_list.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            VacancyViewHolder photoViewHolder = holder as VacancyViewHolder;
            photoViewHolder.Caption.Text = weekly_list[position].content_title;
            Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up1 : Resource.Animation.slide_down1);
            photoViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Vacancy_Fragment_Row, parent, false);
            VacancyViewHolder vh = new VacancyViewHolder(itemView, OnClick, mRecyclerView);
            return vh;
        }
        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }
    }
    public class VacancyViewHolder : RecyclerView.ViewHolder
    {

        public TextView Caption { get; set; }

        public VacancyViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            Caption = itemview.FindViewById<TextView>(Resource.Id.vacancytitle);
            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}