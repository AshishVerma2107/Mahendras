using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using System.Collections.Generic;
using Android.Support.V4.App;
using Android;

using Android.Support.V7.Widget;
using Android.Support.Design.Widget;

using Android.Support.V4.View;
using Android.Content.Res;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Content;
using Android.Content.PM;

namespace ImageSlider
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme")]
    public class MainActivity1 : AppCompatActivity
    {
        ISharedPreferences pref;
        ISharedPreferencesEditor edit;
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        IMenuItem previousItem;
        Android.Support.V7.App.ActionBarDrawerToggle toggle;
        Toolbar toolbar;
        public static int userid;
        string username;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main1);

            pref = GetSharedPreferences("login", FileCreationMode.Private);
            edit = pref.Edit();
            username = pref.GetString("username", String.Empty);
            userid = pref.GetInt("userid", 0);
           
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new Dashboard()).Commit();
          
        }

        

        //Handling Back Key Press  
        public override void OnBackPressed()
        {
            base.OnBackPressed();
          
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 402)
            {
                pref = GetSharedPreferences("login", FileCreationMode.Private);
                edit = pref.Edit();
                username = pref.GetString("username", String.Empty);
                userid = pref.GetInt("userid", 0);
            }

        }
          protected override void OnDestroy()
        {
            base.OnDestroy();
        }

    }
}

