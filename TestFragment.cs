using System.Text;
using Android.OS;
using Android.Views;
using Android.Widget;
//using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.V4.App;
using Android.Views;
using System.Drawing;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using System;
using Android.Views.Animations;
using Android.Support.V4.View.Accessibility;
using System.IO;
using System.Net;
using System.Threading.Tasks;


namespace ImageSlider.Fragments
{
    public class TestFragment : Fragment,View.IOnClickListener
    {
        private const string KeyContent = "TestFragment:Content";
        private string _content = "???";
        private int itemData;
        private Bitmap myBitmap;
        private ImageView ivImageView;

        int position;

        public static TestFragment NewInstance()
        {
            TestFragment f = new TestFragment();
            return f;
        }
        public void setImageList(int intData,int position)
        {
            this.itemData = intData;
            this.position = position;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if ((savedInstanceState != null) && savedInstanceState.ContainsKey(KeyContent))
                _content = savedInstanceState.GetString(KeyContent);
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString(KeyContent, _content);
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.ViewImageFlipper, container, false);
            ivImageView = root.FindViewById<ImageView>(Resource.Id.imgviewflipper);

            int height = ivImageView.Height;
            int width = Resources.DisplayMetrics.WidthPixels;

            BitmapFactory.Options options = GetBitmapOptionsOfImage();


            using (Bitmap bitmapToDisplay = LoadScaledDownBitmapForDisplay(options, 500, 300))
            {

                ivImageView.SetImageBitmap(bitmapToDisplay);
            }
            ivImageView.SetOnClickListener(this);
            return root;
        }
        public Bitmap LoadScaledDownBitmapForDisplay(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {

            options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

            options.InJustDecodeBounds = false;
            return BitmapFactory.DecodeResource(Resources, itemData, options);
        }

        public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {

            float height = options.OutHeight;
            float width = options.OutWidth;
            double inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                int halfHeight = (int)(height / 2);
                int halfWidth = (int)(width / 2);


                while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return (int)inSampleSize;
        }

        public BitmapFactory.Options GetBitmapOptionsOfImage()
        {

            BitmapFactory.Options options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true,
                InPurgeable = true,
            };


            Bitmap result = BitmapFactory.DecodeResource(Resources, itemData, options);
            int imageHeight = options.OutHeight;
            int imageWidth = options.OutWidth;
            Console.WriteLine(string.Format("Original Size= {0}x{1}", imageWidth, imageHeight));
            return options;
        }

        public override void OnDestroyView()
        {

            base.OnDestroyView();
            if (ivImageView != null)
            {
                ivImageView.SetImageBitmap(null);
                GC.Collect(1);
            }
        }

        public void OnClick(View v)
        {
            if (position == 0)
            {

                var uri = Android.Net.Uri.Parse("https://www.mahendras.org/branches.aspx");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

               // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new MahendrasORG_Fragment() ).Commit();
            }
            if (position == 1)
            {

                var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
               // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new MyShop()).Commit();
            }
            if (position == 2)
            {

                var uri = Android.Net.Uri.Parse("https://www.youtube.com/results?search_query=mahendra+guru");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
              //  Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new YouTubeFragment()).Commit();
            }
            if (position == 3)
            {

                var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

               // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new MyShop()).Commit();
            }
            if (position == 4)
            {
                var uri = Android.Net.Uri.Parse("https://myshop.mahendras.org");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

                // Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new YouTubeFragment()).Commit();

            }
        }
    }
}
