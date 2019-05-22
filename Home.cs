using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;
using System.Threading;
using Android.Views;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using ImageSlider.Adapter;
using Android.Widget;
using ImageSlider.Model;
using Android.Util;
using Android.Graphics.Drawables;
using Android.Views.Animations;
using ImageSlider.Fragments;

namespace ImageSlider
{
    public class Home : Fragment
    {

        private List<int> itemData;
        private int imageValue;
        FragStateSupport _adapter;
        ViewPager _pager;
        // ExpandableHeightGridView gridcard;
      //  ExpandableListView myList, myList2;
       private bool Continue = false;
       // HomeOfferAdapter offersAdapter;
        public static List<BasketModel> data { get; private set; }

        public FragmentManager fragManager;


      //  ExpandableListView mainList;
        //string[] Noticeitems;

        //RecyclerView mRecycleView;
        //RecyclerView.LayoutManager mLayoutManager;
        //PhotoAlbum mPhotoAlbum;
        //HomeSlider_Adapter mAdapter;

        public override void OnCreate(Bundle savedInstanceState)
        {

            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            StrictMode.ThreadPolicy.Builder builder1 = new StrictMode.ThreadPolicy.Builder().PermitAll();
            StrictMode.SetThreadPolicy(builder1.Build());

            base.OnCreate(savedInstanceState);

          

            Activity.OverridePendingTransition(Resource.Animation.slide_up1, Resource.Animation.hold);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            itemData = new List<int>();
            itemData.Add(Resource.Drawable.Join_mahen);
            itemData.Add(Resource.Drawable.my_shop_final);
            itemData.Add(Resource.Drawable.youtube);
            itemData.Add(Resource.Drawable.st23);
            itemData.Add(Resource.Drawable.videoguru_23);

            //itemData.Add(Resource.Drawable.banner4);
            //itemData.Add(Resource.Drawable.banner20);
            //itemData.Add(Resource.Drawable.banner6);
            //itemData.Add(Resource.Drawable.banner7);
            //itemData.Add(Resource.Drawable.banner8);
            //itemData.Add(Resource.Drawable.banner9);
            //itemData.Add(Resource.Drawable.banner10);
            //itemData.Add(Resource.Drawable.banner11);
            //itemData.Add(Resource.Drawable.banner12);
            //itemData.Add(Resource.Drawable.banner13);
            //itemData.Add(Resource.Drawable.banner14);


            imageValue = 0;

            var view = inflater.Inflate(Resource.Layout.home, null);

           _adapter = new FragStateSupport(FragmentManager, itemData);
           _pager = view.FindViewById<ViewPager>(Resource.Id.pager);
            _pager.Adapter = _adapter;

          //  _pager.Touch += RunAutoImageScroller;

           // _pager.Clickable = true;
            Continue = true;

            if (Continue)
            {
                ThreadPool.QueueUserWorkItem(o => RunSlowMethod());
            }



            //mRecycleView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            //mLayoutManager = new LinearLayoutManager(Activity);
            //mRecycleView.SetLayoutManager(mLayoutManager);
            //mAdapter = new HomeSlider_Adapter(mPhotoAlbum);
            //mAdapter.ItemClick += MAdapter_ItemClick;
            //mRecycleView.SetAdapter(mAdapter);

            GridView gridcardabout_slide = view.FindViewById<GridView>(Resource.Id.gridviewslide);

            GridViewSlideAdapter gridview_slide = new GridViewSlideAdapter(Activity);


            gridcardabout_slide.Adapter = gridview_slide;

            gridcardabout_slide.FastScrollEnabled = true;

            gridview_slide.Itemclick += MAdapterSlide_ItemClick;






            GridView gridcardabout_exam = view.FindViewById<GridView>(Resource.Id.gridviewabout);

            GridViewAboutExam gridview_aboutexam = new GridViewAboutExam(Activity);

            gridcardabout_exam.Adapter = gridview_aboutexam;

            gridcardabout_exam.FastScrollEnabled = true;

            gridview_aboutexam.Itemclick += MAdapter_ItemClick;





            GridView gridcard_notice = view.FindViewById<GridView>(Resource.Id.gridviewnotice);

            GridView_Notice grid_notice = new GridView_Notice(Activity);


            gridcard_notice.Adapter = grid_notice;

            gridcard_notice.FastScrollEnabled = true;

            grid_notice.Itemclick += Notice_Adapter_ItemClick;



            GridView image_youtube = view.FindViewById<GridView>(Resource.Id.imageyoutube);

            YouTubeAdapter youadapter = new YouTubeAdapter(Activity);

            image_youtube.Adapter = youadapter;

            youadapter.Itemclickedyoutube += youTube_itemclicked;
            




           //GridView gridcard_weekly = view.FindViewById<GridView>(Resource.Id.weeklycurrentaffair);

           // GridView_Weekly_CurrentAffairs weekly_current_affair = new GridView_Weekly_CurrentAffairs(Activity);



           // gridcard_weekly.Adapter = weekly_current_affair;

           // gridcard_weekly.FastScrollEnabled = true;

           // weekly_current_affair.Itemclicked += Weekly_Adapter_ItemClick;



            GridView gridcard_videoLecture = view.FindViewById<GridView>(Resource.Id.gridviewvideo);

            GridView_VideoLecture videoLecture = new GridView_VideoLecture(Activity);

            gridcard_videoLecture.Adapter = videoLecture;
            
            videoLecture.Itemclickedguru += Bank_Video_Play_itemclcked;




            //mainList = view.FindViewById<ExpandableListView>(Resource.Id.ListView1);

            //NoticeBoardAdapter notice = new NoticeBoardAdapter(UserDataNoticeBoard.Users);

            //mainList.Adapter = notice;

            //mainList.setExpanded(true);

            //mainList.FastScrollEnabled = true;

            return view;

        }




        public void Weekly_Adapter_ItemClick(object sender, int e)
        {

            int counter = e;
            if (counter == 0)
            {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new Weekly_CurrentAffair_Fragment()).AddToBackStack(null).Commit();
               

            }

            if (counter == 1)

            {
                
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new Vocabulary_Fragment()).AddToBackStack(null).Commit();

               
            }
        }




       public void MAdapter_ItemClick(object sender, int e)
        {

            int counter = e;
            if (counter == 0)
            {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new YouTubeFragment()).AddToBackStack(null).Commit();

         }
            if (counter == 1)
            {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new ExamAlert_Fragment()).AddToBackStack(null).Commit();

                

               
             }



            if (counter == 2)
            {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new Weekly_CurrentAffair_Fragment()).AddToBackStack(null).Commit();


            }

            if (counter == 3)
            {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new Vocabulary_Fragment()).AddToBackStack(null).Commit();


            }
            if (counter == 4)
            {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new NoticeBoardFinal()).AddToBackStack(null).Commit();


            }
            //  Toast.MakeText(Activity, "This is photo number " + counter, ToastLength.Short).Show();

            if (counter == 5)
            {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new DailyCurrent_Affairs_Fragment()).AddToBackStack(null).Commit();


            }

        }


        public void Notice_Adapter_ItemClick(object sender, int e)
        {

            int counter = e;
            if (counter == 0)
            {
                var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org/Display/CategoryItems/158");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

                //Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new JoinClassRoom_Fragment()).AddToBackStack(null).Commit();

            }

            if (counter == 1)
            {

                var uri = Android.Net.Uri.Parse("https://mahendras.org/faq.aspx");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

                // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new FAQ_Fragment()).AddToBackStack(null).Commit();

            }

            if (counter == 2)
            {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new Facilities_Fragment()).AddToBackStack(null).Commit();

            }

            if (counter == 3)
            {
                StartActivityForResult(new Intent(Activity, typeof(VacancyFragment)), 111);
               // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new VacancyFragment()).AddToBackStack(null).Commit();

            }

            if (counter == 4)
            {


                var uri = Android.Net.Uri.Parse("https://career.mahendras.org");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
                // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new Career_Fragment()).AddToBackStack(null).Commit();

            }

            if (counter == 5)
            {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new JoinTelegramFragment()).AddToBackStack(null).Commit();

            }

            //if (counter == 6)
            //{
            //    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new JoinClassRoom_Fragment()).Commit();

            //}

            //if (counter == 7)
            //{
            //    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new DailyCurrent_Affairs_Fragment()).Commit();

            //}
            //if (counter == 8)
            //{
            //    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new ExamAlert_Fragment()).Commit();

            //}


        }

        public void MAdapterSlide_ItemClick(object sender, int e)
        {

            int counterslide = e;
            if(counterslide ==0)
                {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new Home()).AddToBackStack(null).Commit();
            }

            if (counterslide == 1)
            {
                var taskIntentActivity2 = new Intent(Activity, typeof(MainActivity1));
                StartActivityForResult(taskIntentActivity2,204);
               

                // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new ST_Portal_Fragment()).Commit();
            }

            if (counterslide == 2)
            {
                var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

                // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new MyShop()).AddToBackStack(null).Commit();
            }
            if (counterslide == 3)
            {
                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new AboutUsFragment()).AddToBackStack(null).Commit();
            }
            //if (counterslide == 4)
            //{
            //    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new ContactUsFragment()).Commit();
            //}
            //if (counterslide == 5)
            //{
            //    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new Gallery_Fragment()).Commit();
            //}
            //if (counterslide == 6)
            //{
            //    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new MyShop()).Commit();
            //}
            //if (counterslide == 7)
            //{
            //    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new ST_Portal_Fragment()).Commit();
            //}
            //if (counterslide == 8)
            //{
            //    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new AboutUsFragment()).Commit();
            //}
        }

       public void RunAutoImageScroller(object sender, EventArgs e)
        {
            

            var taskIntentActivity = new Intent(Activity, typeof(MainActivity));
            StartActivity(taskIntentActivity);
        }


        public void youTube_itemclicked(object sender, int e)
        {

            int counters = e;
            if (counters == 0)
            {

                Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new YouTubeFragment()).AddToBackStack(null).Commit();

                Activity.OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);

            }
        }


        public void Bank_Video_Play_itemclcked(object sender, int e)
        {

            int counters = e;
            if (counters == 0)
            {


                var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org/Display/DisplayItem?icd=299");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

               // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new BankVideoFragment()).AddToBackStack(null).Commit();
              //Activity.OverridePendingTransition(Resource.Animation.slide_up1, Resource.Animation.hold);

            }

            if (counters == 1)
            {
                var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org/Display/DisplayItem?icd=300");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

               // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new SSCVideoFragment()).AddToBackStack(null).Commit();
               // Activity.OverridePendingTransition(Resource.Animation.slide_up1, Resource.Animation.hold);

            }

            if (counters == 2)
            {

                var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org/Display/DisplayItem?icd=303");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

               // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new RailwayVideoFragment()).AddToBackStack(null).Commit();
               // Activity.OverridePendingTransition(Resource.Animation.slide_up1, Resource.Animation.hold);

            }
        }

        //public override void OnResume()
        //{
        //    base.OnResume();
        //    Activity.Finish();

        //}




        public void RunSlowMethod()
        {
            while (Continue)
            {
                imageValue = imageValue + 1;
                if (imageValue > 5)
                {
                    imageValue = 0;

                }
                Thread.Sleep(6000);
                if (Activity != null)
                {
                    Activity.RunOnUiThread(() => _pager.SetCurrentItem(imageValue, true));
                }
            }
        }


        public override void OnDestroy()
        {
            Continue = false;
            base.OnDestroy();
        }

       

    }


}




//<? xml version="1.0" encoding="utf-8"?>
//<ScrollView
//        xmlns:android="http://schemas.android.com/apk/res/android"
//        xmlns:cardview="http://schemas.android.com/apk/res-auto"
//		android:layout_width="match_parent"
//		android:layout_height="wrap_content"
//		android:id="@+id/scrollView1" >


//<LinearLayout
//         android:orientation="vertical"
//         android:layout_width="match_parent"
//         android:layout_height="match_parent">
//<android.support.v7.widget.CardView
//          cardview:cardCornerRadius="5dp"
//          cardview:cardMaxElevation="2dp"
//          cardview:cardUseCompatPadding="true"
//          cardview:contentPadding="5dp"
//          android:layout_width="match_parent"
//          android:layout_height="wrap_content">

//			<HorizontalScrollView
//            android:layout_width="match_parent"
//            android:layout_height="fill_parent">

//              <FrameLayout
//                android:layout_width="fill_parent"
//                android:layout_height="match_parent">

//		 <LinearLayout
//                    android:id="@+id/linearLayout_gridtableLayout"
//                    android:layout_width="320dp"
//                    android:layout_height="match_parent"
//                    android:orientation="horizontal">

//		<GridView
//                android:id="@+id/gridviewslide"

//				android:layout_width="fill_parent"
//                        android:layout_height="fill_parent"
//                        android:layout_margin="2dp"
//                        android:columnWidth="80dp"
//                        android:gravity="center"
//                        android:numColumns="4"
//                        android:horizontalSpacing="1dp"
//                        android:scrollbarAlwaysDrawHorizontalTrack="true"
//                        android:scrollbarAlwaysDrawVerticalTrack="true"
//                        android:scrollbars="horizontal"
//                        android:stretchMode="none"
//                        android:verticalSpacing="1dp">

//		</GridView>


//                </LinearLayout>
//            </FrameLayout>
//        </HorizontalScrollView>

//		</android.support.v7.widget.CardView >



//<LinearLayout
//                    android:orientation="vertical"
//                    android:layout_width="wrap_content"
//                    android:layout_height="wrap_content">

//<android.support.v4.view.ViewPager
//        android:id="@+id/pager"


//		android:layout_width="wrap_content"
//        android:layout_height="140dp" />
//</LinearLayout>





//		<android.support.v7.widget.CardView
//          cardview:cardCornerRadius="5dp"
//          cardview:cardMaxElevation="2dp"
//          cardview:cardUseCompatPadding="true"
//          cardview:contentPadding="5dp"
//          android:layout_width="match_parent"
//          android:layout_height="wrap_content">

//		<LinearLayout
//                    android:orientation="vertical"
//                    android:layout_width="match_parent"
//                    android:layout_height="wrap_content">

//		<GridView
//                android:id="@+id/gridviewabout"

//				android:layout_width="match_parent"
//				android:layout_height="120dp"
//				android:numColumns="3"

//				android:verticalSpacing="10dp"
//				android:layout_marginBottom="5dp"
//				android:layout_marginLeft="5dp"
//				android:layout_marginRight="5dp"
//				android:layout_marginTop="5dp"/>

//		</LinearLayout>	

//		</android.support.v7.widget.CardView >



//		<android.support.v7.widget.CardView
//          cardview:cardCornerRadius="5dp"
//          cardview:cardMaxElevation="2dp"
//          cardview:cardUseCompatPadding="true"
//          cardview:contentPadding="5dp"
//          android:layout_width="match_parent"
//          android:layout_height="wrap_content">



//		<LinearLayout
//                    android:orientation="vertical"
//                    android:layout_width="match_parent"
//                    android:layout_height="wrap_content">

//		<GridView
//                android:id="@+id/imageyoutube"

//				android:layout_width="match_parent"
//				android:layout_height="150dp"
//				android:numColumns="1"

//			     android:adjustViewBounds="true"

//                android:horizontalSpacing="5dp"
//				android:layout_marginBottom="5dp"
//				android:layout_marginLeft="5dp"
//				android:layout_marginRight="5dp"
//				android:layout_marginTop="5dp"/>

//		</LinearLayout>	


//		</android.support.v7.widget.CardView >










//		<android.support.v7.widget.CardView
//          cardview:cardCornerRadius="5dp"
//          cardview:cardMaxElevation="2dp"
//          cardview:cardUseCompatPadding="true"
//          cardview:contentPadding="5dp"
//          android:layout_width="match_parent"
//          android:layout_height="wrap_content">



//		<LinearLayout
//                    android:orientation="vertical"
//                    android:layout_width="match_parent"
//                    android:layout_height="wrap_content">

//		<GridView
//                android:id="@+id/gridviewnotice"

//				android:layout_width="match_parent"
//				android:layout_height="170dp"
//				android:numColumns="3"

//			     android:adjustViewBounds="true"
//			   android:verticalSpacing="30dp"
//                android:horizontalSpacing="5dp"
//				android:layout_marginBottom="5dp"
//				android:layout_marginLeft="5dp"
//				android:layout_marginRight="5dp"
//				android:layout_marginTop="5dp"/>

//		</LinearLayout>	


//		</android.support.v7.widget.CardView >

//		<android.support.v7.widget.CardView
//          cardview:cardCornerRadius="5dp"
//          cardview:cardMaxElevation="2dp"
//          cardview:cardUseCompatPadding="true"
//            cardview:contentPadding="5dp"

//          android:layout_width="match_parent"
//          android:layout_height="wrap_content">

//			<TextView
//            android:id="@+id/channelTitle"
//            android:layout_width="wrap_content"
//            android:layout_height="wrap_content"
//            android:text="Video Course"
//            android:textStyle="bold" />

//		<HorizontalScrollView
//            android:layout_width="match_parent"
//            android:layout_height="fill_parent"
//           >

//            <FrameLayout
//                android:layout_width="fill_parent"
//                android:layout_height="match_parent">

//                <LinearLayout
//                    android:id="@+id/gridtableLayout"
//                    android:layout_width="320dp"
//                    android:layout_height="match_parent"
//                    android:orientation="vertical">



//		   <GridView
//                        android:id="@+id/gridviewvideo"
//                        android:layout_width="fill_parent"
//                        android:layout_height="120dp"
//                        android:layout_marginBottom="5dp"
//				        android:layout_marginLeft="3dp"
//				         android:layout_marginRight="5dp"
//				         android:layout_marginTop="20dp"
//                        android:columnWidth="100dp"
//                        android:gravity="center"
//                        android:numColumns="3"
//                        android:horizontalSpacing="5dp"

//                        android:scrollbarAlwaysDrawHorizontalTrack="true"
//                        android:scrollbarAlwaysDrawVerticalTrack="true"
//                        android:scrollbars="horizontal"
//                        android:stretchMode="none"

//                        android:verticalSpacing="5dp">

//                    </GridView>
//	 </LinearLayout>
//            </FrameLayout>
//        </HorizontalScrollView>	
//		</android.support.v7.widget.CardView >



//	<!--	<android.support.v7.widget.CardView
//          cardview:cardCornerRadius="5dp"
//          cardview:cardMaxElevation="2dp"
//          cardview:cardUseCompatPadding="true"
//          cardview:contentPadding="5dp"
//          android:layout_width="match_parent"
//          android:layout_height="wrap_content">



//		<LinearLayout
//                    android:orientation="vertical"
//                    android:layout_width="match_parent"
//                    android:layout_height="wrap_content">

//		<GridView
//                android:id="@+id/imageyoutube1"

//				android:layout_width="match_parent"
//				android:layout_height="170dp"
//				android:numColumns="1"

//			     android:adjustViewBounds="true"

//                android:horizontalSpacing="5dp"
//				android:layout_marginBottom="5dp"
//				android:layout_marginLeft="5dp"
//				android:layout_marginRight="5dp"
//				android:layout_marginTop="5dp"/>

//		</LinearLayout>	


//		</android.support.v7.widget.CardView > -->




//		<!--<android.support.v7.widget.CardView
//          cardview:cardCornerRadius="5dp"
//          cardview:cardMaxElevation="2dp"
//          cardview:cardUseCompatPadding="true"
//          cardview:contentPadding="5dp"
//          android:layout_width="match_parent"
//          android:layout_height="wrap_content">



//		<LinearLayout
//                    android:orientation="vertical"
//                    android:layout_width="match_parent"
//                    android:layout_height="wrap_content">

//		<GridView
//                android:id="@+id/weeklycurrentaffair"

//				android:layout_width="match_parent"
//				android:layout_height="100dp"
//				android:numColumns="3"

//			     android:adjustViewBounds="true"
//			   android:verticalSpacing="30dp"
//                android:horizontalSpacing="5dp"
//				android:layout_marginBottom="5dp"
//				android:layout_marginLeft="5dp"
//				android:layout_marginRight="5dp"
//				android:layout_marginTop="5dp"/>

//		</LinearLayout>	-->


//		<!--</android.support.v7.widget.CardView >


//		<android.support.v7.widget.CardView
//          cardview:cardCornerRadius="5dp"
//          cardview:cardMaxElevation="2dp"
//          cardview:cardUseCompatPadding="true"
//          cardview:contentPadding="5dp"
//          android:layout_width="match_parent"
//          android:layout_height="wrap_content">



//		<LinearLayout
//                    android:orientation="vertical"
//                    android:layout_width="match_parent"
//                    android:layout_height="wrap_content">

//		<GridView
//                android:id="@+id/imageyoutube2"

//				android:layout_width="match_parent"
//				android:layout_height="170dp"
//				android:numColumns="1"

//			     android:adjustViewBounds="true"

//                android:horizontalSpacing="5dp"
//				android:layout_marginBottom="5dp"
//				android:layout_marginLeft="5dp"
//				android:layout_marginRight="5dp"
//				android:layout_marginTop="5dp"/>

//		</LinearLayout>	


//		</android.support.v7.widget.CardView >-->

//		<!--<android.support.v7.widget.CardView
//        cardview:cardCornerRadius="5dp"
//        cardview:cardMaxElevation="2dp"
//         cardview:cardUseCompatPadding="true"
//     cardview:contentPadding="5dp"
//     android:layout_width="match_parent"
//      android:layout_height="wrap_content">

//			<LinearLayout
//                    android:orientation="vertical"
//                    android:layout_width="match_parent"
//                    android:layout_height="match_parent">


//			<TextView
//            android:id="@+id/noticeBoard"
//            android:layout_width="wrap_content"
//            android:layout_height="wrap_content"
//            android:text="Notice Board"
//            android:textStyle="bold" />
//			</LinearLayout>
//		<LinearLayout
//                    android:orientation="vertical"
//                    android:layout_width="match_parent"
//                    android:layout_height="match_parent">

//		    <ImageSlider.ExpandableListView

//               android:id="@+id/ListView1"
//               android:layout_width="match_parent"
//               android:layout_height="wrap_content"
//               android:verticalSpacing="2dp"
//               android:horizontalSpacing="1dp"
//               android:layout_margin="20dp" />
//			</LinearLayout>
//			</android.support.v7.widget.CardView > -->









//</LinearLayout>
//</ScrollView>





     