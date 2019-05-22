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
    public class BookMarkFragment : Fragment
    {
        List<BookMarkData> bookmarkList;
        BookMark_API bookmarkapi;
        List<string> BookMark_List = new List<string>();
        ListView MyListBookMark;
        Android.App.ProgressDialog progress;
        DBHelper dba;

        ISharedPreferences pref;

        public static string Book_Id, Book_Title, Book_Date, Book_File;

        string BookMarkDetails;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("BookMarkInfo", FileCreationMode.Private);

            BookMarkDetails = pref.GetString("FinalBookMark", "false");

            dba = new DBHelper();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View v = inflater.Inflate(Resource.Layout.BookMark_Layout, container, false);
            MyListBookMark = v.FindViewById<ListView>(Resource.Id.listViewbookmark);


            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
            // String apiurl = string.Format("http://mg.mahendras.org);
            bookmarkapi = RestService.For<BookMark_API>("http://mg.mahendras.org");
            // getBookMarkList();
            //=====================================================================//

            if (BookMarkDetails.Equals("false"))
            {

                getBookMarkList();







            }
            else
            {

                //getAboutExam();

                bookmarkList = dba.Get_BookMarkData();
                for (int i = 0; i < bookmarkList.Count; i++)
                {
                    BookMark_List.Add(bookmarkList[0].content_title);
                }
                MyListBookMark.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleSpinnerItem, BookMark_List);

            }

            return v;
        }
        private async void getBookMarkList()
        {
            progress.Show();

            try
            {
                BookMarkAPI_Response response = await bookmarkapi.GetBookMarkList();

                bookmarkList = response.res_data;



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                for (int i = 0; i < bookmarkList.Count; i++)
                {
                    BookMark_List.Add(bookmarkList[i].content_id);

                    BookMark_List.Add(bookmarkList[i].content_title);

                    BookMark_List.Add(bookmarkList[i].content_date);
                    BookMark_List.Add(bookmarkList[i].content_file);

                    Book_Id = bookmarkList[i].content_id;
                    Book_Title = bookmarkList[i].content_title;
                    Book_Date = bookmarkList[i].content_date;
                    Book_File = bookmarkList[i].content_file;

                    dba.insertBookMarkData(Book_Id, Book_Title, Book_Date, Book_File);

                }

                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalBookMark", "true");

                edit.Apply();

                MyListBookMark.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleSpinnerItem, BookMark_List);

                progress.Dismiss();

            }
            catch (Exception e)
            {
                progress.Dismiss();
            }


        }

    }
}