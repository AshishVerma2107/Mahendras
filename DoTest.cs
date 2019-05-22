using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using static Android.Support.V4.View.ViewPager;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Refit;
using Newtonsoft.Json;
using ImageSlider.Connection;
using System.IO.Compression;
using Java.IO;
using Java.Net;

using Environment = Android.OS.Environment;

using System.Json;
using System.IO;
using System.Net;
using File = System.IO.File;
using System.Threading;
using System.Threading.Tasks;

namespace ImageSlider.MyTest
{
    [Activity(Label = "Test", Theme = "@style/MyTheme")]
    public class DoTest : AppCompatActivity, View.IOnClickListener, ImyInreface
    {
        Toolbar toolbar;
        FrameLayout viewpager;
        readonly String[] numberofquestion = { "1", "2", "3", "4", "5" };

        LinearLayout llpouse;
        ImageView ivMenu;
        int TestId;
        float negativemarks;
        CustomProgressDialog cp;
        List<questionmodel> qestionlist = new List<questionmodel>();
        List<questionpassagemodel> passagelist = new List<questionpassagemodel>();
        TextView txtTotalCount, txtIncrementCount;
        int position = 0;
        List<List<questionmodel>> groupedCustomerList;
        string myfilename1;
        int timeduration;
        string items;
        string startingquestionposition;
        string subjecttotalquestion;
        string langcode, testtype;
        CountDown countdown;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DoTest);
            txtTotalCount = FindViewById<TextView>(Resource.Id.totalcount);
            txtIncrementCount = FindViewById<TextView>(Resource.Id.incrementcount);
            TestId = Intent.GetIntExtra("TestID", 0);
            timeduration = Intent.GetIntExtra("totaltime", 0);
            testtype = Intent.GetStringExtra("testtype");
            negativemarks = Intent.GetFloatExtra("negativemarks", 0);
            items = Intent.GetStringExtra("items");
            langcode = Intent.GetStringExtra("langcode");
            startingquestionposition = Intent.GetStringExtra("startingquestionposition");
            subjecttotalquestion = Intent.GetStringExtra("subjecttotalquestion");
            llpouse = FindViewById<LinearLayout>(Resource.Id.testpouse);
            ivMenu = FindViewById<ImageView>(Resource.Id.testmenuimage);
            TextView txtCountdownTimer = FindViewById<TextView>(Resource.Id.countdowntimer);

            llpouse.SetOnClickListener(this);
            ivMenu.SetOnClickListener(this);

            viewpager = FindViewById<FrameLayout>(Resource.Id.testpaperviewpager);

            cp = new CustomProgressDialog(this);
            if (Utility.IsNetworkConnected(this))
            {
                CallApi();
            }
            else
            {

                Toast.MakeText(this, "Check your internet connection", ToastLength.Short).Show();
            }

        }

        void CallApi()
        {

            string strongPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            string filePath = System.IO.Path.Combine(strongPath, "Mahendra");


            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Mahendra");
            var jsontextfile = Path.Combine(path, "ST_" + langcode + "_" + TestId);
            if (Directory.Exists(jsontextfile))

            {
                callback();
            }
            else
            {
                downloadzipfile dl = new downloadzipfile(cp, TestId, this, langcode, this);
                dl.Execute();
            }
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:


                    Finish();
                    OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_down);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                Finish();
                OverridePendingTransition(Resource.Animation.hold, Resource.Animation.slide_down);
                return true;
            }

            return true;
        }

        public void OnClick(View v)
        {
            //if (v.Id == Resource.Id.testmenuimage)
            //{
            //    var intent = new Intent(this, typeof(rightdrawer));
            //    StartActivityForResult(intent, 101);
            //    OverridePendingTransition(Resource.Animation.slide_left, Resource.Animation.hold);
            //}
            //else
            //{
            //CustomDialog customDialog = new CustomDialog(this);
            //customDialog.Show();

            // }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {

        }
        public void callback()
        {
            string strongPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            string filePath = System.IO.Path.Combine(strongPath, "Mahendra");
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Mahendra");
            var jsontextfile = path + "/ST_" + langcode + "_" + TestId;

            if (Directory.Exists(jsontextfile))

            {
                string[] filelist = Directory.GetDirectories(jsontextfile);
                var filename = filelist[0];
                var myfilename = filename + "/testquestionsbylangCode.txt";
                myfilename1 = filename;
                //File file = new File(jsontextfile);
                //var filelist = file.ListFiles();
                //var filename="";
                //foreach (var innerfile in filelist)
                //{
                //     filename = innerfile.Name;
                //}
                //myfilename1 = jsontextfile + "/" + filename;
                //var myfilename = jsontextfile + "/" + filename+ "/testquestionsbylangCode.txt";
                try
                {
                    var filecontent = System.IO.File.ReadAllText(myfilename);
                    var jsonobject = JsonObject.Parse(filecontent);
                    if (jsonobject is JsonArray)
                    {
                        var jarray = jsonobject as JsonArray;
                        foreach (var item in jarray)
                        {
                            questionmodel qmodal = new questionmodel
                            {
                                Correctans = (item as JsonObject)["CorrectAns"].ToString().Trim('\"'),
                                Correctmarks = (item as JsonObject)["CorrectMarks"],
                                Datatype = (item as JsonObject)["DataType"],
                                Id = (item as JsonObject)["ID"],
                                Optionnumbering = (item as JsonObject)["OptionNumbering"].ToString().Trim('\"'),
                                Optnseqno = (item as JsonObject)["OptnSeqno"],
                                Passageid = (item as JsonObject)["PassageID"],
                                Qdata = (item as JsonObject)["Qdata"].ToString().Trim('\"'),
                                Qid = (item as JsonObject)["QID"],
                                Qtype = (item as JsonObject)["QType"],
                                Seqno = (item as JsonObject)["Seqno"],
                                Subjectid = (item as JsonObject)["SubjectID"],
                                Testqid = (item as JsonObject)["TestQID"],
                                selectedoption = 0,
                                colorcode = Resource.Drawable.whitecircle1,
                                textcolor = Resource.Color.black,
                                rightorwrongColorCode = Resource.Drawable.whitecircle1,
                                rightorwrongTextColor = Resource.Color.black,
                                markforreview = 0

                            };
                            qestionlist.Add(qmodal);
                        }
                    }
                    groupedCustomerList = qestionlist.GroupBy(u => u.Qid).Select(grp => grp.ToList()).ToList();
                    txtTotalCount.Text = groupedCustomerList.Count() + "";

                    //=================================fetch passage ==============================================//
                    var myfilenamepassage = filename + "/PassagebyTest.txt";
                    try
                    {
                        var filecontentpassage = System.IO.File.ReadAllText(myfilenamepassage);
                        var jsonobjectpassage = JsonObject.Parse(filecontentpassage);
                        if (jsonobjectpassage is JsonArray)
                        {
                            var jarray = jsonobjectpassage as JsonArray;
                            foreach (var item in jarray)
                            {
                                questionpassagemodel questionpassagemodelobj = new questionpassagemodel
                                {
                                    id = (item as JsonObject)["ID"],
                                    Passage = (item as JsonObject)["Passage"]

                                };

                                passagelist.Add(questionpassagemodelobj);
                            }
                        }
                        //=============================================================================================//



                        //var res = groupedCustomerList.ToDictionary(x => x);
                        //System.Console.WriteLine("deopanshu-->" + JsonConvert.SerializeObject(groupedCustomerList));

                        // DoTestAdapter dotestadapter = new DoTestAdapter(SupportFragmentManager, groupedCustomerList, passagelist,myfilename1);

                        // viewpager.Adapter = dotestadapter;
                    }
                    catch (Exception e)
                    {

                    }

                    //=============================================Fetch Instruction=======================================//
                    var myfilenameinstruction = filename + "/InstructionbyTest.txt";
                    string instructiontitle = "";
                    string instruction = "";
                    try
                    {
                        var filecontentindtruction = System.IO.File.ReadAllText(myfilenameinstruction);
                        var jsonobjectindtruction = JsonObject.Parse(filecontentindtruction);
                        if (jsonobjectindtruction is JsonArray)
                        {
                            var jarray = jsonobjectindtruction as JsonArray;
                            foreach (var item in jarray)
                            {

                                instructiontitle = (item as JsonObject)["Title"];
                                instruction = (item as JsonObject)["Instruction"];

                            }
                        }
                        timeduration = timeduration * 60 * 1000;
                        bool timebondflag;
                        if (TestInstruction.testInfoList[0].Duration > 0)
                        {
                            timebondflag = true;
                        }
                        else
                        {
                            timebondflag = false;
                        }
                        DosTestFragment dotestfragment = DosTestFragment.NewInstance(JsonConvert.SerializeObject(groupedCustomerList), JsonConvert.SerializeObject(passagelist), myfilename1, position, TestId, negativemarks, timeduration, instructiontitle, instruction, true, 0, true, items, startingquestionposition, subjecttotalquestion, langcode, 0, testtype, timebondflag);
                        SupportFragmentManager.BeginTransaction().Replace(Resource.Id.testpaperviewpager, dotestfragment).Commit();
                    }
                    catch (Exception e)
                    {

                    }
                    //======================================================================================================//
                }
                catch (Exception)
                {

                }

            }

            else
            {
                System.Console.WriteLine("File NOt Exist");
            }



        }



        class downloadzipfile : AsyncTask<string, string, string>
        {
            CustomProgressDialog cp;
            int TestId;
            ImyInreface myinteface;
            string langcode;
            Context context;
            public downloadzipfile(CustomProgressDialog cp, int TestId, ImyInreface myinteface, string langcode, Context context)
            {
                this.context = context;
                this.cp = cp;
                this.TestId = TestId;
                this.myinteface = myinteface;
                this.langcode = langcode;
            }


            protected override void OnPreExecute()
            {
                cp.Show();
            }

            protected override void OnPostExecute(string result)
            {
                //Thread.Sleep(1000);
                unzip();
                myinteface.callback();
                cp.Dismiss();

            }
            protected override string RunInBackground(params string[] @params)
            {

                //===============================external storage==========================//


                var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Mahendra");
                if (!Directory.Exists(path))
                {


                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception e)
                    {
                        Toast.MakeText(context, "Please give Storage permission in App Setting", ToastLength.Long).Show();
                    }
                }

                var outputpath = Path.Combine(path, langcode + "_" + TestId + ".zip");
                if (!File.Exists(outputpath))
                {
                    File.Create(outputpath).Dispose();
                }

                //string strongPath = Android.OS.Environment.ExternalStorageDirectory.Path;
                //string filePath = System.IO.Path.Combine(strongPath, "Mahendra");
                //if (Environment.ExternalStorageState.Equals(Environment.MediaMounted))
                //{

                //}
                //File f = new File(strongPath, "Mahendra");
                //if (!f.Exists())
                //{
                //    try
                //    {
                //        f.Mkdir();
                //    }
                //    catch (Exception)
                //    {
                //        Toast.MakeText(context, "Please give Storage permission in App Setting", ToastLength.Long).Show();
                //    }
                //}

                //File outputFile = new File(filePath, langcode + "_" + TestId + ".zip");
                //if (!outputFile.Exists())
                //{
                //    try
                //    {
                //        outputFile.CreateNewFile();
                //    }
                //    catch (Exception)
                //    {
                //        Toast.MakeText(context, "Please give Storage permission in App Setting", ToastLength.Long).Show();
                //    }

                //}

                //==========================================================================//

                //=============================Internal Storage=============================//

                //File file = new File(context.FilesDir, "Mahendra");


                //==========================================================================//

                int count;
                try
                {

                    WebClient webclient = new WebClient();
                    webclient.DownloadFile(Utility.stapibaseUrl + "/v1/app/getstbundle/" + TestId + "/" + langcode, path + "/" + langcode + "_" + TestId + ".zip");

                    //URL url = new URL(Utility.baseurl1+"/v1/app/getstbundle/" + TestId + "/"+ langcode);
                    //URLConnection connection = url.OpenConnection();
                    //connection.Connect();
                    //int LengthOfFile = connection.ContentLength;
                    //InputStream input = new BufferedInputStream(url.OpenStream(), LengthOfFile);
                    //OutputStream output = new FileOutputStream(outputFile);
                    //byte[] data = new byte[LengthOfFile];
                    //long total = 0;
                    //while ((count = input.Read(data)) != -1)
                    //{

                    //    output.Write(data, 0, count);
                    //}
                    //output.Flush();
                    //output.Close();
                    //input.Close();
                }
                catch (Exception ee)
                {

                }

                return null;
            }


            void unzip()
            {
                //string strongPath = Android.OS.Environment.ExternalStorageDirectory.Path;
                //string filePath = System.IO.Path.Combine(strongPath, "Mahendra");
                //string sourcepath = filePath + "/" +langcode + "_" + TestId +  ".zip";
                //File f = new File(filePath, "ST_"+langcode+"_" + TestId);
                //DateTime dateTime = DateTime.Now;
                //long creteiontime = dateTime.Millisecond;
                //if (!f.Exists())
                //{
                //    f.Mkdir();
                //    f.SetLastModified(creteiontime);

                //}
                //string destinationpath = filePath + "/ST_"+langcode+"_" + TestId;

                var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Mahendra");
                var sourcepath = Path.Combine(path, langcode + "_" + TestId + ".zip");
                var zipfolderpath = Path.Combine(path, "ST_" + langcode + "_" + TestId);
                if (!Directory.Exists(zipfolderpath))
                {
                    Directory.CreateDirectory(zipfolderpath);
                }
                var destinationpath = zipfolderpath;

                try
                {

                    ZipFile.ExtractToDirectory(sourcepath, destinationpath);
                }
                catch (Exception e)
                {

                    using (ZipArchive archive = ZipFile.OpenRead(sourcepath))
                    {
                        foreach (var entry in archive.Entries)
                        {

                            string extractPath = Path.GetFullPath(Path.Combine(destinationpath, entry.FullName));

                            if (extractPath.LastIndexOf('.') > 0)
                            {
                                try
                                {
                                    entry.ExtractToFile(extractPath);
                                }
                                catch (Exception)
                                {

                                }

                            }

                            //using (var entryStream = entry.Open())
                            //{
                            //    entryStream.CopyTo()
                            //    // Write unzipped file using an IO file stream.
                            //}
                        }
                    }


                    // Toast.MakeText(context, e.Message, ToastLength.Long).Show();
                }

            }

        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

    }
}