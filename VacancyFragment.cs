using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using ImageSlider.Adapter;
using ImageSlider.API_Interface;
using Refit;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using Android.Support.V7.App;

namespace ImageSlider.Fragments
{
    [Activity(Label = "Mahendras", Theme = "@style/AppTheme")]
    public class VacancyFragment : AppCompatActivity
    {
        Android.App.ProgressDialog progress;
       //public static List<Vacancy_API> vacancyList;
        Vacancy_API vacancy_ApI;
        public static List<Vacancy_Model> myvacancyList = new List<Vacancy_Model>();
        RecyclerView MyVacancyRecyclerview;

        RecyclerView.LayoutManager mLayoutManager;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VacancyFragment);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);
            // View v = inflater.Inflate(Resource.Layout.VacancyFragment, container, false);
            MyVacancyRecyclerview = FindViewById<RecyclerView>(Resource.Id.vacancylist);
            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait..");
            progress.Show();

            try
            {
                vacancy_ApI = RestService.For<Vacancy_API>("http://mg.mahendras.org");
                getVacancyList();

            }
            catch (Exception)
            {

            }

          

        }
        private async void getVacancyList()
        {
            ResDataVacancy response = await vacancy_ApI.GetVacancyList();
            myvacancyList = response.res_datavacancy;
            mLayoutManager = new LinearLayoutManager(this);
            MyVacancyRecyclerview.SetLayoutManager(mLayoutManager);
            VacancyFragmentAdapter mAdapter = new VacancyFragmentAdapter(this, myvacancyList, MyVacancyRecyclerview);
            mAdapter.ItemClick += MAdapter_ItemClick;
            MyVacancyRecyclerview.SetAdapter(mAdapter);
            progress.Dismiss();

        }
        private void MAdapter_ItemClick(object sender, int e)
        {
            try
            {
                var uri = Android.Net.Uri.Parse(myvacancyList[e].content_text);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

                //Intent i = new Intent(this, typeof(Vacancy_Fragment));
                //i.PutExtra("position", e);
                ////i.PutExtra("text", myvacancyList[e].content_text);
                //StartActivityForResult(i, 112);
                //OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
          

        }

    }
}