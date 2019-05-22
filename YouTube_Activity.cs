using Android.Support.V4.App;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using ImageSlider.Adapter;
using Android.Support.V7.App;

namespace ImageSlider
{
    
    public class YouTube_Activity : AppCompatActivity
    {
 
			int intDisplayWidth;
        int intDisplayHeight;

      //  List<VideoData> youTube;
       // string YouTubeVideo;

       // Videos_GridView_Adapter videosPostion;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.WebView_YouTube);
            var metrics = Resources.DisplayMetrics;
            //fix video screen height and width
            intDisplayWidth = (FnConvertPixelsToDp(metrics.WidthPixels) + 200);
            intDisplayHeight = (FnConvertPixelsToDp(metrics.HeightPixels)) / (2);
            FnPlayInWebView();
        }


        

        void FnPlayInWebView()
        {

            //Videos_GridView_Adapter videos_detail = new Videos_GridView_Adapter(this, youTube);

          

            string strUrl = "https://www.youtube.com/watch?v=rOUAWzxVwX0";

            string id = FnGetVideoID(strUrl);

            if (!string.IsNullOrEmpty(id))
            {
                strUrl = string.Format("http://youtube.com/embed/{0}", id);
            }
            else
            {
                Toast.MakeText(this, "Video url is not in correct format", ToastLength.Long).Show();
                return;
            }

            string html = @"<html><body><iframe width=""videoWidth"" height=""videoHeight"" src=""strUrl""></iframe></body></html>";
            var myWebView = (WebView)FindViewById(Resource.Id.videoView);
            var settings = myWebView.Settings;
            settings.JavaScriptEnabled = true;
            settings.UseWideViewPort = true;
            settings.LoadWithOverviewMode = true;
            settings.JavaScriptCanOpenWindowsAutomatically = true;
            settings.DomStorageEnabled = true;
            settings.SetRenderPriority(WebSettings.RenderPriority.High);
            settings.BuiltInZoomControls = false;

            settings.JavaScriptCanOpenWindowsAutomatically = true;
            myWebView.SetWebChromeClient(new WebChromeClient());
            settings.AllowFileAccess = true;
            settings.SetPluginState(WebSettings.PluginState.On);
            string strYouTubeURL = html.Replace("videoWidth", intDisplayWidth.ToString()).Replace("videoHeight", intDisplayHeight.ToString()).Replace("strUrl", strUrl);

            myWebView.LoadDataWithBaseURL(null, strYouTubeURL, "text/html", "UTF-8", null);

        }
        int FnConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }
        static string FnGetVideoID(string strVideoURL)
        {
            const string regExpPattern = @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)";
            //for Vimeo: vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)
            var regEx = new Regex(regExpPattern);
            var match = regEx.Match(strVideoURL);
            return match.Success ? match.Groups[1].Value : null;
        }

    }
}