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
    class SolutionSliderAdapter : RecyclerView.Adapter
    {

        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        RecyclerView mRecyclerView;
        List<List<questionmodel>> Allquestion;
        View myview;
        public SolutionSliderAdapter(Activity ac,List<List<questionmodel>> Allquestion,  RecyclerView mRecyclerView)
        {

            this.ac = ac;
            this.Allquestion = Allquestion;
            this.mRecyclerView = mRecyclerView;
        }
        public override int ItemCount
        {
            get { return Allquestion.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            solutionViewHolder photoViewHolder = holder as solutionViewHolder;
            //int myposition = Convert.ToInt32(photoViewHolder.Caption.GetTag(position));


            List<questionmodel> questionlist = Allquestion[position];
            Console.WriteLine("deepanshu---------->" + position);
            for (int i = 0; i < questionlist.Count; i++)
            {
                if (questionlist[i].Datatype == 1)
                {
                    photoViewHolder.Caption.SetBackgroundResource(questionlist[i].rightorwrongColorCode);
                    photoViewHolder.Caption.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(ac, questionlist[i].rightorwrongTextColor)));
                    break;
                }
                else
                {
                    continue;
                }
            }
          
            
           // photoViewHolder.view.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(ac, questionlist[position].Stripecolor)));
            
            position = position + 1;
            photoViewHolder.Caption.Text = position + "";

            //int color = Resource.Color.abc_background_cache_hint_selector_material_dark;

            Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.scale500 : Resource.Animation.scale500);
            photoViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
           
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.solution_horizontal_row, parent, false);
            solutionViewHolder vh = new solutionViewHolder(itemView, OnClick, mRecyclerView);
            return vh;
        }
        public void OnClick(int obj,View v)
        {
           
            //if (answeplatelist[obj].Stripecolor == Resource.Color.colorPrimary1)
            //{

                
               

            //    v.Selected = false;
            //    AnswerPlatee ansplatee = answeplatelist[obj];
            //    ansplatee.Stripecolor = Resource.Color.lightgrey;
            //    answeplatelist[obj] = ansplatee;
            //    View view = v.FindViewById<View>(Resource.Id.stripe);
            //    view.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(ac, answeplatelist[obj].Stripecolor)));
            //}
            //else {
              
            //    v.Selected = true;
            //    AnswerPlatee ansplatee = answeplatelist[obj];
            //    ansplatee.Stripecolor = Resource.Color.colorPrimary1;
            //    answeplatelist[obj] = ansplatee;
            //    View view = v.FindViewById<View>(Resource.Id.stripe);
            //    view.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(ac, answeplatelist[obj].Stripecolor)));
            //}
            ItemClick?.Invoke(this, obj);
        }

       
    }
    public class solutionViewHolder : RecyclerView.ViewHolder
    {

        public TextView Caption { get; set; }
        public View view;

        public solutionViewHolder(View itemview, Action<int,View> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            Caption = itemview.FindViewById<TextView>(Resource.Id.answerplatecircle);
            view = itemview.FindViewById<View>(Resource.Id.stripe);
            Caption.SetTag(mRecyclerView.GetChildAdapterPosition(itemview), mRecyclerView.GetChildAdapterPosition(itemview));
            AnswerPlatee ans=null;
            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender),itemview);
        }
    }
}