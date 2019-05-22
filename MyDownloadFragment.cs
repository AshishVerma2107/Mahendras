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

namespace ImageSlider.Fragments
{
    public class MyDownloadFragment : Fragment
    {
        List<DownloadData> downloadList;

        Download_API downloadapi;

        

        List<string> Download_List = new List<string>();

        ListView MyListDownload;

        Android.App.ProgressDialog progress;

        DBHelper Download_dba;
        ISharedPreferences pref;
        string DownLoad_Data;
        public static string Down_Id, Download_Title, Download_Date, Download_File;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("DownLoadInfo", FileCreationMode.Private);

            DownLoad_Data = pref.GetString("FinalDownload", "false");

            Download_dba = new DBHelper();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View v = inflater.Inflate(Resource.Layout.MyDownload_Layout, container, false);
            MyListDownload = v.FindViewById<ListView>(Resource.Id.listViewdownload);


            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
            // String apiurl = string.Format("http://mg.mahendras.org);
            downloadapi = RestService.For<Download_API>("http://mg.mahendras.org");
            // getDownLoadList();
            //=====================================================================//


            if (DownLoad_Data.Equals("false"))
            {

                getDownLoadList();







            }
            else
            {

                //getAboutExam();

                downloadList = Download_dba.Get_DownloadData();

                for (int i = 0; i < downloadList.Count; i++)
                {
                    Download_List.Add(downloadList[i].content_title);
                }

                MyListDownload.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleSpinnerItem, Download_List);

                progress.Dismiss();

            }
            return v;
        }
        private async void getDownLoadList()
        {
            progress.Show();

            try
            {
                DownloadAPI_Response response = await downloadapi.GetDownloadList();

                downloadList = response.res_data;



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                for (int i = 0; i < downloadList.Count; i++)
                {
                    Download_List.Add(downloadList[i].content_file);



                    Download_File = downloadList[i].content_file;

                    Download_dba. insertDownLoadData("", Download_File, "", "");
                }

                ISharedPreferencesEditor edit = pref.Edit();

                edit.PutString("FinalDownload", "true");

                edit.Apply();

                MyListDownload.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleSpinnerItem, Download_List);

                progress.Dismiss();

            }
            catch (Exception e)
            {
                progress.Dismiss();
            }


        }

    }
}