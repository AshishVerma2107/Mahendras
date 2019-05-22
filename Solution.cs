using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Newtonsoft.Json;

namespace ImageSlider.MyTest
{
    [Activity(Label = "Solution" ,Theme ="@style/MyTheme1")]
    public class Solution : Activity,View.IOnClickListener
    {
        Android.Support.V7.Widget.RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        AnswerPlatee answerplate;
        List<AnswerPlatee> answePlateList = new List<AnswerPlatee>();
        LinearLayout llanswera, llanswerb, llanswerc, llanswerd, llanswere, llpaasage;
        WebView txtpassage, txtquestion, txtoption1, txtoption2, txtoption3, txtoption4, txtoption5;
        ImageView ivimage;
        ImageView  option1Image, option2image, option3image, option4image, option5image, passageImage, passageImage2, questionImage2;
        TextView txtRightOrWrong,txtinsidecirclea,txtinsidecircleb,txtinsidecirclec,txtinsidecircled,txtinsidecirclee;
        WebView txtoptiona, txtoptionb, txtoptionc, txtoptiond, txtoptione;
        int stripeposition = 0;
        List<List<questionmodel>> Allquestionlist = new List<List<questionmodel>>();
        List<List<questionmodel>> AllquestionlistFilter;
        List<questionmodel> questionlist = new List<questionmodel>();
        string path;
        TextView txtshowcorrectanswer;
        List<questionpassagemodel> passagelist = new List<questionpassagemodel>();
        TextView txtAll, txtCorrect, txtWrong, txtSkipped;
        bool filterornot = false;
        int selectedposition = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.solution);
            Allquestionlist = JsonConvert.DeserializeObject<List<List<questionmodel>>>(CustomDialog.allquestion);
            passagelist = JsonConvert.DeserializeObject<List<questionpassagemodel>>(Intent.GetStringExtra("passage"));
            path = Intent.GetStringExtra("path");

            txtshowcorrectanswer = FindViewById<TextView>(Resource.Id.showcorrectanswer);
            llanswera = FindViewById<LinearLayout>(Resource.Id.soultionanwera);
            llanswerb = FindViewById<LinearLayout>(Resource.Id.solutionanwerb);
            llanswerc = FindViewById<LinearLayout>(Resource.Id.solutionanswerc);
            llanswerd = FindViewById<LinearLayout>(Resource.Id.solutionanswerd);
            llanswere = FindViewById<LinearLayout>(Resource.Id.solutionanswere);
            passageImage = FindViewById<ImageView>(Resource.Id.passageimage);
            passageImage2 = FindViewById<ImageView>(Resource.Id.passageimage2);
            questionImage2 = FindViewById<ImageView>(Resource.Id.solutionquestionimage2);

            option1Image = FindViewById<ImageView>(Resource.Id.optionimage1);
            option2image = FindViewById<ImageView>(Resource.Id.optionimage2);
            option3image = FindViewById<ImageView>(Resource.Id.optionimage3);
            option4image = FindViewById<ImageView>(Resource.Id.optionimage4);
            option5image = FindViewById<ImageView>(Resource.Id.optionimage5);
            txtAll = FindViewById<TextView>(Resource.Id.tabAll);
            txtCorrect = FindViewById<TextView>(Resource.Id.tabcorrect);
            txtWrong = FindViewById<TextView>(Resource.Id.tabwrong);
            txtSkipped = FindViewById<TextView>(Resource.Id.tabskipped);
            txtAll.SetOnClickListener(this);
            txtCorrect.SetOnClickListener(this);
            txtWrong.SetOnClickListener(this);
            txtSkipped.SetOnClickListener(this);
            txtshowcorrectanswer.SetOnClickListener(this);

            txtinsidecirclea = FindViewById<TextView>(Resource.Id.insidecirclea);
            txtinsidecircleb = FindViewById<TextView>(Resource.Id.insidecircleb);
            txtinsidecirclec = FindViewById<TextView>(Resource.Id.insidecirclec);
            txtinsidecircled = FindViewById<TextView>(Resource.Id.insidecircled);
            txtinsidecirclee = FindViewById<TextView>(Resource.Id.insidecirclee);

            txtoptiona = FindViewById<WebView>(Resource.Id.solutionoptiona);
            txtoptionb = FindViewById<WebView>(Resource.Id.solutionoptionb);
            txtoptionc = FindViewById<WebView>(Resource.Id.solutionoptionc);
            txtoptiond = FindViewById<WebView>(Resource.Id.solutionoptiond);
            txtoptione = FindViewById<WebView>(Resource.Id.solutionoptione);
            txtRightOrWrong = FindViewById<TextView>(Resource.Id.correctwronganswer);


            llpaasage = FindViewById<LinearLayout>(Resource.Id.passagelayout);
            txtpassage = FindViewById<WebView>(Resource.Id.passagetext);
            txtpassage.SetBackgroundColor(Color.Transparent);
            txtquestion = FindViewById<WebView>(Resource.Id.solutionquestion);
            txtoption1 = FindViewById<WebView>(Resource.Id.solutionoptiona);
            ivimage = FindViewById<ImageView>(Resource.Id.solutionquestionimage);

            
            txtoption1.SetBackgroundColor(Color.Transparent);
            txtoption2 = FindViewById<WebView>(Resource.Id.solutionoptionb);
            txtoption3 = FindViewById<WebView>(Resource.Id.solutionoptionc);
            txtoption4 = FindViewById<WebView>(Resource.Id.solutionoptiond);
            txtoption5 = FindViewById<WebView>(Resource.Id.solutionoptione);
            txtoption2.SetBackgroundColor(Color.Transparent);
            txtoption3.SetBackgroundColor(Color.Transparent);
            txtoption4.SetBackgroundColor(Color.Transparent);
            txtoption5.SetBackgroundColor(Color.Transparent);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.solutionSpinner);
            var items = new List<string>() { "All" };
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, items);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            mRecycleView = FindViewById<RecyclerView>(Resource.Id.horizontalrecyclerview);
            mLayoutManager = new LinearLayoutManager(this,LinearLayoutManager.Horizontal, false);
            mRecycleView.SetLayoutManager(mLayoutManager);
            SolutionSliderAdapter mAdapter = new SolutionSliderAdapter(this,Allquestionlist, mRecycleView);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
            // Create your application here
            MAdapter_ItemClick("", 0);
        }
        private void MAdapter_ItemClick(object sender, int e)
        {

            // Toast.MakeText(this, "This is question number " + e, ToastLength.Short).Show();
            selectedposition = e;
            List<questionmodel> questionlist;
            if (filterornot)
            {
                questionlist = AllquestionlistFilter[e];
            }
            else {
                questionlist = Allquestionlist[e];
            }
            stripeposition = e;
            for (int i = 0; i < questionlist.Count; i++)
            {
                questionmodel objmodal = questionlist[i];
                if (questionlist[i].Datatype == 1)
                {




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


                                // txtpassage.LoadData(objppasagemodal.Passage, "text/html", null);
                            }
                            else
                            {
                                continue;
                            }
                        }

                    }

                    StringWriter myWriter = new StringWriter();

                    // Decode the encoded string.
                    HttpUtility.HtmlDecode(objmodal.Qdata, myWriter);
                    string questiondata = objmodal.Qdata.Replace("</p>", "");
                    questiondata = questiondata.Replace("\\n", "");
                    questiondata = questiondata.Replace("\r", "");
                    questiondata = questiondata.Replace("\\/", "/");

                    //DisplaySelectedQuestion(objmodal.selectedoption);



                    if (questiondata.Contains("IMG"))
                    {
                        string[] image1 = questiondata.Split("IMG");
                        string image2 = image1[1].Split("=")[1];
                        string image3 = "";
                        if (image2.Contains("ostimg"))
                        {
                            image3 = image2.Split("ostimg/")[1].Split(".")[0];
                        }
                        else if (image2.Contains("Images"))
                        {
                            image3 = image2.Split("Images/")[1].Split(".")[0];
                        }
                        txtquestion.LoadData(image1[0].Trim(), "text/html", null);
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
                        }
                        catch (Exception)
                        {

                        }


                    }
                    else
                    {
                        ivimage.Visibility = ViewStates.Gone;
                        txtquestion.LoadData(questiondata.Trim(), "text/html", null);
                    }


                    if (questionlist[i].rightorwrongColorCode == Resource.Drawable.greenCircle)
                    {
                        txtRightOrWrong.SetBackgroundResource(Resource.Drawable.greenRectangle);
                        txtRightOrWrong.SetText(Resource.String.correct);
                        txtRightOrWrong.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                        if (questionlist[i].selectedoption == 1)
                        {
                            llanswera.SetBackgroundResource(Resource.Drawable.grrenRectangleWithStroke);
                            llanswerb.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerc.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerd.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswere.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                            txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclea.SetBackgroundResource(Resource.Drawable.greenCircle);
                            txtinsidecircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        }
                        else if (questionlist[i].selectedoption == 2)
                        {
                            llanswera.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerb.SetBackgroundResource(Resource.Drawable.grrenRectangleWithStroke);
                            llanswerc.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerd.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswere.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                            txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircleb.SetBackgroundResource(Resource.Drawable.greenCircle);
                            txtinsidecirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        }

                        else if (questionlist[i].selectedoption == 3)
                        {
                            llanswera.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerb.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerc.SetBackgroundResource(Resource.Drawable.grrenRectangleWithStroke);
                            llanswerd.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswere.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                            txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclec.SetBackgroundResource(Resource.Drawable.greenCircle);
                            txtinsidecircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        }
                        else if (questionlist[i].selectedoption == 4)
                        {
                            llanswera.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerb.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerc.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerd.SetBackgroundResource(Resource.Drawable.grrenRectangleWithStroke);
                            llanswere.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                            txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircled.SetBackgroundResource(Resource.Drawable.greenCircle);
                            txtinsidecirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        }
                        else if (questionlist[i].selectedoption == 5)
                        {
                            llanswera.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerb.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerc.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerd.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswere.SetBackgroundResource(Resource.Drawable.grrenRectangleWithStroke);
                            txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                            txtinsidecirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclee.SetBackgroundResource(Resource.Drawable.greenCircle);
                        }


                    }
                    else if (questionlist[i].rightorwrongColorCode == Resource.Drawable.redcircle)
                    {
                        txtRightOrWrong.SetBackgroundResource(Resource.Drawable.redRectangle);
                        txtRightOrWrong.SetText(Resource.String.wronganswer);
                        txtRightOrWrong.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));

                        if (questionlist[i].selectedoption == 1)
                        {
                            llanswera.SetBackgroundResource(Resource.Drawable.redRactangleWithStroke);
                            llanswerb.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerc.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerd.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswere.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                            txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclea.SetBackgroundResource(Resource.Drawable.redcircle);
                            txtinsidecircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        }
                        else if (questionlist[i].selectedoption == 2)
                        {
                            llanswera.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerb.SetBackgroundResource(Resource.Drawable.redRactangleWithStroke);
                            llanswerc.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerd.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswere.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                            txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircleb.SetBackgroundResource(Resource.Drawable.redcircle);
                            txtinsidecirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        }

                        else if (questionlist[i].selectedoption == 3)
                        {
                            llanswera.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerb.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerc.SetBackgroundResource(Resource.Drawable.redRactangleWithStroke);
                            llanswerd.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswere.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                            txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclec.SetBackgroundResource(Resource.Drawable.redcircle);
                            txtinsidecircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        }
                        else if (questionlist[i].selectedoption == 4)
                        {
                            llanswera.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerb.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerc.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerd.SetBackgroundResource(Resource.Drawable.redRactangleWithStroke);
                            llanswere.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                            txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircled.SetBackgroundResource(Resource.Drawable.redcircle);
                            txtinsidecirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        }
                        else if (questionlist[i].selectedoption == 5)
                        {
                            llanswera.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerb.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerc.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswerd.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                            llanswere.SetBackgroundResource(Resource.Drawable.redRactangleWithStroke);
                            txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                            txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                            txtinsidecirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                            txtinsidecirclee.SetBackgroundResource(Resource.Drawable.redcircle);
                        }

                    }
                    else
                    {
                        txtRightOrWrong.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                        txtRightOrWrong.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                        txtRightOrWrong.SetText(Resource.String.unattempted);
                        llanswera.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                        llanswerb.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                        llanswerc.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                        llanswerd.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                        llanswere.SetBackgroundResource(Resource.Drawable.lightgreybackground);
                        txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                        txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                        txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                        txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                        txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.black)));
                        txtinsidecirclea.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        txtinsidecircleb.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        txtinsidecirclec.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        txtinsidecircled.SetBackgroundResource(Resource.Drawable.whitecolor2);
                        txtinsidecirclee.SetBackgroundResource(Resource.Drawable.whitecolor2);
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
                            string image2 = image1[1].Split("=")[1];
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
                            txtoption1.Visibility = ViewStates.Gone;

                        }
                        else
                        {
                            option1Image.Visibility = ViewStates.Gone;
                            txtoption1.LoadData(objmodal.Qdata, "text/html", null);
                        }


                    }
                    else if (objmodal.Optnseqno == 2)
                    {
                        if (objmodal.Qdata.Contains("IMG"))
                        {
                            string[] image1 = objmodal.Qdata.Split("IMG");
                            string image2 = image1[1].Split("=")[1];
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
                            txtoption2.Visibility = ViewStates.Gone;

                        }
                        else
                        {
                            option2image.Visibility = ViewStates.Gone;
                            txtoption2.LoadData(objmodal.Qdata, "text/html", null);
                        }
                    }
                    else if (objmodal.Optnseqno == 3)
                    {
                        if (objmodal.Qdata.Contains("IMG"))
                        {
                            string[] image1 = objmodal.Qdata.Split("IMG");
                            string image2 = image1[1].Split("=")[1];
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
                            txtoption3.Visibility = ViewStates.Gone;

                        }
                        else
                        {
                            option3image.Visibility = ViewStates.Gone;
                            txtoption3.LoadData(objmodal.Qdata, "text/html", null);
                        }
                    }
                    else if (objmodal.Optnseqno == 4)
                    {
                        if (objmodal.Qdata.Contains("IMG"))
                        {
                            string[] image1 = objmodal.Qdata.Split("IMG");
                            string image2 = image1[1].Split("=")[1];
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
                            txtoption4.Visibility = ViewStates.Gone;

                        }
                        else
                        {
                            option4image.Visibility = ViewStates.Gone;
                            txtoption4.LoadData(objmodal.Qdata, "text/html", null);
                        }
                    }
                    else if (objmodal.Optnseqno == 5)
                    {
                        if (objmodal.Qdata.Contains("IMG"))
                        {
                            string[] image1 = objmodal.Qdata.Split("IMG");
                            string image2 = image1[1].Split("=")[1];
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
                            txtoption5.Visibility = ViewStates.Gone;

                        }
                        else
                        {
                            option5image.Visibility = ViewStates.Gone;
                            txtoption5.LoadData(objmodal.Qdata, "text/html", null);
                        }

                    }





                }
            }



        }
           
        
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                Finish();
                OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_right);
                return true;
            }

            return true;
        }

        public void OnClick(View v)
        {
            int itemid = v.Id;
            if (itemid == Resource.Id.tabAll)
            {
                filterornot = false;
                txtAll.SetBackgroundResource(Resource.Drawable.TabOnlineTextviewSelect);
                txtCorrect.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                txtWrong.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                txtSkipped.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                txtAll.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.white)));
                txtCorrect.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                txtWrong.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                txtSkipped.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                setAllRecord();

            }
            else if (itemid == Resource.Id.tabcorrect)
            {
                filterornot = true;

                AllquestionlistFilter = new List<List<questionmodel>>();
                for (int i = 0; i < Allquestionlist.Count; i++)
                {
                    List<questionmodel> questionlist = Allquestionlist[i];
                    for (int y = 0; y < questionlist.Count; y++)
                    {
                        if (questionlist[y].Datatype == 1)
                        {
                            if (questionlist[y].rightorwrongColorCode == Resource.Drawable.greenCircle)
                            {
                                AllquestionlistFilter.Add(questionlist);
                            }

                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                if (AllquestionlistFilter.Count > 0)
                {
                    txtAll.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                    txtCorrect.SetBackgroundResource(Resource.Drawable.TabOnlineTextviewSelect);
                    txtWrong.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                    txtSkipped.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                    txtAll.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                    txtCorrect.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.white)));
                    txtWrong.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                    txtSkipped.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                    setFilterRecord();
                }
                else
                {
                    Toast.MakeText(this, "Correct Answer Not Found", ToastLength.Short).Show();
                }
            }
            else if (itemid == Resource.Id.tabwrong)
            {
                filterornot = true;

                AllquestionlistFilter = new List<List<questionmodel>>();
                for (int i = 0; i < Allquestionlist.Count; i++)
                {
                    List<questionmodel> questionlist = Allquestionlist[i];
                    for (int y = 0; y < questionlist.Count; y++)
                    {
                        if (questionlist[y].Datatype == 1)
                        {
                            if (questionlist[y].rightorwrongColorCode == Resource.Drawable.redcircle)
                            {
                                AllquestionlistFilter.Add(questionlist);
                            }

                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                if (AllquestionlistFilter.Count > 0)
                {
                    txtAll.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                    txtCorrect.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                    txtWrong.SetBackgroundResource(Resource.Drawable.TabOnlineTextviewSelect);
                    txtSkipped.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                    txtAll.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                    txtCorrect.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                    txtWrong.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.white)));
                    txtSkipped.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                    setFilterRecord();
                }
                else
                {
                    Toast.MakeText(this, "Wrong Answer Not Found", ToastLength.Short).Show();
                }
            }
            else if (itemid == Resource.Id.showcorrectanswer)
            {
               
                    List<questionmodel> questionlist;
                    if (filterornot)
                    {
                        questionlist = AllquestionlistFilter[selectedposition];
                    }
                    else
                    {
                        questionlist = Allquestionlist[selectedposition];
                    }

                    for (int i = 0; i < questionlist.Count; i++)
                    {
                        questionmodel objmodal = questionlist[i];
                        if (objmodal.Datatype == 1)
                        {
                            if (objmodal.Correctans.Equals("1"))
                            {
                                  llanswera.SetBackgroundResource(Resource.Drawable.grrenRectangleWithStroke);
                                  txtinsidecirclea.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                                  txtinsidecirclea.SetBackgroundResource(Resource.Drawable.greenCircle);
                            }
                            else if (objmodal.Correctans.Equals("2"))
                            {
                                llanswerb.SetBackgroundResource(Resource.Drawable.grrenRectangleWithStroke);
                                txtinsidecircleb.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                                txtinsidecircleb.SetBackgroundResource(Resource.Drawable.greenCircle);
                            }
                            else if (objmodal.Correctans.Equals("3"))
                            {
                                llanswerc.SetBackgroundResource(Resource.Drawable.grrenRectangleWithStroke);
                                txtinsidecirclec.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                                txtinsidecirclec.SetBackgroundResource(Resource.Drawable.greenCircle);
                            }
                            else if (objmodal.Correctans.Equals("4"))
                            {
                                llanswerd.SetBackgroundResource(Resource.Drawable.grrenRectangleWithStroke);
                                txtinsidecircled.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                                txtinsidecircled.SetBackgroundResource(Resource.Drawable.greenCircle);
                            }
                            else if (objmodal.Correctans.Equals("5"))
                            {
                                llanswere.SetBackgroundResource(Resource.Drawable.grrenRectangleWithStroke);
                                txtinsidecirclee.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.white)));
                                txtinsidecirclee.SetBackgroundResource(Resource.Drawable.greenCircle);
                            }
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
                filterornot = true;

                AllquestionlistFilter = new List<List<questionmodel>>();
                for (int i = 0; i < Allquestionlist.Count; i++)
                {
                    List<questionmodel> questionlist = Allquestionlist[i];
                    for (int y = 0; y < questionlist.Count; y++)
                    {
                        if (questionlist[y].Datatype == 1)
                        {
                            if (questionlist[y].rightorwrongColorCode == Resource.Drawable.whitecircle1)
                            {
                                AllquestionlistFilter.Add(questionlist);
                            }

                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                if (AllquestionlistFilter.Count > 0)
                {
                    txtAll.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                    txtCorrect.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                    txtWrong.SetBackgroundResource(Resource.Drawable.TabMockTextviewUnselect);
                    txtSkipped.SetBackgroundResource(Resource.Drawable.TabOnlineTextviewSelect);
                    txtAll.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                    txtCorrect.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                    txtWrong.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.black)));
                    txtSkipped.SetTextColor(new Color(ContextCompat.GetColor(this, Resource.Color.white)));
                    setFilterRecord();
                }
                else
                {
                    Toast.MakeText(this, "Skipped Answer Not Found", ToastLength.Short).Show();
                }
            }
        }
        public void setAllRecord()
        {
            mRecycleView = FindViewById<RecyclerView>(Resource.Id.horizontalrecyclerview);
            mLayoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            mRecycleView.SetLayoutManager(mLayoutManager);
            SolutionSliderAdapter mAdapter = new SolutionSliderAdapter(this, Allquestionlist, mRecycleView);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
            // Create your application here
            MAdapter_ItemClick("", 0);
        }
        public void setFilterRecord()
        {
           
            // Create your application here
            try
            {
                if (AllquestionlistFilter.Count > 0)
                {
                    mRecycleView = FindViewById<RecyclerView>(Resource.Id.horizontalrecyclerview);
                    mLayoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
                    mRecycleView.SetLayoutManager(mLayoutManager);
                    SolutionSliderAdapter mAdapter = new SolutionSliderAdapter(this, AllquestionlistFilter, mRecycleView);
                    mAdapter.ItemClick += MAdapter_ItemClick;
                    mRecycleView.SetAdapter(mAdapter);
                    MAdapter_ItemClick("", 0);
                }
                else {
                    Toast.MakeText(this, "Record Not Found", ToastLength.Short).Show();
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}