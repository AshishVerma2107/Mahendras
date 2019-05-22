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
using Android.Views.Animations;
using Android.Support.V7.Widget;

namespace ImageSlider.Fragments
{
    public class Vocabulary_Fragment : Fragment
    {
        List<Vocabulary_Model> vocabularyList;
        List<string> WeeklyCurrentsAffairs_List = new List<string>();
        RecyclerView MyListWeeklyVocabulary;
        RecyclerView.LayoutManager mLayoutManager;
        Vacabulary_API vocabularyapi;
        List<string> Vocabulary_List = new List<string>();
       
        Android.App.ProgressDialog progress;
        DBHelper dba;

        ISharedPreferences pref;

        public static string Voc_Id, Voc_Title, Voc_Date, Voc_File;

        string VocabularyDetails;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("VocabularyInfo", FileCreationMode.Private);

            VocabularyDetails = pref.GetString("FinalVocabulary", "false");

            dba = new DBHelper();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View v = inflater.Inflate(Resource.Layout.Vocabulary_ListView_Layout, container, false);
            MyListWeeklyVocabulary = v.FindViewById<RecyclerView>(Resource.Id.vocab);

            Button btnrefresh = v.FindViewById<Button>(Resource.Id.refresh);

            btnrefresh.Click += delegate
            {

                vocabularyList = new List<Vocabulary_Model>();
                getVocabularyList();

            };



            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
            // String apiurl = string.Format("http://mg.mahendras.org);
            vocabularyapi = RestService.For<Vacabulary_API>("http://mg.mahendras.org");
            // getBookMarkList();
            //=====================================================================//

            if (VocabularyDetails.Equals("false"))
            {

                getVocabularyList();

            }
            else
            {

                //getAboutExam();

                vocabularyList = dba.Get_VocabularyData();
               
                mLayoutManager = new LinearLayoutManager(this.Activity);
                MyListWeeklyVocabulary.SetLayoutManager(mLayoutManager);
                VocabularyAdapter mAdapter = new VocabularyAdapter(Activity, vocabularyList, MyListWeeklyVocabulary);
                mAdapter.ItemClick += MAdapter_ItemClick;
                MyListWeeklyVocabulary.SetAdapter(mAdapter);

            }

            return v;
        }
        private async void getVocabularyList()
        {
            progress.Show();

            try
            {
                VocabularyAPI_Response response = await vocabularyapi.GetVocabularyList();

                vocabularyList = response.res_data;



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                for (int i = 0; i < vocabularyList.Count; i++)
                {
                    Vocabulary_List.Add(vocabularyList[i].content_id);

                    Vocabulary_List.Add(vocabularyList[i].content_title);

                    Vocabulary_List.Add(vocabularyList[i].content_date);
                    Vocabulary_List.Add(vocabularyList[i].content_file);

                    Voc_Id = vocabularyList[i].content_id;
                    Voc_Title = vocabularyList[i].content_title;
                    Voc_Date = vocabularyList[i].content_date;
                    Voc_File = vocabularyList[i].content_file;

                    dba.insertVocabularyData(Voc_Id, Voc_Title, Voc_Date, Voc_File);

                }

                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalVocabulary", "true");

                edit.Apply();
                mLayoutManager = new LinearLayoutManager(this.Activity);
                MyListWeeklyVocabulary.SetLayoutManager(mLayoutManager);
                VocabularyAdapter mAdapter = new VocabularyAdapter(Activity, vocabularyList, MyListWeeklyVocabulary);
                mAdapter.ItemClick += MAdapter_ItemClick;
                MyListWeeklyVocabulary.SetAdapter(mAdapter);
                progress.Dismiss();

            }
            catch (Exception e)
            {
                progress.Dismiss();
            }


        }
        private void MAdapter_ItemClick(object sender, int e)
        {
            Vocabulary_Model objmodelvoc = vocabularyList[e];

            if (objmodelvoc.content_file == null || objmodelvoc.content_file == "")


            {

                Toast.MakeText(Activity, " Sorry...PDF is not available", ToastLength.Long).Show();



            }

            else
            {



                var uri = Android.Net.Uri.Parse(objmodelvoc.content_file);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
                //int counter = e;

                //Bundle bundle_voc = new Bundle();
                //bundle_voc.PutString("pdfurlvocabulary", objmodelvoc.content_file);


                //Vocabulary_PDFReader objreader_voc = new Vocabulary_PDFReader();
                //objreader_voc.Arguments = bundle_voc;

                //Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, objreader_voc).AddToBackStack(null).Commit();
            }

        }
       
    }
}