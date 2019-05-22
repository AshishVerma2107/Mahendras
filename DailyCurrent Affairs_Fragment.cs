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
    public class DailyCurrent_Affairs_Fragment : Fragment
    {
        List<Current_Affairs_Model> affairsList;
        CurrentAffairs_API currentaffairapi;
        List<string> CurrentsAffairs_List = new List<string>();

        // ListView MyListCurrentAffairs;

        RecyclerView MyListCurrentAffairs;

        RecyclerView.LayoutManager mLayoutManager;
        Android.App.ProgressDialog progress;
        DBHelper dba;

        ISharedPreferences pref;

        public static string Curr_Id, Curr_Title, Curr_Date, Curr_File;

        string CurrentAffairDetails;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("AffairsInfo", FileCreationMode.Private);

            CurrentAffairDetails = pref.GetString("FinalAffairs", "false");

            dba = new DBHelper();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View v = inflater.Inflate(Resource.Layout.CurrentAffairsLayout, container, false);
            MyListCurrentAffairs = v.FindViewById<RecyclerView>(Resource.Id.listViewaffairs);

            Button btnrefresh = v.FindViewById<Button>(Resource.Id.c_refresh);

            btnrefresh.Click += delegate
            {

                affairsList = new List<Current_Affairs_Model>();
                getAffairsList();

            };


            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
            // String apiurl = string.Format("http://mg.mahendras.org);
            currentaffairapi = RestService.For<CurrentAffairs_API>("http://mg.mahendras.org");
            // getBookMarkList();
            //=====================================================================//

            if (CurrentAffairDetails.Equals("false"))
            {

                getAffairsList();







            }
            else
            {



                affairsList = dba.Get_CurrentAffairData();

                mLayoutManager = new LinearLayoutManager(this.Activity);
                MyListCurrentAffairs.SetLayoutManager(mLayoutManager);
                CurrentAffairAdapter mAdapter = new CurrentAffairAdapter(Activity, affairsList, MyListCurrentAffairs);
                mAdapter.ItemClick += MAdapter_ItemClick;
                MyListCurrentAffairs.SetAdapter(mAdapter);



            }

            return v;
        }

        private void MAdapter_ItemClick(object sender, int e)
        {
            Current_Affairs_Model objmodel5 = affairsList[e];
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
                //bundle8.PutString("pdfurlaffair", objmodel5.content_file);
                //CurrentAffair_PDFReader objreader7 = new CurrentAffair_PDFReader();
                //objreader7.Arguments = bundle8;
                //Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, objreader7).AddToBackStack(null).Commit();
            }
        }

        private async void getAffairsList()
        {
            progress.Show();

            try
            {
                CurrentAffairsAPI_Response response = await currentaffairapi.GetCurrentAffairsList();

                affairsList = response.res_data;



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                for (int i = 0; i < affairsList.Count; i++)
                {
                    CurrentsAffairs_List.Add(affairsList[i].content_id);

                    CurrentsAffairs_List.Add(affairsList[i].content_title);

                    CurrentsAffairs_List.Add(affairsList[i].content_date);
                    CurrentsAffairs_List.Add(affairsList[i].content_file);

                    Curr_Id = affairsList[i].content_id;
                    Curr_Title = affairsList[i].content_title;
                    Curr_Date = affairsList[i].content_date;
                    Curr_File = affairsList[i].content_file;

                    dba.insertCurrentAffairData(Curr_Id, Curr_Title, Curr_Date, Curr_File);

                }

                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalAffairs", "true");

                edit.Apply();


                CurrentAffairAdapter W_Adapter = new CurrentAffairAdapter(Activity, affairsList, MyListCurrentAffairs);
                mLayoutManager = new LinearLayoutManager(this.Activity);
                MyListCurrentAffairs.SetLayoutManager(mLayoutManager);


                CurrentAffairAdapter mAdapter = new CurrentAffairAdapter(Activity, affairsList, MyListCurrentAffairs);
                mAdapter.ItemClick += MAdapter_ItemClick;
                MyListCurrentAffairs.SetAdapter(mAdapter);
                progress.Dismiss();

            }
            catch (Exception e)
            {
                progress.Dismiss();
            }


        }
    }
}

       