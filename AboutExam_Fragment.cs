using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ImageSlider.Interface;
using ImageSlider.Model;
using Newtonsoft.Json;
using Refit;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Threading;
using ImageSlider.Adapter;
using Android.Content.PM;

namespace ImageSlider.Fragments
{
    public class AboutExam_Fragment : Fragment
    {

        Dictionary<string, AboutExamData> aboutexamDictinary;
        AboutExamAPI aboutapi;
        List<AboutExamData> myFinalList;
        List<string> aboutExamCoursename = new List<string>();

        ListView MyList;

        GridView aboutgrid;
        Android.App.ProgressDialog progress;

        string Abour_Exam;
        DBHelper dba;

        ISharedPreferences pref;

       public static string A_Exam_ID, A_Exam_Name;

        public override void OnCreate(Bundle savedInstanceState)
        {

            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            StrictMode.ThreadPolicy.Builder builder1 = new StrictMode.ThreadPolicy.Builder().PermitAll();
            StrictMode.SetThreadPolicy(builder1.Build());


            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please Wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("AboutExamInfo", FileCreationMode.Private);

            Abour_Exam = pref.GetString("FinalAboutExam", "false");

            dba = new DBHelper();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            //  View v = inflater.Inflate(Resource.Layout.AboutExam_Layout, container, false);
            //  MyList = v.FindViewById<ListView>(Resource.Id.listView);


            View v = inflater.Inflate(Resource.Layout.AboutExam_GridLayout, container, false);

       

             aboutgrid = v.FindViewById<GridView>(Resource.Id.gridview_aboutexam);

            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
           // String apiurl = string.Format("http://mg.mahendras.org);
            aboutapi = RestService.For<AboutExamAPI>("http://mg.mahendras.org");
             
            //=====================================================================//


            if (Abour_Exam.Equals("false"))
            {

                getAboutExam();

               

               

              

            }
            else
            {

                //getAboutExam();

                myFinalList = dba.Get_AboutExamData();

                AboutExamGridAdapter videoLecture = new AboutExamGridAdapter(Activity, myFinalList);

                aboutgrid.Adapter = videoLecture;

            }

            return v;
        }
        private async void getAboutExam()
        {
            progress.Show();

            try
            {
                
                ApiResponse response = await aboutapi.GetAboutExamList();

                aboutexamDictinary = response.items;

                Dictionary<string, AboutExamData>.KeyCollection keys = aboutexamDictinary.Keys;
               

                myFinalList = new List<AboutExamData>();
                foreach (string key in keys)
                {
                   

                    myFinalList.Add(aboutexamDictinary.GetValueOrDefault(key));

                    aboutExamCoursename.Add(aboutexamDictinary.GetValueOrDefault(key).id);
                    aboutExamCoursename.Add(aboutexamDictinary.GetValueOrDefault(key).name);

                    A_Exam_ID = aboutexamDictinary.GetValueOrDefault(key).id;

                    A_Exam_Name = aboutexamDictinary.GetValueOrDefault(key).name;

                    dba.insertAboutExamData(A_Exam_ID, A_Exam_Name);

                }
                   


                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalAboutExam", "true");

                edit.Apply();
                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();


                //  MyList.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, aboutExamCoursename);

                AboutExamGridAdapter videoLecture = new AboutExamGridAdapter(Activity, myFinalList);

                aboutgrid.Adapter = videoLecture;

                progress.Dismiss();

            }
            catch (Exception e)
            {

            }


        }

    }
}