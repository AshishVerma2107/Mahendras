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
using ImageSlider.Adapter;
using Android.Content;
using System.Threading;

namespace ImageSlider.Fragments
{
    public class VideoList_Fragment : Fragment
    {
        List<VideoData> videoList;

        Video_API videoapi;

       List<string> Video_List = new List<string>();

        ListView MyList_Videos;

     

        Android.App.ProgressDialog progress;

        DBHelper Video_dba;
        ISharedPreferences pref;
        string Video_Data;

        public static string Vid_Id, Vid_thum, Vid_title, Vid_url;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("VideoInfo", FileCreationMode.Private);

            Video_Data = pref.GetString("FinalVideo", "false");

            Video_dba = new DBHelper();




        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View v = inflater.Inflate(Resource.Layout.VideosList_Fragment_Layout, container, false);
            MyList_Videos = v.FindViewById<ListView>(Resource.Id.listView_Videos);

           

            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
            // String apiurl = string.Format("http://mg.mahendras.org);
            videoapi = RestService.For<Video_API>("http://mg.mahendras.org");
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(10000);
            //  getVideo();
            //=====================================================================//

            if (Video_Data.Equals("false"))
            {

                getVideo();







            }
            else
            {

                //getAboutExam();

                videoList = Video_dba.Get_VideoData();

                Videos_GridView_Adapter videos_detail = new Videos_GridView_Adapter(Activity, videoList);

                MyList_Videos.Adapter = videos_detail;

            }


            return v;
        }
        private async void getVideo()
        {
            progress.Show();

            try
            {
                VideoAPI_Response response = await videoapi.GetVideoList();

                videoList = response.res_data;



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                for (int i = 0; i < videoList.Count; i++)
                {
                    Video_List.Add(videoList[i].videoId);
                    Video_List.Add(videoList[i].title);
                    Video_List.Add(videoList[i].thumb);
                    Video_List.Add(videoList[i].videoURL);

                    Vid_Id = videoList[i].videoId;
                    Vid_title = videoList[i].title;
                    Vid_thum = videoList[i].thumb;
                    Vid_url = videoList[i].videoURL;

                    Video_dba.insertVideoData(Vid_Id, Vid_title, Vid_thum, Vid_url);

                }

                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalVideo", "true");

                edit.Apply();

                Videos_GridView_Adapter videos_detail = new Videos_GridView_Adapter(Activity, videoList);

                MyList_Videos.Adapter = videos_detail;

                // MyList.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, Video_List);

                progress.Dismiss();

                //MyList_Videos.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
                //{
                //    Intent taskIntentActivity5 = new Intent(this.Activity, typeof(YouTube_Activity));

                //    StartActivity(taskIntentActivity5);
                //};

            }
            catch (Exception e)
            {
                progress.Dismiss();
            }

            
        }

       
        


        
        
    }
}