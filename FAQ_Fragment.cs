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

namespace ImageSlider.Fragments
{
    public class FAQ_Fragment : Fragment
    {
        List<FAQ_Data> faq_List;

        FAQ_API faq_api;

        List<string> FAQ_List = new List<string>();

        ListView MyList_FAQ;

        Android.App.ProgressDialog progress;

        DBHelper FAQ_dba;
        ISharedPreferences pref;
        string FAQ_Data;

        public static string FAQ_Id, FAQ_Title, FAQ_text, FAQ_date;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("FAQInfo", FileCreationMode.Private);

            FAQ_Data = pref.GetString("FinalFAQ", "false");

            FAQ_dba = new DBHelper();

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.FAQ_ListView_Fragment_Layout, container, false);

            MyList_FAQ = v.FindViewById<ListView>(Resource.Id.listView_FAQ);


            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };

            faq_api = RestService.For<FAQ_API>("http://mg.mahendras.org");
            //  getVideo();
            //=====================================================================//


            if (FAQ_Data.Equals("false"))
            {

                getFAQ();







            }
            else
            {

                //getAboutExam();

                faq_List = FAQ_dba.Get_FAQ_Data();

                FAQ_ListView_Adapter faq_detail = new FAQ_ListView_Adapter(Activity, faq_List);

                MyList_FAQ.Adapter = faq_detail;

                // Notice_ListView_Adapter notice_detail = new Notice_ListView_Adapter(Activity, faq_List);

                // MyList_FAQ.Adapter = notice_detail;

            }
            return v;
        }

        private async void getFAQ()
        {
            progress.Show();

            try
            {
                FAQ_ResData response = await faq_api.GetFAQList();

                faq_List = response.res_data;



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                for (int i = 0; i < faq_List.Count; i++)
                {
                    FAQ_List.Add(faq_List[i].content_id);
                    FAQ_List.Add(faq_List[i].content_title);
                    FAQ_List.Add(faq_List[i].content_text);
                    FAQ_List.Add(faq_List[i].content_date);

                    FAQ_Id = faq_List[i].content_id;
                    FAQ_Title = faq_List[i].content_title;
                    FAQ_text = faq_List[i].content_text;
                    FAQ_date = faq_List[i].content_date;

                    FAQ_dba.insertFAQData(FAQ_Id, FAQ_Title, FAQ_text, FAQ_date);

                }

                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalFAQ", "true");

                edit.Apply();

                FAQ_ListView_Adapter faq_detail = new FAQ_ListView_Adapter(Activity, faq_List);

                MyList_FAQ.Adapter = faq_detail;

               

                progress.Dismiss();

               

            }
            catch (Exception e)
            {
                progress.Dismiss();
            }


        }







    }
}