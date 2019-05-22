using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace ImageSlider.MyTest
{
    class RightdrawerAdapter : RecyclerView.Adapter
    {
       
        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        RecyclerView mRecyclerView;
        List<List<questionmodel>> answerplatee;
        List<int> startingquestionposition;
        List<string> myitems;
        int myposition;
        int count;
        int startposition;
        public RightdrawerAdapter(Activity ac,List<List<questionmodel>> answerplatee,List<string> myitems,List<int> startingquestionposition, RecyclerView mRecyclerView,int myposition)
        {
           
            this.ac = ac;
            this.answerplatee = answerplatee;
            this.mRecyclerView = mRecyclerView;
            this.startingquestionposition = startingquestionposition;
            this.myitems = myitems;
            this.myposition = myposition;
            if (myposition == 0)
            {
                count = answerplatee.Count;
                startposition = 0;
            }
            else
            {
                 startposition = startingquestionposition[myposition - 1];
                int endposition;
                if (myposition - 1 < myitems.Count - 2)
                {
                     endposition = startingquestionposition[myposition];
                    count = endposition -startposition;
                }
                else
                {
                     endposition = answerplatee.Count;
                    count = endposition - startposition;
                }
            }
        }
        public override int ItemCount
        {

            get { return count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RightDRawerViewHolder photoViewHolder = holder as RightDRawerViewHolder;
            //int myposition = Convert.ToInt32(photoViewHolder.Caption.GetTag(position));


               position = position + startposition;
                List<questionmodel> question = answerplatee[position];
                for (int i = 0; i < question.Count; i++)
                {
                    questionmodel objmodal = question[i];
                    if (objmodal.Datatype == 1)
                    {
                        photoViewHolder.Caption.SetBackgroundResource(objmodal.colorcode);
                        photoViewHolder.Caption.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(ac, objmodal.textcolor)));
                    }
                }


                position = position + 1;
                photoViewHolder.Caption.Text = position + "";
         
           
            //int color = Resource.Color.abc_background_cache_hint_selector_material_dark;
           
            //Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.scale500 : Resource.Animation.scale500);
            //photoViewHolder.ItemView.StartAnimation(animation);
            //lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.leftdrawerlist_row, parent, false);
            RightDRawerViewHolder vh = new RightDRawerViewHolder(itemView, OnClick, mRecyclerView,startposition);
            return vh;
        }
        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }
    }
    public class RightDRawerViewHolder : RecyclerView.ViewHolder
    {
      
        public TextView Caption { get; set; }
      

        public RightDRawerViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView,int startposition) : base(itemview)
        {
            Caption = itemview.FindViewById<TextView>(Resource.Id.answerplatecircle);

           
            itemview.Click += (sender, e) => listener(startposition + mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}