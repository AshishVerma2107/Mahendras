using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Refit;
using ImageSlider.Connection; 
namespace ImageSlider.MyTest
{
    class CustomDialog : Dialog
    {
        Activity context;
        int answeredquestion = 0;
        int markforreview = 0;
        int unseenquestion = 0;
        int unanswered = 0;
        string path;
        string question;
        string passage;
        int testid;
        public static string allquestion;
        float negativemarks;
        List<List<questionmodel>> AllTestList = new List<List<questionmodel>>();
        bool submitoncancel;
        ISharedPreferencesEditor edit;
        string item;
        List<int> startingquestionposition;
        string langcode;
        string testtype;
        bool cancelshowornot;
        public CustomDialog(Activity activity) : base(activity)
        {
        }
            public CustomDialog(Activity activity, int answeredquestion, int markforreview, int unseenquestion, string question, string passage, string path, int testid, float negativematks,bool submitoncancel, ISharedPreferencesEditor edit,string item,List<int> startingquestionposition,string langcode,string testtype, bool cancelshowornot,int unanswered) : base(activity)
        {
            context = activity;
            this.answeredquestion = answeredquestion;
            this.markforreview = markforreview;
            this.unseenquestion = unseenquestion;
            this.unanswered = unanswered;
            this.question = question;
            this.passage = passage;
            this.path = path;
            this.testid = testid;
            this.negativemarks = negativematks;
            this.submitoncancel = submitoncancel;
            this.edit = edit;
            this.item = item;
            this.startingquestionposition = startingquestionposition;
            this.langcode = langcode;
            this.testtype = testtype;
            this.cancelshowornot = cancelshowornot;
            AllTestList = JsonConvert.DeserializeObject<List<List<questionmodel>>>(question);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.CustomDialog);
            this.Window.Attributes.WindowAnimations = Resource.Style.DialogAnimation;
            TextView cancel = (TextView)FindViewById(Resource.Id.button_cancel);
            TextView submit = (TextView)FindViewById(Resource.Id.submit_button);
            TextView txtanswer = (TextView)FindViewById(Resource.Id.answered);
            TextView txtmarkforreview = (TextView)FindViewById(Resource.Id.markforreview);
            TextView txtunseen = (TextView)FindViewById(Resource.Id.unseenquestion);
            TextView txtunanswered = (TextView)FindViewById(Resource.Id.unanswered);
            TextView txtTitle = (TextView)FindViewById(Resource.Id.dialogTitle);
            //======================if popup open fron timeup==========================//
            if (cancelshowornot)
            {
                cancel.Visibility = ViewStates.Visible;

            }
            else
            {
                cancel.Visibility = ViewStates.Invisible;
                txtTitle.Text = "Time Up";
            }
           
            //=========================================================================//
            txtanswer.Text = answeredquestion + "";
            txtmarkforreview.Text = markforreview + "";
            txtunseen.Text = unseenquestion + "";
            txtunanswered.Text = unanswered + "";

            submit.Click += (e, a) =>
            {
                Dismiss();
                List<UserResponse> myuserresponselist = new List<UserResponse>();
                for (int i = 0; i < AllTestList.Count(); i++)
                {
                    List<questionmodel> questionlist = AllTestList[i];
                    for (int y = 0; y < questionlist.Count; y++)
                    {
                        questionmodel objmodel = questionlist[y];

                        if (objmodel.Datatype == 1)
                        {
                            bool iscoorect = false;
                            bool ismarkforreview = false;
                            float marks = 0;
                            if (objmodel.colorcode != Resource.Drawable.whitecircle1)
                            {


                                if (objmodel.rightorwrongColorCode == Resource.Drawable.greenCircle)
                                {
                                    iscoorect = true;
                                    marks = objmodel.Correctmarks;
                                    if (objmodel.markforreview == 0)
                                    {
                                        ismarkforreview = false;
                                    }
                                    else
                                    {
                                        ismarkforreview = true;
                                    }
                                }
                                else if (objmodel.rightorwrongColorCode == Resource.Drawable.redcircle)
                                {
                                    iscoorect = false;
                                    marks = negativemarks*-1;
                                    if (objmodel.markforreview == 0)
                                    {
                                        ismarkforreview = false;
                                    }
                                    else
                                    {
                                        ismarkforreview = true;
                                    }
                                }

                                if (objmodel.colorcode == Resource.Drawable.redcircle)
                                {
                                    iscoorect = false;
                                    marks = 0;
                                    if (objmodel.markforreview == 0)
                                    {
                                        ismarkforreview = false;
                                    }
                                    else
                                    {
                                        ismarkforreview = true;
                                    }
                                }

                                String date = "01/05/2019";
                                DateTime oDate = Convert.ToDateTime(date);
                                UserResponse userresponse = new UserResponse
                                {
                                    ID = objmodel.Id,
                                    TestID = testid,
                                    QID = objmodel.Qid,
                                    UserID = MainActivity1.userid,
                                    TimeTaken = 5,
                                    IsCorrect = iscoorect,
                                    Marks = marks,
                                    MarkForReview = ismarkforreview,
                                    Response = objmodel.selectedoption+"",
                                    

                                };
                                myuserresponselist.Add(userresponse);
                            }
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                SubmitTestData sdata = new SubmitTestData
                {
                    TestID = testid,
                    userid = MainActivity1.userid,
                    deflanguage = langcode,
                    UserResponseData = myuserresponselist

                };
               
                CustomProgressDialog cp = new CustomProgressDialog(context);
              
                cp.Show();
                if (Utility.IsNetworkConnected(context))
                {
                    Callapi(sdata, cp);
                }
                else
                {
                    cp.Dismiss();
                    Toast.MakeText(context, "Check your internet connection", ToastLength.Short).Show();
                }
               


            };
            cancel.Click += (e, a) =>
            {
                Dismiss();
                if (submitoncancel)
                {
                    context.Finish();
                }
            };
        }

        async void Callapi(SubmitTestData sdata, CustomProgressDialog cp)
        {
            string test = JsonConvert.SerializeObject(sdata);
            Console.WriteLine("deepanshu-->"+test);
            try
            {
                var apiresponse = RestService.For<ApiConnectionForTestPackage>(Utility.stapibaseUrl);
                string response = await apiresponse.SubmitTestRecord(sdata);
                // edit.Clear();
                //edit.Apply();
              
               
              
                if (testtype.Equals("online"))
                {

                    FetchSumaryFromServer(testid,cp);
                    //var intent = new Intent(context, typeof(TestSummary));
                    //// intent.PutExtra("question", question);
                    //// intent.PutExtra("passage", passage);
                    //edit.PutString("path", path);
                    //edit.PutString("item", item);
                    //edit.PutString("startingquestionposition", JsonConvert.SerializeObject(startingquestionposition));

                    //edit.Apply();
                    //intent.PutExtra("path", path);
                    //intent.PutExtra("item", item);
                    //intent.PutExtra("startingquestionposition", JsonConvert.SerializeObject(startingquestionposition));
                    //context.StartActivityForResult(intent, 101);
                    //context.OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);
                }
                else
                {
                    var intent = new Intent(context, typeof(Solution));
                    allquestion = question;
                    //intent.PutExtra("question", question);
                    intent.PutExtra("passage", passage);
                    intent.PutExtra("path", path);
                    // intent.PutExtra("item", item);
                    //intent.PutExtra("startingquestionposition", JsonConvert.SerializeObject(startingquestionposition));
                    context.StartActivityForResult(intent, 101);
                    context.OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);
                }
            }
            catch (Exception)
            {
                cp.Dismiss();
                Dismiss();
                Toast.MakeText(context, "Test schedule is over", ToastLength.Long).Show();
            }
        }

        async void FetchSumaryFromServer(int testid,CustomProgressDialog cpd)
        {
           
            var apiresponse = RestService.For<ApiConnectionForTestPackage>(Utility.stapibaseUrl);
            var responseforsummary = await apiresponse.GetTestSummary(MainActivity1.userid + "", testid + "");
            List<TestSummaryDataModel> summarylist = JsonConvert.DeserializeObject<List<TestSummaryDataModel>>(responseforsummary.Data);

            cpd.DismissDialog();
            context.Finish();
            TestInstruction.activity.Finish();
            MyTest.activity.Finish();
            var intent1 = new Intent(context, typeof(MyTest));
            intent1.PutExtra("serilizeObj", MyTest.SerilizeString);
            context.StartActivity(intent1);
            var intent = new Intent(context, typeof(TestSummary));
            intent.PutExtra("path", "portal");
            intent.PutExtra("testsummarylist", responseforsummary.Data);
            context.StartActivityForResult(intent, 102);
            context.OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);

        }
    }
}