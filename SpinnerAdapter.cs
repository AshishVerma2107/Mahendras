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

namespace ImageSlider.MyTest
{
    class SpinnerAdapter : RecyclerView.Adapter
    {
        Activity ac;
        List<string> itemlist;
        public event EventHandler<int> Item_click;
        int lastPosition = -1;
        View itemView;
        RecyclerView mRecyclerView;
        public SpinnerAdapter(Activity ac,List<string> itemlist,RecyclerView mRecyclerView)
        {
            this.ac = ac;
            this.itemlist = itemlist;
            this.mRecyclerView = mRecyclerView;
        }

        public override int ItemCount
        {
            get { return itemlist.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SpinnerViewHolder photoViewHolder = holder as SpinnerViewHolder;
            photoViewHolder.txtspinnertext.Text = itemlist[position];
            //int color = Resource.Color.abc_background_cache_hint_selector_material_dark;
         
            Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up : Resource.Animation.slide_up);
            photoViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.spinner_row, parent, false);
            SpinnerViewHolder vh = new SpinnerViewHolder(itemView, OnClick, mRecyclerView);
            return vh;

        }
        private void OnClick(int obj)
        {
            Item_click?.Invoke(this, obj);
        }
    }
    public class SpinnerViewHolder : RecyclerView.ViewHolder
    {
        public TextView txtspinnertext { get; set; }

        public SpinnerViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            txtspinnertext = itemview.FindViewById<TextView>(Resource.Id.spinner_row_text);
            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }

    }
}