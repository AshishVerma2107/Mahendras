using Android.Support.V4.App;
using Android.OS;
using Android.Views;
using Android.Support.V4.View;
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
using ImageSlider.API_Interface;

namespace ImageSlider.Fragments
{
    public class StudyMaterial_Fragment : Fragment
    {

        List<StudyMaterialData> SM_List;

        StudyMaterial_API studymaterial_api;

        List<string> StudyMaterial_List = new List<string>();

        ListView MyList_StudyMaterial;
        StudyMaterial_ListView_Adapter Study_detail;


        Android.App.ProgressDialog progress;

        DBHelper StudyMaterial_dba;
        ISharedPreferences pref;
        string StudyMaterial_Data;
        public static string SM_Id, SM__Title, SM_url, SM_ordering;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("StudyMaterialInfo", FileCreationMode.Private);

            StudyMaterial_Data = pref.GetString("FinalStudyMaterial", "false");

            StudyMaterial_dba = new DBHelper();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.StudyMaterial_Fragment_Layout, container, false);

            MyList_StudyMaterial = v.FindViewById<ListView>(Resource.Id.listView_Study);



            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };

            studymaterial_api = RestService.For<StudyMaterial_API>("http://mg.mahendras.org");

            //  getVideo();
            //=====================================================================//

            if (StudyMaterial_Data.Equals("false"))
            {

                getStudyMaterial();







            }
            else
            {

                //getAboutExam();

                SM_List = StudyMaterial_dba.Get_StudyData();

                Study_detail = new StudyMaterial_ListView_Adapter(Activity, SM_List);

                MyList_StudyMaterial.Adapter = Study_detail;


            }


         //   Study_detail.ItemClick += S_Adapter_ItemClick;


            return v;


        }
        private async void getStudyMaterial()
        {
            progress.Show();

            try
            {
                StudyMaterialAPI_Response response = await studymaterial_api.GetStudyMaterialList();

                SM_List = response.res_data;



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                for (int i = 0; i < SM_List.Count; i++)
                {
                    StudyMaterial_List.Add(SM_List[i].content_id);
                    StudyMaterial_List.Add(SM_List[i].content_title);
                    StudyMaterial_List.Add(SM_List[i].click_url);
                    StudyMaterial_List.Add(SM_List[i].ordering);

                    SM_Id = SM_List[i].content_id;
                    SM__Title = SM_List[i].content_title;
                    SM_url = SM_List[i].click_url;
                    SM_ordering = SM_List[i].ordering;

                    StudyMaterial_dba.insertStudyMaterialData(SM_Id, SM__Title, SM_url, SM_ordering);

                }

                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalStudyMaterial", "true");

                edit.Apply();


                StudyMaterial_ListView_Adapter Study_detail = new StudyMaterial_ListView_Adapter(Activity, SM_List);

                MyList_StudyMaterial.Adapter = Study_detail;



                progress.Dismiss();



            }
            catch (Exception e)
            {
                progress.Dismiss();
            }


        }

        //public void S_Adapter_ItemClick(object sender, int e)
        //{

        //    int counter = e;
        //    if (counter == 0)
        //    {
        //        Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new PrivacyFragment()).Commit();





        //    }







        //}
    }
}