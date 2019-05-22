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
using ImageSlider.Model;
using ImageSlider.Adapter;
using Android.Content;

namespace ImageSlider.Fragments
{
    public class Gallery_Fragment : Fragment
    {
        
            List<GalleryData> galleryList;

            Gallery_API galleryapi;

            List<string> Gallery_List = new List<string>();

           ListView List_View;

        Android.App.ProgressDialog progress;

        DBHelper Gallerydba;
        ISharedPreferences pref;
        string Gallery_Data;

        public static string Gallr_Id, Gallr_Name, Gallr_url;

        public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                progress = new Android.App.ProgressDialog(Activity);
                progress.Indeterminate = true;
                progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                progress.SetCancelable(false);
                progress.SetMessage("Please wait...");

            pref = Android.App.Application.Context.GetSharedPreferences("GalleryInfo", FileCreationMode.Private);

            Gallery_Data = pref.GetString("FinalGallery", "false");

            Gallerydba = new DBHelper();
        }

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {

                View v = inflater.Inflate(Resource.Layout.Gallery_Grid_Layout, container, false);
            //  MyListGallery = v.FindViewById<ListView>(Resource.Id.listViewgallery);

            List_View = v.FindViewById<ListView>(Resource.Id.gridview_gallery);


            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = { new StringEnumConverter() }
                };
                // String apiurl = string.Format("http://mg.mahendras.org);
                galleryapi = RestService.For<Gallery_API>("http://mg.mahendras.org");
            // getGallery();
            //=====================================================================//

            if (Gallery_Data.Equals("false"))
            {

                getGallery();







            }
            else
            {

                //getAboutExam();

                galleryList = Gallerydba.Get_GalleryData();

                Gallery_Grid_Adapter gallerydetail = new Gallery_Grid_Adapter(Activity, galleryList);

                List_View.Adapter = gallerydetail;

            }

            return v;
            }
            private async void getGallery()
            {
                progress.Show();

                try
                {
                    GalleryAPI_Response response = await galleryapi.GetGalleryList();

                    galleryList = response.res_data;



                    //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                    for (int i = 0; i < galleryList.Count; i++)
                    {
                    Gallery_List.Add(galleryList[i].gallery_id);
                    Gallery_List.Add(galleryList[i].gallery_name);
                    Gallery_List.Add(galleryList[i].gallery_url);

                    Gallr_Id = galleryList[i].gallery_id;
                    Gallr_Name = galleryList[i].gallery_name;
                    Gallr_url = galleryList[i].gallery_url;



                    Gallerydba.insertGalleryData( "",Gallr_Name, Gallr_url);

                }

                ISharedPreferencesEditor edit = pref.Edit();
                edit.PutString("FinalGallery", "true");

                edit.Apply();

                // MyListGallery.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, Gallery_List);

                Gallery_Grid_Adapter gallerydetail = new Gallery_Grid_Adapter(Activity, galleryList);

                List_View.Adapter = gallerydetail;

                progress.Dismiss();

               

                }
                catch (Exception e)
                {
                    progress.Dismiss();
                }


            }

        }
    }
