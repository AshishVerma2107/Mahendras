using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using ImageSlider.Model;

namespace ImageSlider.MyTest
{
    class TestSelectionAdapter : RecyclerView.Adapter
    {
        
        List<UserPackageModel> enablelist = new List<UserPackageModel>();
        List<UserPackageModel> disablelist = new List<UserPackageModel>();
       public static  List<UserPackageModel> finallist = new List<UserPackageModel>();
        List<UserPackageModel> alllist = new List<UserPackageModel>();
       
        int[] colorcode = { };
        int[] imagename = { };
        int lastPosition = -1;
        public event EventHandler<int> ItemClick;
        Activity ac;
        View itemView;
        RecyclerView mRecyclerView;
        ISharedPreferences pref;
        ISharedPreferencesEditor edit;
        
        public TestSelectionAdapter(List<UserPackageModel> alllist,List<UserPackageModel> stpackagelist, int[] colorcode, int[] imagename, Activity ac, RecyclerView mRecyclerView)
        {

            
            disablelist = alllist.Except(stpackagelist,new UserStPackageComparable()).ToList<UserPackageModel>();
            enablelist = alllist.Except(disablelist,new UserStPackageComparable()).ToList<UserPackageModel>();
            Random random = new Random();
            pref = ac.GetSharedPreferences("login", FileCreationMode.Private);
            edit = pref.Edit();
            bool  pluscard = pref.GetBoolean("pluscard",false);
            try
            {
                for (int i = 0; i < enablelist.Count; i++)
                {

                    int randomposition = random.Next(0, colorcode.Length - 1);
                    enablelist[i].alpha = 1f;
                    enablelist[i].colorcode = colorcode[randomposition];
                    enablelist[i].disable_enable = 1;


                }
            }
            catch (Exception e)
            {
                Toast.MakeText(ac, "disable-->" + e.Message, ToastLength.Short).Show();
            }
            try
            {
                for (int i = 0; i < disablelist.Count; i++)
                {

                    int randomposition = random.Next(0, colorcode.Length - 1);
                    disablelist[i].alpha = 0.3f;
                    disablelist[i].colorcode = colorcode[randomposition];
                    if (disablelist[i].CourseName.Equals("*PLUS*"))
                    {
                        if (pluscard)
                        {
                            disablelist[i].alpha = 1f;
                            disablelist[i].disable_enable = 1;
                            disablelist[i].PackageID = 96;

                        }
                        else
                        {
                            disablelist[i].disable_enable = 0;
                        }
                    }
                    else
                    {
                        disablelist[i].disable_enable = 0;
                    }



                }
            }
            catch (Exception e)
            {
                Toast.MakeText(ac, "enable-->" + e.Message, ToastLength.Short).Show();
            }

            finallist =  enablelist.Concat(disablelist).ToList<UserPackageModel>();
            
            this.ac = ac;
            this.colorcode = colorcode;
            this.imagename = imagename;
            this.mRecyclerView = mRecyclerView;
         
         

        }
        public override int ItemCount
        {
            get { return finallist.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder photoViewHolder = holder as PhotoViewHolder;
           
            photoViewHolder.Caption.Text = finallist[position].CourseName;
            //int color = Resource.Color.abc_background_cache_hint_selector_material_dark;
            photoViewHolder.ItemView.Alpha = finallist[position].alpha;
            var color = new Color(ContextCompat.GetColor(ac, finallist[position].colorcode));
            photoViewHolder.cardview.SetBackgroundColor(color);
            photoViewHolder.Image.SetBackgroundResource(imagename[0]);
            Animation animation = AnimationUtils.LoadAnimation(ac, (position > lastPosition) ? Resource.Animation.slide_up : Resource.Animation.slide_up);
            photoViewHolder.ItemView.StartAnimation(animation);
            lastPosition = position;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.TestSelection_Row, parent, false);
            PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick, mRecyclerView);
            return vh;
        }
        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }
    }
    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public TextView Caption { get; set; }
        public LinearLayout cardview { get; set; }

        public PhotoViewHolder(View itemview, Action<int> listener, RecyclerView mRecyclerView) : base(itemview)
        {
            //Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemview.FindViewById<TextView>(Resource.Id.imagename);
            cardview = itemview.FindViewById<LinearLayout>(Resource.Id.cardviewfordashboard);
            Image = itemview.FindViewById<ImageView>(Resource.Id.dashobardimage);

            itemview.Click += (sender, e) => listener(mRecyclerView.GetChildAdapterPosition((View)sender));
        }
    }
}