using System;
using System.Collections.Generic;
using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Refit;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using ImageSlider.Adapter;
using ImageSlider.API_Interface;
using ImageSlider.Model;
using Android.Support.V7.Widget;

namespace ImageSlider.Fragments
{
    public class NoticeBoardFinal : Fragment
    {
        public static List<NoticeData> noticeList;

        NoticeBoard_API noticeapi;

        List<string> Notice_List = new List<string>();

        ListView MyList_Notice;

        Android.App.ProgressDialog progress;

        DBHelper Notice_dba;
        ISharedPreferences pref;
        string Notice_Data;

        public static string Not_Id, Not_Title, Not_text, Not_date;
        RecyclerView MyListNotice;

        RecyclerView.LayoutManager mLayoutManager;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("NoticeInfo", FileCreationMode.Private);

            Notice_Data = pref.GetString("FinalNotice", "false");

            Notice_dba = new DBHelper();

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.Notice_ListView_Fragment_Layout, container, false);

            MyListNotice = v.FindViewById<RecyclerView>(Resource.Id.listView_Notice);


            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };

            noticeapi = RestService.For<NoticeBoard_API>("http://mg.mahendras.org");
            //  getVideo();
            //=====================================================================//


            if (Notice_Data.Equals("false"))
            {

                getNotice();

            }
            else
            {
                getNotice();
                //getAboutExam();

                //noticeList = Notice_dba.Get_NoticeBoardData();
                //mLayoutManager = new LinearLayoutManager(this.Activity);
                //MyListNotice.SetLayoutManager(mLayoutManager);
                //Notice_ListView_Adapter notice_detail = new Notice_ListView_Adapter(Activity, noticeList, MyListNotice);

                //MyListNotice.SetAdapter(notice_detail);
                //notice_detail.ItemClick += Notice_detail_ItemClick;
            }
            return v;
        }

        private void Notice_detail_ItemClick(object sender, int e)
        {
            Intent i = new Intent(Activity,typeof(NoticeBoardDetail_Activity));
            i.PutExtra("position", e);
            StartActivityForResult(i, 111);
            Activity.OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);
        }

        private async void getNotice()
        {
            progress.Show();

            try
            {

                string datetime = DateTime.Now.ToString("yyyy-MM-dd");
                NoticeBoadrAPI_Response response = await noticeapi.GetNoticeBoardList(datetime);

                noticeList = response.res_data;



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                for (int i = 0; i < noticeList.Count; i++)
                {
                    Notice_List.Add(noticeList[i].content_id);
                    Notice_List.Add(noticeList[i].content_title);
                    Notice_List.Add(noticeList[i].content_text);
                    Notice_List.Add(noticeList[i].content_date);

                    Not_Id = noticeList[i].content_id;
                    Not_Title = noticeList[i].content_title;
                    Not_text = noticeList[i].content_text;
                    Not_date = noticeList[i].content_date;

                    Notice_dba.insertNoticeBoardlData(Not_Id, Not_Title, Not_text, Not_date);

                }

                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalNotice", "true");

                edit.Apply();
                mLayoutManager = new LinearLayoutManager(this.Activity);
                MyListNotice.SetLayoutManager(mLayoutManager);
                Notice_ListView_Adapter videos_detail = new Notice_ListView_Adapter(Activity, noticeList,MyListNotice);

                MyListNotice.SetAdapter(videos_detail);
                videos_detail.ItemClick += Videos_detail_ItemClick;
             

                progress.Dismiss();

                

            }
            catch (Exception e)
            {
                progress.Dismiss();
            }


        }

        private void Videos_detail_ItemClick(object sender, int e)
        {
            Intent i = new Intent(Activity, typeof(NoticeBoardDetail_Activity));
            i.PutExtra("position", e);
            StartActivityForResult(i, 112);
        }
    }
}