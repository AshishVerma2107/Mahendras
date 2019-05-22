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
using Newtonsoft.Json;

namespace ImageSlider.MyTest
{
    class SummaryAdapter : RecyclerView.Adapter
    {
        
        Activity activity;
        int lastPosition = -1;
        List<string> items;
        List<List<questionmodel>> Alllist;
        List<questionmodel> questionlist;
        View itemView;
        public event EventHandler<int> ItemClick;
        RecyclerView mRecyclerView;
        List<int> startingquestionposition;
        string portal_or_local = "";
        List<TestSummaryDataModel> listTestSummaryModal;
        TextView txtrank;
        public SummaryAdapter(Activity activity,List<List<questionmodel>> Alllist,List<string> items,List<int> startingquestionposition, RecyclerView mRecyclerView,string portal_or_local)
        {
            this.activity = activity;
            this.Alllist = Alllist;
            this.items = items;
            this.mRecyclerView = mRecyclerView;
            this.startingquestionposition = startingquestionposition;
            this.portal_or_local = portal_or_local;
        }

        public SummaryAdapter(Activity activity, string testsummarylist, RecyclerView mRecyclerView, string portal_or_local, TextView txtrank)
        {
            this.activity = activity;
            this.portal_or_local = portal_or_local;
            listTestSummaryModal = JsonConvert.DeserializeObject<List<TestSummaryDataModel>>(testsummarylist);
            this.txtrank = txtrank;
        }
        public override int ItemCount
        {
            get
            {
                if (portal_or_local.Equals("portal"))
                {
                    return listTestSummaryModal.Count;
                }
                else
                {
                    return items.Count;
                }

            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            int answeredquestion = 0;
            int unsweredquestion = 0;
            int markforreview = 0;
            int unseenquestion = 0;
            string title="";
            if (portal_or_local.Equals("portal"))
            {
                txtrank.Text = "Current Rank : "+listTestSummaryModal[position].Rank+"";
                title = listTestSummaryModal[position].Title;
                try
                {
                    answeredquestion = listTestSummaryModal[position].Answered;
                }
                catch (Exception)
                {
                    answeredquestion = 0;
                }
                try
                {
                    unsweredquestion = listTestSummaryModal[position].NotAnswered;
                }
                catch (Exception)
                {
                    unsweredquestion = 0;
                }
                try
                {
                    unseenquestion= listTestSummaryModal[position].NotVisited;
                }
                catch (Exception)
                {
                    unseenquestion = 0;
                }

            }
            else
            {

                title = items[position];
                int startinpoint = startingquestionposition[position];
                int endpoint;
                if (position < items.Count - 1)
                {
                    endpoint = startingquestionposition[position + 1];
                }
                else
                {
                    endpoint = Alllist.Count;
                }
                for (int i = startinpoint; i < endpoint; i++)
                {
                    List<questionmodel> question = Alllist[i];

                    for (int y = 0; y < question.Count; y++)
                    {
                        if (question[y].Datatype == 1)
                        {

                            if (question[y].selectedoption == 0 && question[y].colorcode == Resource.Drawable.whitecircle1)
                            {
                                unseenquestion = unseenquestion + 1;

                            }
                            else if (question[y].colorcode == Resource.Drawable.redcircle)
                            {
                                unsweredquestion = unsweredquestion + 1;
                            }
                            else if (question[y].selectedoption == 0 && question[y].markforreview == 1)
                            {
                                markforreview = markforreview + 1;
                            }
                            else if (question[y].selectedoption > 0)
                            {
                                answeredquestion = answeredquestion + 1;
                                if (question[y].markforreview == 1)
                                {
                                    markforreview = markforreview + 1;
                                }
                            }

                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }

                }
            }
            SummaryViewHolder photoViewHolder = holder as SummaryViewHolder;
            photoViewHolder.title.Text = title;
            photoViewHolder.answerd.Text = answeredquestion + "";
            photoViewHolder.unanswerd.Text = unsweredquestion + "";
            photoViewHolder.reviewd.Text = markforreview + "";
            photoViewHolder.notvisited.Text = unseenquestion + "";
            Animation animation = AnimationUtils.LoadAnimation(activity, (position > lastPosition) ? Resource.Animation.slide_up : Resource.Animation.slide_up);
            photoViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.TestSummary_row, parent, false);
            SummaryViewHolder vh = new SummaryViewHolder(itemView, OnClick, mRecyclerView);
            return vh;
        }
        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }
    }
    public class SummaryViewHolder : RecyclerView.ViewHolder
    {
        
        public TextView answerd { get; set; }
        public TextView unanswerd { get; set; }
        public TextView notvisited { get; set; }
        public TextView reviewd { get; set; }
        public TextView title { get; set; }

        public SummaryViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            //Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);
            answerd = itemview.FindViewById<TextView>(Resource.Id.answeredcounter);
            unanswerd = itemview.FindViewById<TextView>(Resource.Id.unansweredcounter);
            notvisited = itemview.FindViewById<TextView>(Resource.Id.notcounter);
            reviewd = itemview.FindViewById<TextView>(Resource.Id.reviewcounter);
            title = itemview.FindViewById<TextView>(Resource.Id.subjecttitle);

            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}