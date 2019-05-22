using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Webkit;
using Android.Widget;
using ImageSlider.Model;
using Java.Util;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ImageSlider.MyTest
{
    public class DosTestFragment : Fragment, View.IOnClickListener, View.IOnTouchListener, ICountdownInterface, ItestInstructionCallBack, ICustomMessageInterface
    {
        bool timerstartornot = false;
        int answeredquestion = 0;
        int markforreview = 0;
        int unseenquestion = 0;
        int unansweredquestion = 0;
        string selectedspinner = "";
        int selectedspinnerPosition = 0;
        CustomDialog submitcd = null;
        List<int> subjectidlist = new List<int>();
        List<int> subjectstartingPosition = new List<int>();
        List<int> subjectwiseTotalQuestion = new List<int>();
        LinearLayout lloption1, lloption2, lloption3, lloption4, lloption5;
        TextView txtCirclea, txtcircleb, txtcirclec, txtcircled, txtcirclee, txtmarkfroreview;
        WebView txtpassage;
        int seletedoption = 0;
        List<List<questionmodel>> Allquestionlist;
        List<List<questionmodel>> Allquestionlistfilter;
        List<questionmodel> questionlist;
        int position = 0;
        List<questionpassagemodel> passagelist;
        string path;
        ImageView ivmenu, ivnext, iveprev;
        TextView txtsubmit;
        int testid;
        float negativemarks;
        TextView txtCountdownTimer;
        int timeduration;

        string instructiontitle;
        string instruction;
        bool instructionshowornot = false;
        public static string myquestionlist;
        bool selectitemonspinnerornot = false;
        LinearLayout llMycutomSpinner;
        RecyclerView mySpinnerRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        ImageView ivdownupimage, option1Image, option2image, option3image, option4image, option5image,passageImage, passageImage2, questionImage2;
        TextView txtclearselection;
        string item, startingquestionposition, subjecttotalquestion;
        string langcode;
        ISharedPreferences pref;
        ISharedPreferencesEditor edit;
        string question;
        int drawerselelectedspinner;
        string testtype;
        List<TestInfoListRecord> testinfolistrecord;
        TextView txtSaveNext;
        CountDown countdown = null;

        bool timebondOrNot = false;

        public static DosTestFragment NewInstance(string question, string passage, string path, int position, int testid, float negativemarks, int timeduration, string instructiontitle, string instruction, bool instructionshowornot, int selectedapinnerposition, bool selectitemonspinnerornot, string items, string startingquestionposition, string subjecttotalquestion, string langcode, int drawerselelectedspinner, string testtype, bool timerstartornot)
        {
            DosTestFragment fragment = new DosTestFragment();

            Bundle args = new Bundle();
            args.PutString("question", question);
            args.PutString("passage", passage);
            args.PutString("path", path);
            args.PutInt("position", position);
            args.PutInt("testid", testid);
            args.PutFloat("negativemarks", negativemarks);
            args.PutInt("timeduration", timeduration);
            args.PutString("instructiontile", instructiontitle);
            args.PutString("instruction", instruction);
            args.PutBoolean("instructionshowornot", instructionshowornot);
            args.PutInt("selectedapinnerposition", selectedapinnerposition);
            args.PutBoolean("selectitemonspinnerornot", selectitemonspinnerornot);
            args.PutString("items", items);
            args.PutString("startingquestionposition", startingquestionposition);
            args.PutString("subjecttotalquestion", subjecttotalquestion);
            args.PutString("langcode", langcode);
            args.PutInt("drawerselelectedspinner", drawerselelectedspinner);
            args.PutString("testtype", testtype);
            args.PutBoolean("timerstartornot", timerstartornot);
            fragment.Arguments = args;

            return fragment;
        }



        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);
            View v = inflater.Inflate(Resource.Layout.DoTestFragment, container, false);

            submitcd = new CustomDialog(Activity);
            testinfolistrecord = TestInstruction.testInfoList;
            if (testinfolistrecord[0].Duration > 0)
            {
                timebondOrNot = true;
            }
            else
            {
                timebondOrNot = false;
            }
            TextView txtquestioncounter = Activity.FindViewById<TextView>(Resource.Id.incrementcount);
            txtclearselection = Activity.FindViewById<TextView>(Resource.Id.clearselection);
            txtSaveNext = Activity.FindViewById<TextView>(Resource.Id.savenext);
            ivdownupimage = Activity.FindViewById<ImageView>(Resource.Id.imagedown);
            ivdownupimage.SetImageResource(Resource.Drawable.downarrow);
            mySpinnerRecyclerView = v.FindViewById<RecyclerView>(Resource.Id.spinnerrecyclerview);
            passageImage = v.FindViewById<ImageView>(Resource.Id.passageimage);
            passageImage2 = v.FindViewById<ImageView>(Resource.Id.passageimage2);
            questionImage2 = v.FindViewById<ImageView>(Resource.Id.questionimage2);
            txtCountdownTimer = Activity.FindViewById<TextView>(Resource.Id.countdowntimer);
            TextView spinner = Activity.FindViewById<TextView>(Resource.Id.mytesttestSpinner);

            llMycutomSpinner = v.FindViewById<LinearLayout>(Resource.Id.mycustomspinner);
            item = Arguments.GetString("items", "");
            langcode = Arguments.GetString("langcode", "");
            startingquestionposition = Arguments.GetString("startingquestionposition", "");
            subjecttotalquestion = Arguments.GetString("subjecttotalquestion", "");


            List<string> items = JsonConvert.DeserializeObject<List<string>>(item);
            subjectstartingPosition = JsonConvert.DeserializeObject<List<int>>(startingquestionposition);
            subjectwiseTotalQuestion = JsonConvert.DeserializeObject<List<int>>(subjecttotalquestion);
            iveprev = Activity.FindViewById<ImageView>(Resource.Id.prev);
            ivnext = Activity.FindViewById<ImageView>(Resource.Id.next);
            ivmenu = Activity.FindViewById<ImageView>(Resource.Id.testmenuimage);
            txtsubmit = Activity.FindViewById<TextView>(Resource.Id.testsubmit);
            txtmarkfroreview = Activity.FindViewById<TextView>(Resource.Id.markforreview);
            lloption1 = v.FindViewById<LinearLayout>(Resource.Id.lloption1);
            lloption2 = v.FindViewById<LinearLayout>(Resource.Id.lloption2);
            lloption3 = v.FindViewById<LinearLayout>(Resource.Id.lloption3);
            lloption4 = v.FindViewById<LinearLayout>(Resource.Id.lloption4);
            lloption5 = v.FindViewById<LinearLayout>(Resource.Id.lloption5);
            txtCirclea = v.FindViewById<TextView>(Resource.Id.circlea);
            txtcircleb = v.FindViewById<TextView>(Resource.Id.circleb);
            txtcirclec = v.FindViewById<TextView>(Resource.Id.circlec);
            txtcircled = v.FindViewById<TextView>(Resource.Id.circled);
            txtcirclee = v.FindViewById<TextView>(Resource.Id.circlee);
            option1Image = v.FindViewById<ImageView>(Resource.Id.optionimage1);
            option2image = v.FindViewById<ImageView>(Resource.Id.optionimage2);
            option3image = v.FindViewById<ImageView>(Resource.Id.optionimage3);
            option4image = v.FindViewById<ImageView>(Resource.Id.optionimage4);
            option5image = v.FindViewById<ImageView>(Resource.Id.optionimage5);
            lloption1.SetOnClickListener(this);
            lloption2.SetOnClickListener(this);
            lloption3.SetOnClickListener(this);
            lloption4.SetOnClickListener(this);
            lloption5.SetOnClickListener(this);
            txtclearselection.SetOnClickListener(this);
            txtSaveNext.SetOnClickListener(this);
            ivmenu.SetOnClickListener(this);
            iveprev.SetOnClickListener(this);
            ivnext.SetOnClickListener(this);
            txtmarkfroreview.SetOnClickListener(this);
            txtsubmit.SetOnClickListener(this);
            spinner.SetOnClickListener(this);
            ivdownupimage.SetOnClickListener(this);

            string passage = Arguments.GetString("passage", "");
            path = Arguments.GetString("path");

            testid = Arguments.GetInt("testid", 0);
            negativemarks = Arguments.GetFloat("negativemarks");

            instructiontitle = Arguments.GetString("instructiontile");
            instruction = Arguments.GetString("instruction");
            instructionshowornot = Arguments.GetBoolean("instructionshowornot");
            selectedspinnerPosition = Arguments.GetInt("selectedapinnerposition");

            if (timebondOrNot)
            {
                drawerselelectedspinner = selectedspinnerPosition + 1;

            }
            else
            {
                drawerselelectedspinner = Arguments.GetInt("drawerselelectedspinner", 0);
            }

            selectitemonspinnerornot = Arguments.GetBoolean("selectitemonspinnerornot");
            testtype = Arguments.GetString("testtype");
            pref = Activity.GetSharedPreferences(testid + "", FileCreationMode.Private);
            edit = pref.Edit();
            question = pref.GetString("TestRecord", String.Empty);
            //==================================disable click event of submit and spinner test are time bond========================//
                if (timebondOrNot)
                {
                    timerstartornot = Arguments.GetBoolean("timerstartornot");
                    spinner.Clickable = false;
                    ivdownupimage.Clickable = false;

                    txtsubmit.Clickable = false;
                    txtsubmit.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.grey)));
                    txtsubmit.SetBackgroundResource(Resource.Drawable.greyrectangle);
                    
                    //===========================check Test is resumable or not=========================================================//
                    if (question.Length <= 0)
                    {
                        question = Arguments.GetString("question", "");
                        position = Arguments.GetInt("position", 0);
                        //=======================if next section of test open then fetch time duration of that section=====================//
                        if (timerstartornot)
                        {
                            timeduration = TestInstruction.testInfoList[selectedspinnerPosition].Duration;
                        }
                        else
                        {
                            timeduration = Arguments.GetInt("timeduration");
                        }
                    }
                    else
                    {
                        position = pref.GetInt("position", 0);
                        selectedspinnerPosition = pref.GetInt("selectedspinnerposition", 0);
                        if (timerstartornot)
                        {
                            if (instructionshowornot)
                            {
                                timeduration = pref.GetInt("timeduration", 0);
                               
                            }
                            else
                            {
                                timeduration = TestInstruction.testInfoList[selectedspinnerPosition].Duration;
                            }
                        }
                        else
                        {
                            timeduration = pref.GetInt("timeduration",0);
                        }
                    }
                   
                    //=================================================================================================================//

                }
                else
                {
                    txtsubmit.Clickable = true;
                    spinner.Clickable = true;
                    ivdownupimage.Clickable = true;
                  
                    txtsubmit.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                    txtsubmit.SetBackgroundResource(Resource.Drawable.mark_for_review);
                    if (question.Length <= 0)
                    {
                        question = Arguments.GetString("question", "");
                        position = Arguments.GetInt("position", 0);
                        //=======================if next section of test open then fetch time duration of that section=====================//
                        if (timerstartornot)
                        {
                            timeduration = TestInstruction.testInfoList[selectedspinnerPosition].Duration;
                        }
                        else
                        {
                            timeduration = Arguments.GetInt("timeduration");
                        }
                    }
                    else
                    {
                        position = pref.GetInt("position", 0);
                        selectedspinnerPosition = pref.GetInt("selectedspinnerposition", 0);
                        if (timerstartornot)
                        {
                            timeduration = TestInstruction.testInfoList[selectedspinnerPosition].Duration;
                        }
                        else
                        {
                            timeduration = pref.GetInt("timeduration", 0);
                        }
                    }


            }


            //==========================================================================================================================//
            if (timebondOrNot)
            {
                if (selectedspinnerPosition < testinfolistrecord.Count - 1)
                {
                    if (position == subjectstartingPosition[selectedspinnerPosition + 1] - 1)
                    {
                       // txtSaveNext.Clickable = false;
                        ivnext.Visibility = ViewStates.Invisible;
                        txtSaveNext.Text = "Save";
                       // txtSaveNext.SetBackgroundResource(Resource.Drawable.greenrectanglewithoutborder);
                       // txtSaveNext.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));

                    }
                    else
                    {
                        // txtSaveNext.Clickable = true;
                        txtSaveNext.Text = "Save Next";
                        ivnext.Visibility = ViewStates.Visible;
                        //txtSaveNext.SetBackgroundResource(Resource.Drawable.greenrectanglewithoutborder);
                       // txtSaveNext.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                    }
                }
                else
                {
                    txtSaveNext.Clickable = true;
                    ivnext.Visibility = ViewStates.Visible;
                    txtSaveNext.SetBackgroundResource(Resource.Drawable.greenrectanglewithoutborder);
                    txtSaveNext.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                }
                if (selectedspinnerPosition > 0)
                {
                    if (position == subjectstartingPosition[selectedspinnerPosition])
                    {
                        iveprev.Visibility = ViewStates.Invisible;
                    }
                    else
                    {
                        iveprev.Visibility = ViewStates.Visible;
                    }
                }
            }

            mLayoutManager = new LinearLayoutManager(this.Activity);
            mySpinnerRecyclerView.SetLayoutManager(mLayoutManager);
            SpinnerAdapter mAdapter = new SpinnerAdapter(Activity, items, mySpinnerRecyclerView);
            mAdapter.Item_click += MAdapter_Item_click;
            mySpinnerRecyclerView.SetAdapter(mAdapter);
            spinner.Text = items[selectedspinnerPosition];


            //=====================================Show Instruction=====================================//
            if (instructionshowornot)
            {
                InstructionCustomDialog cdi = new InstructionCustomDialog(Activity, instructiontitle, instruction, this);
                cdi.Show();
            }
            else
            {


                if (timebondOrNot)
                {
                    if (countdown != null)
                    {
                        countdown.Cancel();
                    }
                    countdown = new CountDown(timeduration, 1000, txtCountdownTimer, this);
                    countdown.Start();
                }
               
            }

            //=========================================================================================//
            int questionnumber = position + 1;
            txtquestioncounter.Text = questionnumber + "";




            LinearLayout llpaasage = v.FindViewById<LinearLayout>(Resource.Id.passagelayout);
            txtpassage = v.FindViewById<WebView>(Resource.Id.passage);
            txtpassage.SetBackgroundColor(Color.Transparent);
            WebView txtquestion = v.FindViewById<WebView>(Resource.Id.question);
            WebView txtoption1 = v.FindViewById<WebView>(Resource.Id.option1);
            ImageView ivimage = v.FindViewById<ImageView>(Resource.Id.questionimage);

            txtoption1.SetOnTouchListener(this);
            txtoption1.SetBackgroundColor(Color.Transparent);
            WebView txtoption2 = v.FindViewById<WebView>(Resource.Id.option2);
            WebView txtoption3 = v.FindViewById<WebView>(Resource.Id.option3);
            WebView txtoption4 = v.FindViewById<WebView>(Resource.Id.option4);
            WebView txtoption5 = v.FindViewById<WebView>(Resource.Id.option5);
            txtoption2.SetBackgroundColor(Color.Transparent);
            txtoption3.SetBackgroundColor(Color.Transparent);
            txtoption4.SetBackgroundColor(Color.Transparent);
            txtoption5.SetBackgroundColor(Color.Transparent);
            txtoption2.SetOnTouchListener(this);
            txtoption3.SetOnTouchListener(this);
            txtoption4.SetOnTouchListener(this);
            txtoption5.SetOnTouchListener(this);
            try
            {
                Allquestionlist = JsonConvert.DeserializeObject<List<List<questionmodel>>>(question);
                questionlist = Allquestionlist[position];
            }
            catch (Exception)
            {
                questionlist = new List<questionmodel>();
            }

            passagelist = JsonConvert.DeserializeObject<List<questionpassagemodel>>(passage);
            if (questionlist.Count < 6)
            {
                lloption5.Visibility = ViewStates.Gone;
            }
            else
            {

                lloption5.Visibility = ViewStates.Visible;
            }
            for (int i = 0; i < questionlist.Count; i++)
            {
                questionmodel objmodal = questionlist[i];
                if (objmodal.Passageid == 0)
                {

                    llpaasage.Visibility = ViewStates.Gone;
                }
                else
                {

                    llpaasage.Visibility = ViewStates.Visible;
                    for (int y = 0; y < passagelist.Count(); y++)
                    {
                        questionpassagemodel objppasagemodal = passagelist[y];
                        if (objmodal.Passageid == objppasagemodal.id)
                        {

                            //passageImage.Visibility = ViewStates.Gone;
                            //txtpassage.Settings.DomStorageEnabled = true;
                            //txtpassage.Settings.LoadsImagesAutomatically = true;
                            //txtpassage.Settings.MixedContentMode = Android.Webkit.MixedContentHandling.AlwaysAllow;

                            //txtpassage.LoadData(objppasagemodal.Passage.Trim(), "text/html", null);


                            if (objppasagemodal.Passage.Contains("IMG"))
                            {
                                string[] image1 = objppasagemodal.Passage.Split("IMG");
                                string image2 = image1[1].Split("alt=")[1];
                                string image3 = "";
                                if (image2.Contains("ostimg"))
                                {
                                    image3 = image2.Split("ostimg/")[1].Split(".")[0];
                                }
                                else if (image2.Contains("Images"))
                                {
                                    image3 = image2.Split("Images/")[1].Split(".")[0];
                                }
                                txtpassage.LoadData(image1[0].Trim(), "text/html", null);
                                string path1;
                                if (image2.Contains("jpg"))
                                {
                                    path1 = path + "/" + image3 + ".jpg";
                                }
                                else
                                {
                                    path1 = path + "/" + image3 + ".png";
                                }
                                try
                                {
                                    string image4 = image1[2].Split("alt=")[1];
                                    string image5 = "";
                                    if (image4.Contains("ostimg"))
                                    {
                                        image5 = image4.Split("ostimg/")[1].Split(".")[0];
                                    }
                                    else if (image4.Contains("Images"))
                                    {
                                        image5 = image4.Split("Images/")[1].Split(".")[0];
                                    }

                                    string path2;
                                    if (image4.Contains("jpg"))
                                    {
                                        path2 = path + "/" + image5 + ".jpg";
                                    }
                                    else
                                    {
                                        path2 = path + "/" + image5 + ".png";
                                    }
                                    passageImage2.Visibility = ViewStates.Visible;
                                    passageImage2.SetImageBitmap(BitmapFactory.DecodeFile(path2));
                                }
                                catch (Exception)
                                {

                                }
                                passageImage.Visibility = ViewStates.Visible;
                                passageImage.SetImageBitmap(BitmapFactory.DecodeFile(path1));
                            }
                            else
                            {
                                passageImage.Visibility = ViewStates.Gone;
                                txtpassage.LoadData(objppasagemodal.Passage.Trim(), "text/html", null);
                            }



                        }
                        else
                        {
                            continue;
                        }
                    }

                }
                if (objmodal.Datatype == 1)
                {
                    StringWriter myWriter = new StringWriter();

                    // Decode the encoded string.
                    HttpUtility.HtmlDecode(objmodal.Qdata, myWriter);
                    string questiondata = objmodal.Qdata.Replace("</p>", "");
                    questiondata = questiondata.Replace("\\n", "");
                    questiondata = questiondata.Replace("\\r", "");
                    questiondata = questiondata.Replace("\\/", "/");
                    seletedoption = objmodal.selectedoption;
                    DisplaySelectedQuestion(seletedoption);
                    if (objmodal.markforreview == 1)
                    {
                        txtmarkfroreview.SetBackgroundResource(Resource.Drawable.orangerectangle);
                        txtmarkfroreview.SetText(Resource.String.markedforreview);
                    }
                    else
                    {
                        txtmarkfroreview.SetBackgroundResource(Resource.Drawable.mark_for_review);
                        txtmarkfroreview.SetText(Resource.String.markforreview);
                    }


                    if (questiondata.Contains("IMG"))
                    {
                        string[] image1 = questiondata.Split("IMG");
                        string image2 = image1[1].Split("alt=")[1];
                        string image3 = "";
                        if (image2.Contains("ostimg"))
                        {
                            image3 = image2.Split("ostimg/")[1].Split(".")[0];
                        }
                        else if (image2.Contains("Images"))
                        {
                            image3 = image2.Split("Images/")[1].Split(".")[0];
                        }
                        txtquestion.LoadData("<b>Ques " + questionnumber + ":</b> " + image1[0].Trim(), "text/html", null);
                        string path1;
                        if (image2.Contains("jpg"))
                        {
                            path1 = path + "/" + image3 + ".jpg";
                        }
                        else
                        {
                            path1 = path + "/" + image3 + ".png";
                        }
                        ivimage.Visibility = ViewStates.Visible;
                        ivimage.SetImageBitmap(BitmapFactory.DecodeFile(path1));
                        ivimage.Post(() =>
                        {
                            try
                            {
                                if (ivimage.Height < 110)
                                {
                                  
                                    ivimage.LayoutParameters.Height = ivimage.Height * 2;
                                }
                                
                            }
                            catch (Exception)
                            {
                            }
                        });
                       


                        try
                        {
                            string image4 = image1[2].Split("alt=")[1];
                            string image5 = "";
                            if (image4.Contains("ostimg"))
                            {
                                image5 = image4.Split("ostimg/")[1].Split(".")[0];
                            }
                            else if (image4.Contains("Images"))
                            {
                                image5 = image4.Split("Images/")[1].Split(".")[0];
                            }

                            string path2;
                            if (image4.Contains("jpg"))
                            {
                                path2 = path + "/" + image5 + ".jpg";
                            }
                            else
                            {
                                path2 = path + "/" + image5 + ".png";
                            }
                            questionImage2.Visibility = ViewStates.Visible;
                            questionImage2.SetImageBitmap(BitmapFactory.DecodeFile(path2));
                            questionImage2.Post(() =>
                            {
                                try
                                {
                                    questionImage2.LayoutParameters.Height = questionImage2.Height * 2;

                                }
                                catch (Exception)
                                {
                                }
                            });
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        ivimage.Visibility = ViewStates.Gone;
                        txtquestion.LoadData("<b>Ques " + questionnumber + ":</b> " + questiondata.Trim(), "text/html", null);
                    }
                    //txtquestion.Text = myWriter.ToString();
                }
                else
                {
                    if (objmodal.Optnseqno == 1)
                    {

                        if (objmodal.Qdata.Contains("IMG"))
                        {
                            string[] image1 = objmodal.Qdata.Split("IMG");
                            string image2 = image1[1].Split("alt=")[1];
                            string image3 = "";
                            if (image2.Contains("ostimg"))
                            {
                                image3 = image2.Split("ostimg/")[1].Split(".")[0];
                            }
                            else if (image2.Contains("Images"))
                            {
                                image3 = image2.Split("Images/")[1].Split(".")[0];
                            }

                            string path1;
                            if (image2.Contains("jpg"))
                            {
                                path1 = path + "/" + image3 + ".jpg";
                            }
                            else
                            {
                                path1 = path + "/" + image3 + ".png";
                            }
                            option1Image.Visibility = ViewStates.Visible;
                            option1Image.SetImageBitmap(BitmapFactory.DecodeFile(path1));
                            option1Image.Post(() =>
                            {
                                try
                                {
                                    option1Image.LayoutParameters.Height = option1Image.Height * 2;
                                    
                                }
                                catch (Exception)
                                {
                                }
                            });
                            txtoption1.Visibility = ViewStates.Gone;

                        }
                        else
                        {
                            option1Image.Visibility = ViewStates.Gone;
                            string option1 = objmodal.Qdata.Replace("\\/", "/");
                            txtoption1.LoadData(option1, "text/html", null);
                        }


                    }
                    else if (objmodal.Optnseqno == 2)
                    {
                        if (objmodal.Qdata.Contains("IMG"))
                        {
                            string[] image1 = objmodal.Qdata.Split("IMG");
                            string image2 = image1[1].Split("alt=")[1];
                            string image3 = "";
                            if (image2.Contains("ostimg"))
                            {
                                image3 = image2.Split("ostimg/")[1].Split(".")[0];
                            }
                            else if (image2.Contains("Images"))
                            {
                                image3 = image2.Split("Images/")[1].Split(".")[0];
                            }

                            string path1;
                            if (image2.Contains("jpg"))
                            {
                                path1 = path + "/" + image3 + ".jpg";
                            }
                            else
                            {
                                path1 = path + "/" + image3 + ".png";
                            }
                            option2image.Visibility = ViewStates.Visible;
                            option2image.SetImageBitmap(BitmapFactory.DecodeFile(path1));
                            option2image.Post(() =>
                            {
                                try
                                {
                                    option2image.LayoutParameters.Height = option2image.Height * 2;
                                    
                                }
                                catch (Exception)
                                {
                                }
                            });
                            txtoption2.Visibility = ViewStates.Gone;

                        }
                        else
                        {
                            option2image.Visibility = ViewStates.Gone;
                            string option2 = objmodal.Qdata.Replace("\\/", "/");
                            txtoption2.LoadData(option2, "text/html", null);
                        }
                    }
                    else if (objmodal.Optnseqno == 3)
                    {
                        if (objmodal.Qdata.Contains("IMG"))
                        {
                            string[] image1 = objmodal.Qdata.Split("IMG");
                            string image2 = image1[1].Split("alt=")[1];
                            string image3 = "";
                            if (image2.Contains("ostimg"))
                            {
                                image3 = image2.Split("ostimg/")[1].Split(".")[0];
                            }
                            else if (image2.Contains("Images"))
                            {
                                image3 = image2.Split("Images/")[1].Split(".")[0];
                            }

                            string path1;
                            if (image2.Contains("jpg"))
                            {
                                path1 = path + "/" + image3 + ".jpg";
                            }
                            else
                            {
                                path1 = path + "/" + image3 + ".png";
                            }
                            option3image.Visibility = ViewStates.Visible;
                            option3image.SetImageBitmap(BitmapFactory.DecodeFile(path1));
                            option3image.Post(() =>
                            {
                                try
                                {
                                    option3image.LayoutParameters.Height = option3image.Height * 2;
                                    
                                }
                                catch (Exception)
                                {
                                }
                            });
                            txtoption3.Visibility = ViewStates.Gone;

                        }
                        else
                        {
                            option3image.Visibility = ViewStates.Gone;
                            string option3 = objmodal.Qdata.Replace("\\/", "/");
                            txtoption3.LoadData(option3, "text/html", null);
                        }
                    }
                    else if (objmodal.Optnseqno == 4)
                    {
                        if (objmodal.Qdata.Contains("IMG"))
                        {
                            string[] image1 = objmodal.Qdata.Split("IMG");
                            string image2 = image1[1].Split("alt=")[1];
                            string image3 = "";
                            if (image2.Contains("ostimg"))
                            {
                                image3 = image2.Split("ostimg/")[1].Split(".")[0];
                            }
                            else if (image2.Contains("Images"))
                            {
                                image3 = image2.Split("Images/")[1].Split(".")[0];
                            }

                            string path1;
                            if (image2.Contains("jpg"))
                            {
                                path1 = path + "/" + image3 + ".jpg";
                            }
                            else
                            {
                                path1 = path + "/" + image3 + ".png";
                            }
                            option4image.Visibility = ViewStates.Visible;
                            option4image.SetImageBitmap(BitmapFactory.DecodeFile(path1));
                            option4image.Post(() =>
                            {
                                try
                                {
                                    option4image.LayoutParameters.Height = option2image.Height * 2;
                                    
                                }
                                catch (Exception)
                                {
                                }
                            });
                            txtoption4.Visibility = ViewStates.Gone;

                        }
                        else
                        {
                            option4image.Visibility = ViewStates.Gone;
                            string option4 = objmodal.Qdata.Replace("\\/", "/");
                            txtoption4.LoadData(option4, "text/html", null);
                        }
                    }
                    else if (objmodal.Optnseqno == 5)
                    {
                        if (objmodal.Qdata.Contains("IMG"))
                        {
                            string[] image1 = objmodal.Qdata.Split("IMG");
                            string image2 = image1[1].Split("alt=")[1];
                            string image3 = "";
                            if (image2.Contains("ostimg"))
                            {
                                image3 = image2.Split("ostimg/")[1].Split(".")[0];
                            }
                            else if (image2.Contains("Images"))
                            {
                                image3 = image2.Split("Images/")[1].Split(".")[0];
                            }

                            string path1;
                            if (image2.Contains("jpg"))
                            {
                                path1 = path + "/" + image3 + ".jpg";
                            }
                            else
                            {
                                path1 = path + "/" + image3 + ".png";
                            }
                            option5image.Visibility = ViewStates.Visible;
                            option5image.SetImageBitmap(BitmapFactory.DecodeFile(path1));
                            option5image.Post(() =>
                            {
                                try
                                {
                                    option5image.LayoutParameters.Height = option2image.Height * 2;
                                   
                                }
                                catch (Exception)
                                {
                                }
                            });
                            txtoption5.Visibility = ViewStates.Gone;

                        }
                        else
                        {
                            option5image.Visibility = ViewStates.Gone;
                            string option5 = objmodal.Qdata.Replace("\\/", "/");
                            txtoption5.LoadData(option5, "text/html", null);
                        }
                    }

                }


            }
            return v;


        }

        private void MAdapter_Item_click(object sender, int e)
        {
            selectedspinnerPosition = e;
            position = subjectstartingPosition[e];
            edit.Clear();
            edit.PutString("TestRecord", JsonConvert.SerializeObject(Allquestionlist));
            edit.PutInt("position", position);
            edit.PutInt("selectedspinnerposition", selectedspinnerPosition);
            int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
            int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
            int seconds = Int32.Parse(txtCountdownTimer.Text.Split(":")[2]);
            int hourmilisecond = hour * 60 * 60 * 1000;
            int minutemilisecond = minute * 60 * 1000;
            int secondsmilisecond = seconds * 1000;
            int mytimeduration = hourmilisecond + minutemilisecond + secondsmilisecond;
            //int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
            //int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
            //int mytimeduration;
            //if (hour > 0)
            //{
            //    mytimeduration = (hour * 60) + minute;
            //}
            //else
            //{
            //    mytimeduration = minute;
            //}
            edit.PutInt("timeduration", mytimeduration);
            edit.Apply();
            DosTestFragment dotestfragment = DosTestFragment.NewInstance(JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, position, testid, negativemarks, timeduration, instructiontitle, instruction, false, selectedspinnerPosition, selectitemonspinnerornot, item, startingquestionposition, subjecttotalquestion, langcode, drawerselelectedspinner, testtype, timerstartornot);
            Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testpaperviewpager, dotestfragment).CommitAllowingStateLoss();
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 101)
            {
                /* activity which needs to handle your photo capture event */
                int position = data.GetIntExtra("position", 0);

                if (position != 4004)
                {
                    drawerselelectedspinner = 0;
                    drawerselelectedspinner = data.GetIntExtra("selectedspinnerPosition", 0);
                    int myposition = position + 1;

                    for (int i = 0; i < subjectstartingPosition.Count; i++)
                    {
                        if (myposition >= subjectstartingPosition[i])
                        {
                            if (i == subjectstartingPosition.Count - 1)
                            {
                                selectedspinnerPosition = i;
                            }
                            else
                            {

                                if (myposition > subjectstartingPosition[i] && myposition <= subjectstartingPosition[i + 1])
                                {
                                    selectedspinnerPosition = i;
                                    break;

                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            selectedspinnerPosition = i;
                            break;
                        }
                    }

                    edit.Clear();
                    edit.PutString("TestRecord", JsonConvert.SerializeObject(Allquestionlist));
                    edit.PutInt("selectedspinnerposition", selectedspinnerPosition);
                    edit.PutInt("position", position);
                    int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
                    int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
                    int seconds = Int32.Parse(txtCountdownTimer.Text.Split(":")[2]);
                    int hourmilisecond = hour * 60 * 60 * 1000;
                    int minutemilisecond = minute * 60 * 1000;
                    int secondsmilisecond = seconds * 1000;
                    int mytimeduration = hourmilisecond + minutemilisecond + secondsmilisecond;
                    //int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
                    //int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
                    //int mytimeduration;
                    //if (hour > 0)
                    //{
                    //    mytimeduration = (hour * 60) + minute;
                    //}
                    //else
                    //{
                    //    mytimeduration = minute;
                    //}
                    edit.PutInt("timeduration", mytimeduration);
                    edit.Apply();
                    if (countdown != null)
                    {
                        if (timebondOrNot)
                        {
                            countdown.Cancel();
                        }
                    }
                    DosTestFragment dotestfragment = DosTestFragment.NewInstance(JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, position, testid, negativemarks, timeduration, instructiontitle, instruction, false, selectedspinnerPosition, selectitemonspinnerornot, item, startingquestionposition, subjecttotalquestion, langcode, drawerselelectedspinner, testtype, timerstartornot);
                    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testpaperviewpager, dotestfragment).CommitAllowingStateLoss();
                }

            }
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {

                Spinner spinner = (Spinner)sender;
                // selectedspinner = spinner.GetItemAtPosition(e.Position) + "";
                selectedspinnerPosition = e.Position;
                position = subjectstartingPosition[e.Position];
                // DosTestFragment dotestfragment = DosTestFragment.NewInstance(JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, position, testid, negativemarks, timeduration, instructiontitle, instruction, false, selectedspinnerPosition, selectitemonspinnerornot);
                // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testpaperviewpager, dotestfragment).Commit();

            }
            catch (Exception)
            {
            }
        }
        public void DisplaySelectedQuestion(int selectedoption)
        {
            if (selectedoption == 0)
            {
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (selectedoption == 1)
            {
                lloption1.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (selectedoption == 2)
            {
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));

            }
            else if (selectedoption == 3)
            {
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (selectedoption == 4)
            {
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (selectedoption == 5)
            {
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
            }


        }


        public void OnClick(View v)
        {
            int itemid = v.Id;
            if (itemid == Resource.Id.lloption1 || itemid == Resource.Id.option1)
            {

                seletedoption = 1;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                lloption1.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (itemid == Resource.Id.lloption2)
            {
                seletedoption = 2;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (itemid == Resource.Id.lloption3)
            {
                seletedoption = 3;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (itemid == Resource.Id.lloption4)
            {
                seletedoption = 4;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }


            else if (itemid == Resource.Id.clearselection)
            {
                seletedoption = 0;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.grey)));
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (itemid == Resource.Id.testmenuimage)
            {
                try
                {
                    var intent = new Intent(Activity, typeof(rightdrawer));
                    myquestionlist = JsonConvert.SerializeObject(Allquestionlist);
                    // intent.PutExtra("QuestionList", myquestionlist);
                    intent.PutExtra("items", item);
                    intent.PutExtra("startingquestionposition", startingquestionposition);
                    intent.PutExtra("position", position);
                    intent.PutExtra("drawerselelectedspinner", drawerselelectedspinner);
                    StartActivityForResult(intent, 101);
                    Activity.OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);
                }
                catch (Exception e)
                {
                    Toast.MakeText(Activity, "eroor->" + e.Message, ToastLength.Long).Show();
                }

            }

            else if (itemid == Resource.Id.prev)
            {

                try
                {
                    if (position >= 1)
                    {


                        int totalquestionpaperwise = subjectstartingPosition[selectedspinnerPosition];
                        if (position <= totalquestionpaperwise)
                        {
                            selectedspinnerPosition = selectedspinnerPosition - 1;
                        }
                        position = position - 1;
                        edit.Clear();
                        edit.PutString("TestRecord", JsonConvert.SerializeObject(Allquestionlist));
                        edit.PutInt("selectedspinnerposition", selectedspinnerPosition);
                        edit.PutInt("position", position);
                        int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
                        int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
                        int seconds = Int32.Parse(txtCountdownTimer.Text.Split(":")[2]);
                        int hourmilisecond = hour * 60 * 60 * 1000;
                        int minutemilisecond = minute * 60 * 1000;
                        int secondsmilisecond = seconds * 1000;
                        int mytimeduration = hourmilisecond + minutemilisecond + secondsmilisecond;
                        //int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
                        //int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
                        //int mytimeduration;
                        //if (hour > 0)
                        //{
                        //    mytimeduration = (hour * 60) + minute;
                        //}
                        //else
                        //{
                        //    mytimeduration = minute;
                        //}
                        edit.PutInt("timeduration", mytimeduration);
                        edit.Apply();
                        if (countdown != null)
                        {
                            if (timebondOrNot)
                            {
                                countdown.Cancel();
                            }
                        }
                        DosTestFragment dotestfragment = DosTestFragment.NewInstance(JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, position, testid, negativemarks, timeduration, instructiontitle, instruction, false, selectedspinnerPosition, selectitemonspinnerornot, item, startingquestionposition, subjecttotalquestion, langcode, drawerselelectedspinner, testtype, false);
                        // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testpaperviewpager, dotestfragment).Commit();
                        FragmentTransaction ft = Activity.SupportFragmentManager.BeginTransaction();
                        ft.SetCustomAnimations(Resource.Animation.enter_from_left, Resource.Animation.exit_to_right, Resource.Animation.enter_from_right, Resource.Animation.exit_to_left);
                        ft.Replace(Resource.Id.testpaperviewpager, dotestfragment);
                        ft.CommitAllowingStateLoss();
                    }
                }
                catch (Exception)
                {

                }
            }
            else if (itemid == Resource.Id.testsubmit)
            {
                answeredquestion = 0;
                markforreview = 0;
                unseenquestion = 0;
                answeredquestion = 0;
                for (int i = 0; i < Allquestionlist.Count; i++)
                {
                    List<questionmodel> question = Allquestionlist[i];
                    for (int y = 0; y < question.Count; y++)
                    {
                        if (question[y].Datatype == 1)
                        {
                            if (question[y].selectedoption == 0 && question[y].colorcode == Resource.Drawable.whitecircle1)
                            {
                                unseenquestion = unseenquestion + 1;

                            }
                            else if (question[y].selectedoption == 0 && question[y].markforreview == 1)
                            {
                                markforreview = markforreview + 1;
                            }
                            else if (question[y].selectedoption == 0)
                            {
                                unansweredquestion = unansweredquestion + 1;
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
                myquestionlist = JsonConvert.SerializeObject(Allquestionlist);
                submitcd = new CustomDialog(Activity, answeredquestion, markforreview, unseenquestion, JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, testid, negativemarks, false, edit, item, subjectstartingPosition, langcode, testtype,true,unansweredquestion);

                submitcd.SetCanceledOnTouchOutside(false);
                submitcd.Show();
            }
            else if (itemid == Resource.Id.markforreview)
            {
                for (int i = 0; i < questionlist.Count; i++)
                {
                    if (questionlist[i].Datatype == 1)
                    {
                        Allquestionlist[position][i].markforreview = 1;
                        txtmarkfroreview.SetBackgroundResource(Resource.Drawable.orangerectangle);
                        txtmarkfroreview.SetText(Resource.String.markedforreview);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else if (itemid == Resource.Id.savenext || itemid == Resource.Id.next)
            {
                try
                {
                    if (position < Allquestionlist.Count())
                    {
                        if (selectedspinnerPosition < subjectstartingPosition.Count - 1)
                        {
                            int totalquestionpaperwise = subjectstartingPosition[selectedspinnerPosition + 1];
                            if (position >= totalquestionpaperwise - 1)
                            {
                                selectedspinnerPosition = selectedspinnerPosition + 1;
                            }
                        }



                        for (int i = 0; i < questionlist.Count; i++)
                        {
                            if (questionlist[i].Datatype == 1)
                            {
                                Allquestionlist[position][i].selectedoption = seletedoption;
                                if (seletedoption > 0)
                                {
                                    if (Allquestionlist[position][i].markforreview == 1)
                                    {
                                        Allquestionlist[position][i].colorcode = Resource.Drawable.orangecirclewithgreen;
                                        Allquestionlist[position][i].textcolor = Resource.Color.white;
                                    }
                                    else
                                    {
                                        if (Allquestionlist[position][i].Correctans.Equals(seletedoption + ""))
                                        {
                                            Allquestionlist[position][i].rightorwrongColorCode = Resource.Drawable.greenCircle;
                                            Allquestionlist[position][i].rightorwrongTextColor = Resource.Color.white;
                                        }
                                        else
                                        {
                                            Allquestionlist[position][i].rightorwrongColorCode = Resource.Drawable.redcircle;
                                            Allquestionlist[position][i].rightorwrongTextColor = Resource.Color.white;
                                        }
                                        Allquestionlist[position][i].colorcode = Resource.Drawable.greenCircle;
                                        Allquestionlist[position][i].textcolor = Resource.Color.white;

                                    }
                                }
                                else
                                {
                                    if (Allquestionlist[position][i].markforreview == 1)
                                    {
                                        Allquestionlist[position][i].rightorwrongColorCode = Resource.Drawable.whitecircle1;
                                        Allquestionlist[position][i].rightorwrongTextColor = Resource.Color.black;
                                        Allquestionlist[position][i].colorcode = Resource.Drawable.orangecircle;
                                        Allquestionlist[position][i].textcolor = Resource.Color.white;
                                    }
                                    else
                                    {
                                        Allquestionlist[position][i].rightorwrongColorCode = Resource.Drawable.whitecircle1;
                                        Allquestionlist[position][i].rightorwrongTextColor = Resource.Color.black;
                                        Allquestionlist[position][i].colorcode = Resource.Drawable.redcircle;
                                        Allquestionlist[position][i].textcolor = Resource.Color.white;
                                    }
                                }


                                if (position < Allquestionlist.Count() - 1)
                                {
                                    if (timebondOrNot)
                                    {
                                        if (ivnext.Visibility == ViewStates.Visible)
                                        {
                                            position = position + 1;
                                            edit.Clear();
                                            edit.PutString("TestRecord", JsonConvert.SerializeObject(Allquestionlist));
                                            edit.PutInt("position", position);
                                            int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
                                            int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
                                            int seconds = Int32.Parse(txtCountdownTimer.Text.Split(":")[2]);
                                            int hourmilisecond = hour * 60 * 60 * 1000;
                                            int minutemilisecond = minute * 60 * 1000;
                                            int secondsmilisecond = seconds * 1000;
                                            int mytimeduration = hourmilisecond + minutemilisecond + secondsmilisecond;
                                            edit.PutInt("selectedspinnerposition", selectedspinnerPosition);
                                            edit.PutInt("timeduration", mytimeduration);
                                            edit.Apply();
                                            if (countdown != null)
                                            {
                                                if (timebondOrNot)
                                                {
                                                    countdown.Cancel();
                                                }
                                            }
                                            DosTestFragment dotestfragment = DosTestFragment.NewInstance(JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, position, testid, negativemarks, timeduration, instructiontitle, instruction, false, selectedspinnerPosition, selectitemonspinnerornot, item, startingquestionposition, subjecttotalquestion, langcode, drawerselelectedspinner, testtype, false);
                                            // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testpaperviewpager, dotestfragment).Commit();
                                            FragmentTransaction ft = Activity.SupportFragmentManager.BeginTransaction();
                                            ft.SetCustomAnimations(Resource.Animation.enter_from_right, Resource.Animation.exit_to_left, Resource.Animation.enter_from_left, Resource.Animation.exit_to_right);
                                            ft.Replace(Resource.Id.testpaperviewpager, dotestfragment);
                                            ft.CommitAllowingStateLoss();
                                        }
                                        else
                                        {
                                            edit.Clear();
                                            edit.PutString("TestRecord", JsonConvert.SerializeObject(Allquestionlist));
                                            edit.PutInt("selectedspinnerposition", selectedspinnerPosition);
                                            edit.PutInt("position", position);
                                            int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
                                            int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
                                            int seconds = Int32.Parse(txtCountdownTimer.Text.Split(":")[2]);
                                            int hourmilisecond = hour * 60 * 60 * 1000;
                                            int minutemilisecond = minute * 60 * 1000;
                                            int secondsmilisecond = seconds * 1000;
                                            int mytimeduration = hourmilisecond + minutemilisecond + secondsmilisecond;

                                            edit.PutInt("timeduration", mytimeduration);
                                            edit.Apply();
                                          
                                            if (timebondOrNot)
                                            {
                                                if (countdown != null)
                                                {
                                                    countdown.Cancel();
                                                }
                                                countdown = new CountDown(mytimeduration, 1000, txtCountdownTimer, this);
                                                countdown.Start();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        position = position + 1;


                                        edit.Clear();
                                        edit.PutString("TestRecord", JsonConvert.SerializeObject(Allquestionlist));
                                        edit.PutInt("position", position);
                                        edit.PutInt("selectedspinnerposition", selectedspinnerPosition);
                                        int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
                                        int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
                                        int seconds = Int32.Parse(txtCountdownTimer.Text.Split(":")[2]);
                                        int hourmilisecond = hour * 60 * 60 * 1000;
                                        int minutemilisecond = minute * 60 * 1000;
                                        int secondsmilisecond = seconds * 1000;
                                        int mytimeduration = hourmilisecond + minutemilisecond + secondsmilisecond;
                                        
                                        edit.PutInt("timeduration", mytimeduration);
                                        edit.Apply();
                                        if (countdown != null)
                                        {
                                            if (timebondOrNot)
                                            {
                                                countdown.Cancel();
                                            }
                                        }
                                        DosTestFragment dotestfragment = DosTestFragment.NewInstance(JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, position, testid, negativemarks, timeduration, instructiontitle, instruction, false, selectedspinnerPosition, selectitemonspinnerornot, item, startingquestionposition, subjecttotalquestion, langcode, drawerselelectedspinner, testtype, false);
                                        // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testpaperviewpager, dotestfragment).Commit();
                                        FragmentTransaction ft = Activity.SupportFragmentManager.BeginTransaction();
                                        ft.SetCustomAnimations(Resource.Animation.enter_from_right, Resource.Animation.exit_to_left, Resource.Animation.enter_from_left, Resource.Animation.exit_to_right);
                                        ft.Replace(Resource.Id.testpaperviewpager, dotestfragment);
                                        ft.CommitAllowingStateLoss();
                                    }
                                }
                                else
                                {
                                    if (!timebondOrNot)
                                    {
                                        CustomMessageDialog cmd = new CustomMessageDialog(Activity, this);
                                        cmd.Show();
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
                catch (Exception)
                {
                }
            }
            else if (itemid == Resource.Id.mytesttestSpinner || itemid == Resource.Id.imagedown)
            {

                if (llMycutomSpinner.Visibility == ViewStates.Visible)
                {
                    llMycutomSpinner.Visibility = ViewStates.Gone;
                    ivdownupimage.SetImageResource(Resource.Drawable.downarrow);
                }
                else
                {
                    llMycutomSpinner.Visibility = ViewStates.Visible;
                    ivdownupimage.SetImageResource(Resource.Drawable.up);
                }
            }
            else
            {
                seletedoption = 5;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
            }


        }

        public bool OnTouch(View v, MotionEvent e)
        {
            int itemid = v.Id;


            if (itemid == Resource.Id.option1 && e.Action == MotionEventActions.Up)
            {
                seletedoption = 1;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                lloption1.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (itemid == Resource.Id.option2 && e.Action == MotionEventActions.Up)
            {
                seletedoption = 2;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (itemid == Resource.Id.option3 && e.Action == MotionEventActions.Up)
            {
                seletedoption = 3;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }
            else if (itemid == Resource.Id.option4 && e.Action == MotionEventActions.Up)
            {
                seletedoption = 4;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                lloption5.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtcirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
            }

            else if (itemid == Resource.Id.option5 && e.Action == MotionEventActions.Up)
            {
                seletedoption = 5;
                txtclearselection.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                lloption1.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption2.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption3.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption4.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                lloption5.SetBackgroundResource(Resource.Drawable.blueRectangleWithStroke);
                txtCirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                txtcirclee.SetBackgroundResource(Resource.Drawable.bluecircle);
                txtCirclea.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircleb.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclec.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcircled.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.black)));
                txtcirclee.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
            }


            return false;
        }

        public void Countdowncallback()
        {
           

            if (!submitcd.IsShowing)
            {
                if (timebondOrNot)
                {
                    timerstartornot = true;
                    selectedspinnerPosition = selectedspinnerPosition + 1;
                    if (selectedspinnerPosition < testinfolistrecord.Count)
                    {
                        position = subjectstartingPosition[selectedspinnerPosition];
                        edit.Clear();
                        edit.PutString("TestRecord", JsonConvert.SerializeObject(Allquestionlist));
                        edit.PutInt("position", position);
                        edit.PutInt("selectedspinnerposition", selectedspinnerPosition);
                        timeduration = testinfolistrecord[selectedspinnerPosition].Duration;

                        edit.PutInt("timeduration", timeduration);
                        edit.Apply();
                        try
                        {
                            DosTestFragment dotestfragment = DosTestFragment.NewInstance(JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, position, testid, negativemarks, timeduration, instructiontitle, instruction, false, selectedspinnerPosition, selectitemonspinnerornot, item, startingquestionposition, subjecttotalquestion, langcode, drawerselelectedspinner, testtype, timerstartornot);
                            // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testpaperviewpager, dotestfragment).Commit();
                            FragmentTransaction ft = Activity.SupportFragmentManager.BeginTransaction();
                            ft.SetCustomAnimations(Resource.Animation.enter_from_right, Resource.Animation.exit_to_left, Resource.Animation.enter_from_left, Resource.Animation.exit_to_right);
                            ft.Replace(Resource.Id.testpaperviewpager, dotestfragment);
                            ft.CommitAllowingStateLoss();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                    else
                    {
                        answeredquestion = 0;
                        markforreview = 0;
                        unseenquestion = 0;
                        unansweredquestion = 0;
                        for (int i = 0; i < Allquestionlist.Count; i++)
                        {
                            List<questionmodel> question = Allquestionlist[i];
                            for (int y = 0; y < question.Count; y++)
                            {
                                if (question[y].Datatype == 1)
                                {
                                    if (question[y].selectedoption == 0 && question[y].colorcode == Resource.Drawable.whitecircle1)
                                    {
                                        unseenquestion = unseenquestion + 1;

                                    }
                                    else if (question[y].selectedoption == 0 && question[y].markforreview == 1)
                                    {
                                        markforreview = markforreview + 1;
                                    }
                                    else if (question[y].selectedoption == 0)
                                    {
                                        unansweredquestion = unansweredquestion + 1;
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
                        try
                        {
                            txtsubmit.Clickable = true;

                            txtsubmit.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                            txtsubmit.SetBackgroundResource(Resource.Drawable.mark_for_review);
                            myquestionlist = JsonConvert.SerializeObject(Allquestionlist);
                            submitcd = new CustomDialog(Activity, answeredquestion, markforreview, unseenquestion, JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, testid, negativemarks, false, edit, item, subjectstartingPosition, langcode, testtype,false,unansweredquestion);

                            submitcd.SetCanceledOnTouchOutside(false);
                            submitcd.Show();
                        }
                        catch (Exception)
                        {

                        }
                    }

                }
                else
                {
                    answeredquestion = 0;
                    markforreview = 0;
                    unseenquestion = 0;
                    unansweredquestion = 0;
                    for (int i = 0; i < Allquestionlist.Count; i++)
                    {
                        List<questionmodel> question = Allquestionlist[i];
                        for (int y = 0; y < question.Count; y++)
                        {
                            if (question[y].Datatype == 1)
                            {
                                if (question[y].selectedoption == 0 && question[y].colorcode == Resource.Drawable.whitecircle1)
                                {
                                    unseenquestion = unseenquestion + 1;

                                }
                                else if (question[y].selectedoption == 0 && question[y].markforreview == 1)
                                {
                                    markforreview = markforreview + 1;
                                }
                                else if (question[y].selectedoption == 0)
                                {
                                    unansweredquestion = unansweredquestion + 1;
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
                    try
                    {
                        txtsubmit.Clickable = true;

                        txtsubmit.SetTextColor(new Color(ContextCompat.GetColor(Activity, Resource.Color.white)));
                        txtsubmit.SetBackgroundResource(Resource.Drawable.mark_for_review);
                        myquestionlist = JsonConvert.SerializeObject(Allquestionlist);
                        submitcd = new CustomDialog(Activity, answeredquestion, markforreview, unseenquestion, JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, testid, negativemarks, false, edit, item, subjectstartingPosition, langcode, testtype,false,unansweredquestion);

                        submitcd.SetCanceledOnTouchOutside(false);
                        submitcd.Show();
                    }
                    catch (Exception)
                    {

                    }
                }

            }




        }

        public void TestinstructionCallBack()
        {
            if (instructionshowornot)
            {
                if (countdown != null)
                {
                    if (timebondOrNot)
                    {
                        countdown.Cancel();
                    }
                }
                countdown = new CountDown(timeduration, 1000, txtCountdownTimer, this);
                countdown.Start();
            }
        }

        public void CustomMessageCallBack()
        {

            position = 0;
            selectedspinnerPosition = 0;
            edit.Clear();
            edit.PutString("TestRecord", JsonConvert.SerializeObject(Allquestionlist));
            edit.PutInt("position", position);
            edit.PutInt("selectedspinnerposition", selectedspinnerPosition);
            edit.PutBoolean("testinstructionshowornot", false);
            int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
            int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
            int seconds = Int32.Parse(txtCountdownTimer.Text.Split(":")[2]);
            int hourmilisecond = hour * 60 * 60 * 1000;
            int minutemilisecond = minute * 60 * 1000;
            int secondsmilisecond = seconds * 1000;
            int mytimeduration = hourmilisecond + minutemilisecond + secondsmilisecond;
            //int hour = Int32.Parse(txtCountdownTimer.Text.Split(":")[0]);
            //int minute = Int32.Parse(txtCountdownTimer.Text.Split(":")[1]);
            //int mytimeduration;
            //if (hour > 0)
            //{
            //    mytimeduration = (hour * 60) + minute;
            //}
            //else
            //{
            //    mytimeduration = minute;
            //}
            edit.PutInt("timeduration", mytimeduration);
            edit.Apply();
            try
            {
                if (countdown != null)
                {
                    if (timebondOrNot)
                    {
                        countdown.Cancel();
                    }
                }
                DosTestFragment dotestfragment = DosTestFragment.NewInstance(JsonConvert.SerializeObject(Allquestionlist), JsonConvert.SerializeObject(passagelist), path, position, testid, negativemarks, timeduration, instructiontitle, instruction, false, selectedspinnerPosition, selectitemonspinnerornot, item, startingquestionposition, subjecttotalquestion, langcode, drawerselelectedspinner, testtype, timerstartornot);
                // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testpaperviewpager, dotestfragment).Commit();
                FragmentTransaction ft = Activity.SupportFragmentManager.BeginTransaction();
                ft.SetCustomAnimations(Resource.Animation.enter_from_right, Resource.Animation.exit_to_left, Resource.Animation.enter_from_left, Resource.Animation.exit_to_right);
                ft.Replace(Resource.Id.testpaperviewpager, dotestfragment);
                ft.CommitAllowingStateLoss();
            }
            catch (Exception e)
            {

            }
        }



        public override void OnSaveInstanceState(Bundle outState)
        {

        }


    }


}