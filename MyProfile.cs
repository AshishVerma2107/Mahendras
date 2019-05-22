
using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.Graphics;
using Android.Support.Design.Widget;
using System;


namespace ImageSlider.Fragments
{
    public class MyProfile : Fragment
    {

        ImageView imageView;

        TextInputEditText DoB;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.MyProfile, container, false);

            var btnCamera = v.FindViewById<Button>(Resource.Id.camerabtn);
            imageView = v.FindViewById<ImageView>(Resource.Id.imageView);
            btnCamera.Click += BtnCamera_Click;

            DoB = v.FindViewById<TextInputEditText>(Resource.Id.dateofbirth);

            DoB.Click += (sender, e) =>
            {
                DateTime today = DateTime.Today;
                Android.App.DatePickerDialog dialog = new Android.App.DatePickerDialog(Activity, OnDateSet, today.Year, today.Month - 1, today.Day);

                dialog.DatePicker.MinDate = today.Millisecond;
                dialog.Show();
            };

            return v;
        }

        private void OnDateSet(object sender, Android.App.DatePickerDialog.DateSetEventArgs e)
        {


            // invoicedate.Text = e.Date.ToLongDateString();
            DoB.Text = e.Date.ToString("dd-MMM-yyyy");
            string date = e.Date.ToString("yyyy-MM-dd");

        }
        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode == 100 && resultCode == (int)Android.App.Result.Ok)
            {
                Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                imageView.SetImageBitmap(bitmap);
            }
        }

        public void BtnCamera_Click(object sender, System.EventArgs e)
        {


            //File _dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "Mahendras");
            //string file_path = _dir.ToString();
            //if (!_dir.Exists())
            //{
            //    _dir.Mkdirs();
            //}

            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 100);

        }
    }
}