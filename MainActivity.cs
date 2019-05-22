using System;
using System.Net;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.InputMethodServices;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.OneSignal;
using ImageSlider.Fragments;

namespace ImageSlider
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, View.IOnClickListener
    {
        ISharedPreferences pref;
        ISharedPreferencesEditor edit;
        TextView txtdisplay;
        Refractored.Controls.CircleImageView profileimage;
        Android.Support.V7.App.ActionBarDrawerToggle toggle;
        public int count = 0;
        string username, profilepath;
        public static int userid;

        private Boolean Log = true;

        static IMenuItem mymenuitem;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            OneSignal.Current.StartInit("aaf605c7-a2fc-401e-b9db-ea54f324f69c").EndInit();
            pref = GetSharedPreferences("login", FileCreationMode.Private);
            edit = pref.Edit();
            username = pref.GetString("username", String.Empty);
            profilepath = pref.GetString("profilePath", String.Empty);
            userid = pref.GetInt("userid", 0);
            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            StrictMode.ThreadPolicy.Builder builder1 = new StrictMode.ThreadPolicy.Builder().PermitAll();
            StrictMode.SetThreadPolicy(builder1.Build());

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            // toolbar.SetNavigationIcon(Resource.Drawable.login_image);

            //  SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            //// SetSupportActionBar(toolbar);


            ////  toolbar.SetNavigationIcon((Resource.Drawable.notification_icon_background));



            if (toolbar != null)


            {
                //toolbar.SetNavigationIcon((Resource.Drawable.home1));
                SetSupportActionBar(toolbar);
                //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                //SupportActionBar.SetHomeButtonEnabled(true);
            }



            //if (SetSupportActionBar(toolbar) != null)
            //{
            //    SetSupportActionBar(toolbar).SetDisplayShowHomeEnabled(true);
            //    SetSupportActionBar(toolbar)SsetDisplayHomeAsUpEnabled(true);
            //}



            // FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //  fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);




            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            LayoutInflater mInflater = (LayoutInflater)GetSystemService(Context.LayoutInflaterService);
            View header = navigationView.GetHeaderView(0);
            txtdisplay = header.FindViewById<TextView>(Resource.Id.usernametext);

            profileimage = header.FindViewById<Refractored.Controls.CircleImageView>(Resource.Id.profile);
            if (username.Length > 0)
            {
                txtdisplay.Text = username;

                var imageBitmap = GetImageBitmapFromUrl(profilepath);

                profileimage.SetImageBitmap(imageBitmap);




            }

            toggle.SyncState();
            navigationView.SetNavigationItemSelectedListener(this);
            var bottomBar = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);

            RemoveShiftMode(bottomBar);

            SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, new Home(), "Home")
               .Commit();



            bottomBar.NavigationItemSelected += (s, a) =>
            {
                LoadFragment(a.Item.ItemId);
            };
        }

        void RemoveShiftMode(BottomNavigationView view)
        {
            var menuView = (BottomNavigationMenuView)view.GetChildAt(0);
            try
            {
                var shiftingMode = menuView.Class.GetDeclaredField("mShiftingMode");
                shiftingMode.Accessible = true;
                shiftingMode.SetBoolean(menuView, false);
                shiftingMode.Accessible = false;

                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    var item = (BottomNavigationItemView)menuView.GetChildAt(i);
                    item.SetShiftingMode(false);
                    // set checked value, so view will be updated
                    item.SetChecked(item.ItemData.IsChecked);
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine((ex.InnerException ?? ex).Message);
            }
        }
        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }
        void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.menuItem1:
                    fragment = new Home();
                    break;
                case Resource.Id.menuItem2:
                    // fragment = new Career_Fragment();

                    var uri = Android.Net.Uri.Parse("https://career.mahendras.org");
                    var intent = new Intent(Intent.ActionView, uri);
                    StartActivity(intent);
                    break;
                case Resource.Id.menuItem3:

                    ISharedPreferences pref;
                    ISharedPreferencesEditor edit;
                    pref = GetSharedPreferences("login", FileCreationMode.Private);
                    edit = pref.Edit();
                    string username = pref.GetString("username", String.Empty);
                    if (username.Length > 0)
                    {
                        var taskIntentActivity2 = new Intent(this, typeof(MainActivity1));
                        StartActivityForResult(taskIntentActivity2, 103);
                    }
                    else
                    {
                        Toast.MakeText(this, "Please Login First", ToastLength.Short).Show();
                    }

                    //  fragment = new ST_Portal_Fragment();
                    break;
                case Resource.Id.menuItem4:
                    // fragment = new MyShop();
                    var uri1 = Android.Net.Uri.Parse("https://myshop.mahendras.org");
                    var intent1 = new Intent(Intent.ActionView, uri1);
                    StartActivity(intent1);
                    break;
                case Resource.Id.menuItem5:
                    fragment = new JoinTelegramFragment();
                    break;

            }
            if (fragment == null)
                return;

            SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, fragment)
               .Commit();
        }

        //  private bool flag = true;
        public override void OnBackPressed()
        {
            //  int counter = 0;
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
             
            }

            else
            {
                int count = SupportFragmentManager.BackStackEntryCount;

                if (count == 0)
                {
                    // 
                    drawer.CloseDrawer(GravityCompat.Start);

                    Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                    alert.SetTitle("Logout");
                    alert.SetMessage("Do you want to Exit ?");
                    alert.SetPositiveButton(("Yes"), (sender, args) =>
                    {

                        base.OnBackPressed();
                       // Toast.MakeText(this, "You have successfully Exit", ToastLength.Long).Show();


                    });

                    alert.SetNegativeButton(("No"), (sender, args) =>
                    {

                    });

                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
                else
                {
                    SupportFragmentManager.PopBackStack();
                }

            }



        }


        public Task<bool> AlertAsync(Context context, string title, string message, string positiveButton, string negativeButton)
        {
            var tcs = new TaskCompletionSource<bool>();

            using (var db = new Android.App.AlertDialog.Builder(context))
            {
                db.SetTitle(title);
                db.SetMessage(message);
                db.SetPositiveButton(positiveButton, (sender, args) => { tcs.TrySetResult(true); });
                db.SetNegativeButton(negativeButton, (sender, args) => { tcs.TrySetResult(false); });
                db.Show();
            }

            return tcs.Task;
        }
        private void SetMenuItemEnabled(bool enabled)
        {
            mymenuitem.SetEnabled(enabled);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
             MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            mymenuitem = menu.FindItem(Resource.Id.nav_login);
           

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {


            // mymenuitem = item;

            if (item.ItemId != Android.Resource.Id.Home)

                Finish();

            return base.OnOptionsItemSelected(item);

        }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
           


            Android.Support.V4.App.Fragment fragment1 = null;

            if (id == Resource.Id.nav_home)
            {
                fragment1 = new Home();
                SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, fragment1)
               .Commit();


                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);


            }


            else if (id == Resource.Id.nav_shop)
            {
                var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
                // fragment1 = new MyShop();
                // SupportFragmentManager.BeginTransaction()
                //.Replace(Resource.Id.content_frame, fragment1)
                //.Commit();


                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);

            }

            else if (id == Resource.Id.nav_portal)
            {


                ISharedPreferences pref;
                ISharedPreferencesEditor edit;
                pref = GetSharedPreferences("login", FileCreationMode.Private);
                edit = pref.Edit();
                string username = pref.GetString("username", String.Empty);
                if (username.Length > 0)
                {
                    var taskIntentActivity2 = new Intent(this, typeof(MainActivity1));
                    StartActivityForResult(taskIntentActivity2, 102);
                }
                else
                {
                    Toast.MakeText(this, "Please Login First", ToastLength.Short).Show();
                }



                //  fragment1 = new ST_Portal_Fragment();
            }
            else if (id == Resource.Id.nav_about)
            {
                fragment1 = new AboutUsFragment();
                SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, fragment1)
               .Commit();


                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);


            }
            //else if (id == Resource.Id.nav_branches)
            //{
            //    fragment1 = new BranchFragment();
            //}
            else if (id == Resource.Id.nav_contact)
            {
                fragment1 = new ContactUsFragment();
                SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, fragment1)
               .Commit();


                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);
            }

            //else if (id == Resource.Id.nav_setting)
            //{
            //    fragment1 = new NoticeBoardFinal();
            //}
            else if (id == Resource.Id.nav_rate)
            {
                var uri = Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=com.makemedroid.keye5bb63c9");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
               // fragment1 = new RateAppFragment();
                // SupportFragmentManager.BeginTransaction()
                //.Replace(Resource.Id.content_frame, fragment1)
                //.Commit();


                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else if (id == Resource.Id.nav_share)
            {
                fragment1 = new ShareFragment();
                SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, fragment1)
               .Commit();


                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);
            }
            //else if (id == Resource.Id.nav_invite)
            //{
            //    fragment1 = new InviteFragment();
            //    SupportFragmentManager.BeginTransaction()
            //   .Replace(Resource.Id.content_frame, fragment1)
            //   .Commit();


            //    DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //    drawer.CloseDrawer(GravityCompat.Start);
            //}
            else if (id == Resource.Id.nav_like)
            {
                fragment1 = new LikeFragment();
                SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, fragment1)
               .Commit();


                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);
            }




            else if (id == Resource.Id.nav_privacy)
            {

                fragment1 = new PrivacyFragment();

                SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, fragment1)
               .Commit();


                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);
            }

            else if (id == Resource.Id.nav_login)
            {

                // var intent3 = new Intent(this, typeof(LoginActivity_STPortal));
                // StartActivityForResult(intent3, 105);



                if (item.ItemId == Resource.Id.nav_login)
                {

                    pref = GetSharedPreferences("login", FileCreationMode.Private);
                    edit = pref.Edit();
                    username = pref.GetString("username", String.Empty);
                    profilepath = pref.GetString("profilePath", String.Empty);

                    userid = pref.GetInt("userid", 0);

                    if (username.Length<=0)
                    {

                        Log = false;
                        item.SetIcon(Resource.Drawable.menu_log);
                       

                        var intent3 = new Intent(this, typeof(LoginActivity_STPortal));
                        StartActivityForResult(intent3, 105);




                    }
                    else
                    {

                        Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                        alert.SetTitle("Logout");
                        alert.SetMessage("Do you want to logoff ?");
                        alert.SetPositiveButton(("Yes"), (sender, args) =>
                        {
                            item.SetIcon(Resource.Drawable.menu_log);
                           

                            edit.Clear();
                            edit.Apply();
                            username = "";
                            profilepath = "";

                            Toast.MakeText(this, "You have successfully logged off", ToastLength.Long).Show();
                            Finish();
                            var log_out = new Intent(this, typeof(MainActivity));
                            StartActivityForResult(log_out, 110);
                        });

                        alert.SetNegativeButton(("No"), (sender, args) =>
                        {

                        });

                        Dialog dialog = alert.Create();
                        dialog.Show();


                    }

                }
            }












            return true;
        }

        public void OnClick(View v)
        {
            var taskIntentActivity = new Intent(this, typeof(MainActivity));
            StartActivity(taskIntentActivity);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 105 || requestCode == 203)
            {
                pref = GetSharedPreferences("login", FileCreationMode.Private);
                edit = pref.Edit();
                username = pref.GetString("username", String.Empty);
                profilepath = pref.GetString("profilePath", String.Empty);

                userid = pref.GetInt("userid", 0);
                if (username.Length > 0)
                {
                   // mymenuitem.SetTitle("Logout");
                    txtdisplay.Text = username;
                    try
                    {
                        var imageBitmap = GetImageBitmapFromUrl(profilepath);

                        profileimage.SetImageBitmap(imageBitmap);
                    }
                    catch (Exception e)
                    {
                        
                    }




                }

                //if (mymenuitem.ItemId == Resource.Id.nav_login)
                //{

                //    if (Log)
                //    {

                //        Log = false;
                //        mymenuitem.SetIcon(Resource.Drawable.menu_log);
                //        mymenuitem.SetTitle("Logout");

                //        // var intent3 = new Intent(this, typeof(LoginActivity_STPortal));
                //        //  StartActivityForResult(intent3, 105);




                //    }
                //    else
                //    {

                //        Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                //        alert.SetTitle("Logout");
                //        alert.SetMessage("Do you want to logoff ?");
                //        alert.SetPositiveButton(("Yes"), (sender, args) =>
                //        {
                //            mymenuitem.SetIcon(Resource.Drawable.menu_log);
                //            mymenuitem.SetTitle("Login");

                //            edit.Clear();
                //            edit.Apply();
                //            username = "";
                //            profilepath = "";

                //            Toast.MakeText(this, "You have successfully logged off", ToastLength.Long).Show();

                //            var log_out = new Intent(this, typeof(MainActivity));
                //            StartActivityForResult(log_out, 110);
                //        });

                //        alert.SetNegativeButton(("No"), (sender, args) =>
                //        {

                //        });

                //        Dialog dialog = alert.Create();
                //        dialog.Show();


                //    }

                //}

            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

    }

}

//, Theme = "@style/AppTheme.NoActionBar", MainLauncher = true

//"http://mg.mahendras.org/api-mob-22-oct/mah-exam.php?api_key=HASH647MAH&action=data_list_menu&menu_id=45&student_id=StudentID&exam_category_id=ExamID&device_id=DeviceID&content_read=ReadType&page_index=0"



//    <? xml version="1.0" encoding="utf-8"?>
//<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
//    xmlns:app="http://schemas.android.com/apk/res-auto"
//    android:layout_width="match_parent"
//    android:layout_height="@dimen/nav_header_height"
//    android:background="@drawable/side_nav_bar"

//    android:orientation="vertical"



//    android:theme="@style/ThemeOverlay.AppCompat.Dark">

//    <ImageView
//        android:id="@+id/imageView"
//        android:layout_width="wrap_content"
//        android:layout_height="wrap_content"

//        app:srcCompat="@drawable/logobanner" />

//	<TextView
//        android:id="@+id/txtView" 
//		 android:text = "www.mahendras.org"
//        android:layout_width="wrap_content"
//        android:layout_height="wrap_content" />




//</LinearLayout>