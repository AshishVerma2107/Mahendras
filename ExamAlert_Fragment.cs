using Android.Support.V4.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using ImageSlider.Interface;
using Newtonsoft.Json;
using Refit;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System;
using ImageSlider.API_Interface;
using Android.Content;
using ImageSlider.Model;
using ImageSlider.Adapter;
using Android.Support.V7.Widget;
using System.Threading;

namespace ImageSlider.Fragments
{
    public class ExamAlert_Fragment : Fragment
    {
        List<ExamAlert_Model> alertList;
        ExamAlert_API examalertapi;
        List<string> ExamAlert_List = new List<string>();
        // ListView MyListExamAlert;
        RecyclerView MyListExamAlert;

        RecyclerView.LayoutManager mLayoutManager;

        Android.App.ProgressDialog progress;
        DBHelper dba;

        ISharedPreferences pref;

        public static string alert_Id, alert_Title, alert_Date, alert_File;

        string ExamAlertDetails;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("ExamAlertInfo", FileCreationMode.Private);

            ExamAlertDetails = pref.GetString("FinalAlert", "false");

            dba = new DBHelper();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View v = inflater.Inflate(Resource.Layout.ExamAlertLayout, container, false);
            MyListExamAlert = v.FindViewById<RecyclerView>(Resource.Id.listViewalert);

            Button btnrefresh = v.FindViewById<Button>(Resource.Id.refresh);

            btnrefresh.Click += delegate
            {

                alertList = new List<ExamAlert_Model>();
                getAlertList();

            };




            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
            // String apiurl = string.Format("http://mg.mahendras.org);
            examalertapi = RestService.For<ExamAlert_API>("http://mg.mahendras.org");
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(10000);
            // getBookMarkList();
            //=====================================================================//

            if (ExamAlertDetails.Equals("false"))
            {

                getAlertList();





            }
            else
            {




                alertList = dba.Get_ExamAlertData();

                mLayoutManager = new LinearLayoutManager(this.Activity);
                MyListExamAlert.SetLayoutManager(mLayoutManager);

                ExamAlertAdapter mAdapter = new ExamAlertAdapter(Activity, alertList, MyListExamAlert);
                mAdapter.ItemClick += MAdapter_ItemClick;
                MyListExamAlert.SetAdapter(mAdapter);


               

            }

            return v;
        }

        private void MAdapter_ItemClick(object sender, int e)
        {
            ExamAlert_Model objmodel5 = alertList[e];
            if (objmodel5.content_file == null || objmodel5.content_file == "")

            {

                Toast.MakeText(Activity, " Sorry...PDF is not available", ToastLength.Long).Show();
            }
            else
            {



                var uri = Android.Net.Uri.Parse(objmodel5.content_file);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

                //int counter = e;
                //Bundle bundle8 = new Bundle();
                //bundle8.PutString("pdfurl", objmodel5.content_file);
                //Global_PDF_Reader objreader7 = new Global_PDF_Reader();
                //objreader7.Arguments = bundle8;
                //Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, objreader7).AddToBackStack(null).Commit();
            }
        }



        private async void getAlertList()
        {
            progress.Show();

            try
            {
                ExamAlertAPI_Response response = await examalertapi.GetExamAlertList();

                alertList = response.res_data;

                for (int i = 0; i < alertList.Count; i++)
                {
                    ExamAlert_List.Add(alertList[i].content_id);

                    ExamAlert_List.Add(alertList[i].content_title);

                    ExamAlert_List.Add(alertList[i].content_date);
                    ExamAlert_List.Add(alertList[i].content_file);

                    alert_Id = alertList[i].content_id;
                    alert_Title = alertList[i].content_title;
                    alert_Date = alertList[i].content_date;
                    alert_File = alertList[i].content_file;

                    dba.insertExamAlertData(alert_Id, alert_Title, alert_Date, alert_File);

                }

                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalAlert", "true");

                edit.Apply();


                ExamAlertAdapter W_Adapter = new ExamAlertAdapter(Activity, alertList, MyListExamAlert);
                mLayoutManager = new LinearLayoutManager(this.Activity);
                MyListExamAlert.SetLayoutManager(mLayoutManager);

                ExamAlertAdapter mAdapter = new ExamAlertAdapter(Activity, alertList, MyListExamAlert);
                mAdapter.ItemClick += MAdapter_ItemClick;
                MyListExamAlert.SetAdapter(mAdapter);
                progress.Dismiss();





            }
            catch (Exception e)
            {
                progress.Dismiss();
            }


        }
    }
}


       