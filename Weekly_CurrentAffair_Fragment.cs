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

namespace ImageSlider.Fragments
{
    public class Weekly_CurrentAffair_Fragment : Fragment
    {
        List<Weekly_Current_Affair_Model> weeklyList;
        WeeklyCurrentAffair_API weeklycurrentaffairapi;
        List<string> WeeklyCurrentsAffairs_List = new List<string>();
        RecyclerView MyListWeeklyCurrentAffairs;
        
        RecyclerView.LayoutManager mLayoutManager;
        Android.App.ProgressDialog progress;
        DBHelper dba;

        ISharedPreferences pref;

        public static string Week_Id, Week_Title, Week_Date, Week_File;

        string WeeklyCurrentAffairDetails;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("WeeklyAffairsInfo", FileCreationMode.Private);

            WeeklyCurrentAffairDetails = pref.GetString("FinalWeeklyAffairs", "false");

            dba = new DBHelper();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View v = inflater.Inflate(Resource.Layout.WeeklyCurrentAffair_Layout, container, false);
            MyListWeeklyCurrentAffairs = v.FindViewById<RecyclerView>(Resource.Id.weeklyaffairs);

            Button btnrefresh = v.FindViewById<Button>(Resource.Id.refresh);

            btnrefresh.Click += delegate
            {

                weeklyList = new List<Weekly_Current_Affair_Model>();
                gettWeeklyAffairsList();

            };

            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
            // String apiurl = string.Format("http://mg.mahendras.org);
            weeklycurrentaffairapi = RestService.For<WeeklyCurrentAffair_API>("http://mg.mahendras.org");
            // getBookMarkList();
            //=====================================================================//

            if (WeeklyCurrentAffairDetails.Equals("false"))
            {

                gettWeeklyAffairsList();

            }
            else
            {
                weeklyList = dba.Get_WeeklyCurrentAffairData();
                //WeeklyCurrentAffairAdapter W_Adapter = new WeeklyCurrentAffairAdapter(Activity, weeklyList, MyListWeeklyCurrentAffairs);

               
                mLayoutManager = new LinearLayoutManager(this.Activity);
                MyListWeeklyCurrentAffairs.SetLayoutManager(mLayoutManager);
                WeeklyCurrentAffairAdapter mAdapter = new WeeklyCurrentAffairAdapter(Activity,weeklyList, MyListWeeklyCurrentAffairs);
                mAdapter.ItemClick += MAdapter_ItemClick;
                MyListWeeklyCurrentAffairs.SetAdapter(mAdapter);
               
              
            }

            return v;
        }
        private void MAdapter_ItemClick(object sender, int e)
        {
            Weekly_Current_Affair_Model objmodel5 = weeklyList[e];
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
                //bundle8.PutString("pdfurlweeklyaffair", objmodel5.content_file);
                //WeeklyCurrentAffair_PDFReader objreader7 = new WeeklyCurrentAffair_PDFReader();
                //objreader7.Arguments = bundle8;
                //Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, objreader7).AddToBackStack(null).Commit();
            }
        }



            private async void gettWeeklyAffairsList()
        {
            progress.Show();

            try
            {
                WeeklyCurrentAffairsAPI_Response response = await weeklycurrentaffairapi.GetWeeklyCurrentAffairsList();

                weeklyList = response.res_data;



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                for (int i = 0; i < weeklyList.Count; i++)
                {
                    WeeklyCurrentsAffairs_List.Add(weeklyList[i].content_id);

                    WeeklyCurrentsAffairs_List.Add(weeklyList[i].content_title);

                    WeeklyCurrentsAffairs_List.Add(weeklyList[i].content_date);
                    WeeklyCurrentsAffairs_List.Add(weeklyList[i].content_file);

                    Week_Id = weeklyList[i].content_id;
                    Week_Title = weeklyList[i].content_title;
                    Week_Date = weeklyList[i].content_date;
                    Week_File = weeklyList[i].content_file;

                    dba.insertWeeklyCurrentAffairData(Week_Id, Week_Title, Week_Date, Week_File);

                }

                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalWeeklyAffairs", "true");

                edit.Apply();


                WeeklyCurrentAffairAdapter W_Adapter = new WeeklyCurrentAffairAdapter(Activity, weeklyList, MyListWeeklyCurrentAffairs);
                mLayoutManager = new LinearLayoutManager(this.Activity);
                MyListWeeklyCurrentAffairs.SetLayoutManager(mLayoutManager);


                WeeklyCurrentAffairAdapter mAdapter = new WeeklyCurrentAffairAdapter(Activity, weeklyList, MyListWeeklyCurrentAffairs);
                mAdapter.ItemClick += MAdapter_ItemClick;
                MyListWeeklyCurrentAffairs.SetAdapter(mAdapter);
                progress.Dismiss();

            }
            catch (Exception e)
            {
                progress.Dismiss();
            }


        }

       
    }
}