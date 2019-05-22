
using System;
using System.IO;
using System.Net;

using Android.Support.V4.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.OS;

using AndroidHUD;

namespace ImageSlider.Fragments
{
    public class PrivacyFragment : Fragment
    {
        private string _documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        private string _pdfPath;
        private string _pdfFileName = "thePDFDocument.pdf";
        private string _pdfFilePath;
        private WebView _webView;
        private string _pdfURL = @"https://mahendras.org/policy/privacynotice.pdf";
        private WebClient _webClient = new WebClient();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.Privacy_Layout, container, false);
            // _pdfURL = Intent.Arg
            _webView = v.FindViewById<WebView>(Resource.Id.webView1);

            _webView.SetInitialScale(115);
            var settings = _webView.Settings;
            settings.JavaScriptEnabled = true;
            settings.AllowFileAccessFromFileURLs = true;
            settings.AllowUniversalAccessFromFileURLs = true;
            settings.BuiltInZoomControls = true;
            _webView.SetWebChromeClient(new WebChromeClient());

            DownloadPDFDocument();

            return v;
        }
        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    _webView.LoadUrl("javascript:window.location.reload( true )");
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    _webView.ClearCache(true);
        //}

        private void DownloadPDFDocument()
        {
            AndHUD.Shared.Show(Activity, "Please Wait....\n We are working for your request...", -1, MaskType.Clear);

            _pdfPath = _documentsPath + "/PDFView";
            _pdfFilePath = Path.Combine(_pdfPath, _pdfFileName);

            // Check if the PDFDirectory Exists
            if (!Directory.Exists(_pdfPath))
            {
                Directory.CreateDirectory(_pdfPath);
            }
            else
            {
                // Check if the pdf is there, If Yes Delete It. Because we will download the fresh one just in a moment
                if (File.Exists(_pdfFilePath))
                {
                    File.Delete(_pdfFilePath);
                }
            }

            // This will be executed when the pdf download is completed
            _webClient.DownloadDataCompleted += OnPDFDownloadCompleted;
            // Lets downlaod the PDF Document
            var url = new Uri(_pdfURL);
            _webClient.DownloadDataAsync(url);
        }

        private void OnPDFDownloadCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            // Okay the download's done, Lets now save the data and reload the webview.
            var pdfBytes = e.Result;
            File.WriteAllBytes(_pdfFilePath, pdfBytes);

            if (File.Exists(_pdfFilePath))
            {
                var bytes = File.ReadAllBytes(_pdfFilePath);
            }

            _webView.LoadUrl("file:///android_asset/pdfviewer/index.html?file=" + _pdfFilePath);

            AndHUD.Shared.Dismiss();
        }
    }

}