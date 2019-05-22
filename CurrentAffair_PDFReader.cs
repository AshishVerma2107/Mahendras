using System;
using System.IO;
using System.Net;

using Android.Support.V4.App;
using Android.Views;
using Android.Webkit;
using Android.OS;

using AndroidHUD;
using Android.Content;

namespace ImageSlider.Fragments
{
    public class CurrentAffair_PDFReader : Fragment
    {
        private string _documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        private string _pdfPath;
        private string _pdfFileName = "thePDFDocument4.pdf";
        private string _pdfFilePath;
        private WebView _webView;
        private string _pdfURLcurr = "";
        private WebClient _webClient = new WebClient();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.C_Affair_PDFReader_Layout, container, false);

            _webView = v.FindViewById<WebView>(Resource.Id.pdfcurrent);

            _webView.SetInitialScale(115);


            _pdfURLcurr = Arguments.GetString("pdfurlaffair", "");


            var settings = _webView.Settings;
            settings.JavaScriptEnabled = true;
            settings.AllowFileAccessFromFileURLs = true;
            settings.AllowUniversalAccessFromFileURLs = true;
            settings.BuiltInZoomControls = true;
            _webView.SetWebChromeClient(new WebChromeClient());

            DownloadPDFDocument();

            // Download_Click();

            // PdfClickHandler();

            return v;
        }


        //private void PdfClickHandler()
        //{
        //    var webClient = new WebClient();

        //    webClient.DownloadDataCompleted += (s, e) => {
        //        var data = e.Result;
        //        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        //        string localFilename = $"{_blueways}.pdf";
        //        File.WriteAllBytes(Path.Combine(documentsPath, localFilename), data);
        //        InvokeOnMainThread(() => {
        //            new UIAlertView("Done", "File downloaded and saved", null, "OK", null).Show();
        //        });
        //    };
        //    var url = new Uri("_blueway.PDF");
        //    webClient.DownloadDataAsync(url);
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

                    //  File.Create(_pdfFilePath);
                    File.Delete(_pdfFilePath);

                    //Android.Net.Uri uri = Android.Net.Uri.FromFile(new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory + _pdfFilePath));
                    //Android.App.DownloadManager dm;
                    //dm = Android.App.DownloadManager.FromContext(Activity);
                    //Android.App.DownloadManager.Request request = new Android.App.DownloadManager.Request(Android.Net.Uri.Parse(_pdfPath));
                    ////request.SetTitle("Mahendras App").SetDescription("Mahendras PDF");
                    //request.SetVisibleInDownloadsUi(true);
                    //request.SetNotificationVisibility(Android.App.DownloadVisibility.VisibleNotifyCompleted);
                    //request.SetDestinationUri(uri);
                    //var c = dm.Enqueue(request);
                }
            }




            // This will be executed when the pdf download is completed
            _webClient.DownloadDataCompleted += OnPDFDownloadCompleted;
            // Lets downlaod the PDF Document
            var url = new Uri(_pdfURLcurr);
            _webClient.DownloadDataAsync(url);
        }


        //public async void Download_Click()
        // {
        //     Android.App.DownloadManager dm;

        //     Android.Net.Uri uri= Android.Net.Uri.FromFile(new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory + _pdfURL));
        //    // string lastSegment = uri.PathSegments.Add();
        //     string struri = uri.ToString();

        //     if (System.IO.File.Exists(struri))
        //     {
        //         // string currenturi = uri + lastSegment;
        //         return;
        //     }
        //     else
        //     {
        //         dm = Android.App.DownloadManager.FromContext(Activity);
        //         Android.App.DownloadManager.Request request = new Android.App.DownloadManager.Request(Android.Net.Uri.Parse(_pdfFilePath));
        //         request.SetTitle("Mahendras App").SetDescription("Mahendras PDF");
        //         request.SetVisibleInDownloadsUi(true);
        //         request.SetNotificationVisibility(Android.App.DownloadVisibility.VisibleNotifyCompleted);
        //         request.SetDestinationUri(uri);
        //         var c = dm.Enqueue(request);
        //     }


        // }

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


    //public class DownloadPDFFromURL : AsyncTask<string, string, string>
    //    {
    //    private Context context;
    //    private Android.App.ProgressDialog pDialog;

    //    public DownloadPDFFromURL(Context con_text)
    //    {
    //        this.context = con_text;
    //    }

    //    protected override void OnPreExecute()
    //    {
    //        pDialog = new Android.App.ProgressDialog(context);
    //        pDialog.SetMessage("Downloading file. Please wai...");
    //        pDialog.Indeterminate = false;
    //        pDialog.Max = 100;
    //        pDialog.SetProgressStyle();


    //        base.OnPreExecute();
    //    }


    //}
}