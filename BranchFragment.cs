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
using Android.Content;

namespace ImageSlider.Fragments
{
    public class BranchFragment : Fragment
    {
        List<BranchData> branchList;

        Branch_API branchapi;

        //List<VideoData> myFinalList;

        List<string> Branch_List = new List<string>();

        ListView MyListBranch;

      //  string mybranch;

        Android.App.ProgressDialog progress;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");

          //  ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyBranchInfo", FileCreationMode.Private);

          //  mybranch = pref.GetString("MyBranch", String.Empty);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View v = inflater.Inflate(Resource.Layout.Branch_ListView, container, false);
            MyListBranch = v.FindViewById<ListView>(Resource.Id.listViewbranch);


            //==================================Fetch api==========================//
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };

            branchapi = RestService.For<Branch_API>("http://mg.mahendras.org");
           getBranch();
            //=====================================================================//

            //if (mybranch.Length <= 0)
            //{
            //    branchapi = RestService.For<Branch_API>("http://mg.mahendras.org");
            //    getBranch();
            //}

            //else
            //{
                

            //   MyListBranch.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleDropDownItem1Line, Branch_List);
            //}

            return v;
        }
        private async void getBranch()
        {
            progress.Show();

            try
            {
                BranchAPI_Response response = await branchapi.GetBranchList();

                branchList = response.res_data;



                //  Toast.MakeText(this.Activity, "-->" + myFinalList[0].name,ToastLength.Short).Show();

                for (int i = 0; i < branchList.Count; i++)
                {
                    Branch_List.Add(branchList[i].state_name);

                }

                MyListBranch.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleDropDownItem1Line, Branch_List);

                progress.Dismiss();

                //if (mybranch.Length <= 0)
                //{
                //    ISharedPreferences pref = Android.App.Application.Context.GetSharedPreferences("MyBranchInfo", FileCreationMode.Private);
                //    ISharedPreferencesEditor edit = pref.Edit();
                //    edit.PutString("MyBranch", Branch_List.ToString());

                //    edit.Apply();
                //}
                //Intent intent = new Intent(Activity, typeof(BranchFragment));
                //Activity.StartActivity(intent);

            

            }
            catch (Exception e)
            {
                progress.Dismiss();
            }


        }

    }
}