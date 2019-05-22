//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Toolbar = Android.Support.V7.Widget.Toolbar;
//using Android.Support.Design.Widget;
//using Android.Support.V7.App;
//namespace ImageSlider.NoticeBoard
//{
    
//    [Activity(Label = "NoticeBoard", Theme = "@style/MyTheme")]
//    public class NoticeBoard : AppCompatActivity
//    {
//        Toolbar toolbar;
//        ExpandableListAdapter listAdapter;
//        ExpandableListView expListView;
//        List<string> listDataHeader;
//        Dictionary<string, List<string>> listDataChild;
//        int previousGroup = -1;
//        protected override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);
//            SetContentView(Resource.Layout.noticeboard);
//            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
//            SetSupportActionBar(toolbar);
//            //For showing back button  
//            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
//            SupportActionBar.SetHomeButtonEnabled(true);
//            toolbar.SetTitle(Resource.String.MyRequest);
//            expListView = FindViewById<ExpandableListView>(Resource.Id.lvExp);

//            // Prepare list data
//            FnGetListData();

//            //Bind list
//            listAdapter = new ExpandableListAdapter(this, listDataHeader, listDataChild);
//            expListView.SetAdapter(listAdapter);

//            FnClickEvents();
//        }
//        void FnClickEvents()
//        {
//            //Listening to child item selection
//            expListView.ChildClick += delegate (object sender, ExpandableListView.ChildClickEventArgs e) {
//                Toast.MakeText(this, "child clicked", ToastLength.Short).Show();
//            };

//            //Listening to group expand
//            //modified so that on selection of one group other opened group has been closed
//            expListView.GroupExpand += delegate (object sender, ExpandableListView.GroupExpandEventArgs e) {

//                if (e.GroupPosition != previousGroup)
//                    expListView.CollapseGroup(previousGroup);
//                previousGroup = e.GroupPosition;
//            };

//            //Listening to group collapse
//            expListView.GroupCollapse += delegate (object sender, ExpandableListView.GroupCollapseEventArgs e) {
//                Toast.MakeText(this, "group collapsed", ToastLength.Short).Show();
//            };

//        }
//        void FnGetListData()
//        {
//            listDataHeader = new List<string>();
//            listDataChild = new Dictionary<string, List<string>>();

//            // Adding child data
//            listDataHeader.Add("Computer science");
//            listDataHeader.Add("Electrocs & comm.");
//            listDataHeader.Add("Mechanical");

//            // Adding child data
//            var lstCS = new List<string>();
//            lstCS.Add("Data structure");
//            lstCS.Add("C# Programming");
//            lstCS.Add("Java programming");
//            lstCS.Add("ADA");
//            lstCS.Add("Operation reserach");
//            lstCS.Add("OOPS with C");
//            lstCS.Add("C++ Programming");

//            var lstEC = new List<string>();
//            lstEC.Add("Field Theory");
//            lstEC.Add("Logic Design");
//            lstEC.Add("Analog electronics");
//            lstEC.Add("Network analysis");
//            lstEC.Add("Micro controller");
//            lstEC.Add("Signals and system");

//            var lstMech = new List<string>();
//            lstMech.Add("Instrumentation technology");
//            lstMech.Add("Dynamics of machinnes");
//            lstMech.Add("Energy engineering");
//            lstMech.Add("Design of machine");
//            lstMech.Add("Turbo machine");
//            lstMech.Add("Energy conversion");

//            // Header, Child data
//            listDataChild.Add(listDataHeader[0], lstCS);
//            listDataChild.Add(listDataHeader[1], lstEC);
//            listDataChild.Add(listDataHeader[2], lstMech);
        


//    }
//        public override bool OnOptionsItemSelected(IMenuItem item)
//        {
//            switch (item.ItemId)
//            {
//                case Android.Resource.Id.Home:


//                    Finish();
//                    OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_down);
//                    return true;
//            }
//            return base.OnOptionsItemSelected(item);
//        }

//        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
//        {
//            if (keyCode == Keycode.Back)
//            {
//                Finish();
//                OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_down);
//                return true;
//            }

//            return true;
//        }
//        // Create your application here
//    }
    
//}