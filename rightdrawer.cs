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
using Newtonsoft.Json;

namespace ImageSlider.MyTest
{
    [Activity(Label = "" ,Theme = "@style/Theme.AppCompat.Translucent")]
    public class rightdrawer : Activity,View.IOnClickListener
    {
        LinearLayout ivclose;
        int selectedspinnerPosition;
        AnswerPlatee answerplate;
        List<AnswerPlatee> answePlateList = new List<AnswerPlatee>();
        Android.Support.V7.Widget.RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        int position;
        List<string> myitems = new List<string>();
        List<string> items;
        List<int> startingquestionposition;
        List<List<questionmodel>> AlQuestionList;
        int drawerselelectedspinner = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.leftdrawer);
            // var myquestionlist = Intent.GetStringExtra("QuestionList");
            var myquestionlist = DosTestFragment.myquestionlist;
            position = Intent.GetIntExtra("position", 0);
            drawerselelectedspinner = Intent.GetIntExtra("drawerselelectedspinner",0);
            startingquestionposition = JsonConvert.DeserializeObject<List<int>>(Intent.GetStringExtra("startingquestionposition"));
            items = JsonConvert.DeserializeObject<List<string>>(Intent.GetStringExtra("items"));
            myitems.Add("All");
            for (int i = 0; i < items.Count; i++)
            {
                myitems.Add(items[i]);
            }



            AlQuestionList = JsonConvert.DeserializeObject<List<List<questionmodel>>>(myquestionlist);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.mytesttestSpinner);
           
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, myitems);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;


            if (TestInstruction.testInfoList[0].Duration > 0)
            {
                spinner.Enabled = false;
               
            }
            else
            {
                spinner.Enabled = true;
               
            }

            spinner.SetSelection(drawerselelectedspinner);
            spinner.ItemSelected += Spinner_ItemSelected;
            ivclose = FindViewById<LinearLayout>(Resource.Id.closeleftdrawer);
            ivclose.SetOnClickListener(this);


            mRecycleView = FindViewById<RecyclerView>(Resource.Id.leftdrawerlist);
            mLayoutManager = new GridLayoutManager(this, 6);
            mRecycleView.SetLayoutManager(mLayoutManager);
          
            RightdrawerAdapter mAdapter = new RightdrawerAdapter(this,AlQuestionList,myitems,startingquestionposition, mRecycleView,0);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
            // Create your application here
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string  selectedspinner = spinner.GetItemAtPosition(e.Position) + "";
            selectedspinnerPosition = e.Position;
            RightdrawerAdapter mAdapter = new RightdrawerAdapter(this, AlQuestionList, myitems, startingquestionposition, mRecycleView, selectedspinnerPosition);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
           
        }

        private void MAdapter_ItemClick(object sender, int e)
        {
            
            Intent i = new Intent();
            i.PutExtra("position", e);
            i.PutExtra("selectedspinnerPosition", selectedspinnerPosition);
            SetResult(Result.Ok, i);
            Finish();
            OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_right);
        }

        public void OnClick(View v)
        {
            Intent i = new Intent();
            i.PutExtra("position", 4004);
            i.PutExtra("selectedspinnerPosition", selectedspinnerPosition);
            SetResult(Result.Ok, i);
            Finish();
            OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_right);
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                return false;
            }
            return false;
        }
    }
}