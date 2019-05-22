using ImageSlider.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ImageSlider
{
    public class DBHelper
    {
        SQLiteConnection db;

        public DBHelper()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Mahendras.db3");
            db = new SQLiteConnection(dbPath);
            try
            {
                db.CreateTable<AboutExamData>();
                db.CreateTable<GalleryData>();
                db.CreateTable<DownloadData>();
                db.CreateTable<VideoData>();
                db.CreateTable<BookMarkData>();

                db.CreateTable<StudyMaterialData>();
                db.CreateTable<NoticeData>();
                db.CreateTable<FAQ_Data>();
                db.CreateTable<Current_Affairs_Model>();
                db.CreateTable<ExamAlert_Model>();
                db.CreateTable<Weekly_Current_Affair_Model>();
                db.CreateTable<Vocabulary_Model>();
              //  db.CreateTable<LoginModel>();





            }
            catch (Exception e)
            {
                Debug.WriteLine("Table error: " + e.Message);
            }

        }

        public int insertAboutExamData(string AboutExamID, string AboutExamName)
        {
           // int insertId = 0;
            try
            {
                AboutExamData tbl = new AboutExamData();
                tbl.id = AboutExamID;
                tbl.name = AboutExamName;

                int i = db.Insert(tbl);

               
                return i;
            }
            catch (Exception ex)
            { return 0; }
        }


        public  List<AboutExamData> Get_AboutExamData()
        {
            try
            {
                List<AboutExamData> data1 = db.Query<AboutExamData>("SELECT * from [AboutExamData]");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
            

        }

        public int insertGalleryData(string Gallery_Id, string Gallery_Name, string Gallery_URL)
        {
            
            try
            {
                GalleryData tbl = new GalleryData();
                tbl.gallery_id = Gallery_Id;
                tbl.gallery_name = Gallery_Name;
                tbl.gallery_url = Gallery_URL;

                int i = db.Insert(tbl);


                return i;
            }
            catch (Exception ex)
            { return 0; }
        }

        public List<GalleryData> Get_GalleryData()
        {
            try
            {
                List<GalleryData> data2 = db.Query<GalleryData>("SELECT * from [GalleryData]");
                return data2;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public int insertDownLoadData(string Download_Id, string Download_File, string Download_Date, string Download_Title)
        {

            try
            {
                DownloadData tbl = new DownloadData();
                tbl.content_id = Download_Id;
                tbl.content_title = Download_Title;
                tbl.content_date = Download_Date;
                tbl.content_file = Download_File;
             

                int i = db.Insert(tbl);


                return i;
            }
            catch (Exception ex)
            { return 0; }
        }

        public List<DownloadData> Get_DownloadData()
        {
            try
            {
                List<DownloadData> data3 = db.Query<DownloadData>("SELECT * from [DownloadData]");

                return data3;
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        public int insertVideoData(string Video_Id, string Video_title,string Video_thum , string Video_url)
        {

            try
            {
                VideoData tbl = new VideoData();
               
                tbl.videoId = Video_Id;
                tbl.thumb = Video_thum;
                tbl.title = Video_title;
                tbl.videoURL = Video_url;
               

                int i = db.Insert(tbl);


                return i;
            }
            catch (Exception ex)
            { return 0; }
        }

        public List<VideoData> Get_VideoData()
        {
            try
            {
                List<VideoData> data_v = db.Query<VideoData>("SELECT * from [VideoData]");
                return data_v;
            }
            catch (Exception ex)
            {
                return null;
            }


        }



        public int insertStudyMaterialData(string Study_Id, string Study_title, string  Study_url, string Study_ordering)
        {

            try
            {
                StudyMaterialData tbl = new StudyMaterialData();

                tbl.content_id = Study_Id;
                tbl.content_title = Study_title;
                tbl.click_url = Study_url;
                tbl.ordering = Study_ordering;


                int i = db.Insert(tbl);


                return i;
            }
            catch (Exception ex)
            { return 0; }
        }

        public List<StudyMaterialData> Get_StudyData()
        {
            try
            {
                List<StudyMaterialData> data_v = db.Query<StudyMaterialData>("SELECT * from [StudyMaterialData]");
                return data_v;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public int insertNoticeBoardlData(string Notice_Id, string Notice_title, string Notice_text, string Notice_Date)
        {

            try
            {
                NoticeData tbl = new NoticeData();

                tbl.content_id = Notice_Id;
                tbl.content_title = Notice_title;
                tbl.content_text = Notice_text;
                tbl.content_date = Notice_Date;


                int i = db.Insert(tbl);


                return i;
            }
            catch (Exception ex)
            { return 0; }
        }

        public List<NoticeData> Get_NoticeBoardData()
        {
            try
            {
                List<NoticeData> data_N = db.Query<NoticeData>("SELECT * from [NoticeData]");
                return data_N;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public int insertFAQData(string FAQ_Id, string FAQ_title, string FAQ_text, string FAQ_Date)
        {

            try
            {
                FAQ_Data tbl = new FAQ_Data();

                tbl.content_id = FAQ_Id;
                tbl.content_title = FAQ_title;
                tbl.content_text = FAQ_text;
                tbl.content_date = FAQ_Date;


                int i = db.Insert(tbl);


                return i;
            }
            catch (Exception ex)
            { return 0; }
        }

        public List<FAQ_Data> Get_FAQ_Data()
        {
            try
            {
                List<FAQ_Data> data_FAQ = db.Query<FAQ_Data>("SELECT * from [FAQ_Data]");
                return data_FAQ;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public int insertBookMarkData(string BookMarkId, string BookMarkTitle, string BookMark_Date, string BookMarkFile)
        {
           
            try
            {
                BookMarkData tbl = new BookMarkData();
                tbl.content_id= BookMarkId;
                tbl.content_title = BookMarkTitle;
                tbl.content_date = BookMark_Date;
                tbl.content_file = BookMarkFile;

                int i = db.Insert(tbl);
                return i;
            }

            catch (Exception ex)
            { return 0; }
        }


        public List<BookMarkData> Get_BookMarkData()
        {
            try
            {
                List<BookMarkData> Book_data = db.Query<BookMarkData>("SELECT * from [BookMarkData]");
                return Book_data;
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        public int insertCurrentAffairData(string CurrId, string CurrTitle, string Curr_Date, string CurrFile)
        {

            try
            {
                Current_Affairs_Model tbl = new Current_Affairs_Model();
                tbl.content_id = CurrId;
                tbl.content_title = CurrTitle;
                tbl.content_date = Curr_Date;
                tbl.content_file = CurrFile;

                int i = db.Insert(tbl);
                return i;
            }

            catch (Exception ex)
            { return 0; }
        }


        public List<Current_Affairs_Model> Get_CurrentAffairData()
        {
            try
            {
                List<Current_Affairs_Model> Affairs_data = db.Query<Current_Affairs_Model>("SELECT * from [Current_Affairs_Model]");
                return Affairs_data;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public int insertWeeklyCurrentAffairData(string WeekId, string WeekTitle, string WeekDate, string WeekFile)
        {

            try
            {
                Weekly_Current_Affair_Model tbl = new Weekly_Current_Affair_Model();
                tbl.content_id = WeekId;
                tbl.content_title = WeekTitle;
                tbl.content_date = WeekDate;
                tbl.content_file = WeekFile;

                int i = db.Insert(tbl);
                return i;
            }

            catch (Exception ex)
            { return 0; }
        }


        public List<Weekly_Current_Affair_Model> Get_WeeklyCurrentAffairData()
        {
            try
            {
                List<Weekly_Current_Affair_Model> WeeklyAffairs_data = db.Query<Weekly_Current_Affair_Model>("SELECT * from [Weekly_Current_Affair_Model]");
                return WeeklyAffairs_data;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public int insertVocabularyData(string VocId, string VocTitle, string VocDate, string VocFile)
        {

            try
            {
                Vocabulary_Model tbl = new Vocabulary_Model();
                tbl.content_id = VocId;
                tbl.content_title = VocTitle;
                tbl.content_date = VocDate;
                tbl.content_file = VocFile;

                int i = db.Insert(tbl);
                return i;
            }

            catch (Exception ex)
            { return 0; }
        }


        public List<Vocabulary_Model> Get_VocabularyData()
        {
            try
            {
                List<Vocabulary_Model> voc_data = db.Query<Vocabulary_Model>("SELECT * from [Vocabulary_Model]");
                return voc_data;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public int insertExamAlertData(string AlertId, string AlertTitle, string AlertDate, string AlertFile)
        {

            try
            {
                ExamAlert_Model tbl = new ExamAlert_Model();
                tbl.content_id = AlertId;
                tbl.content_title = AlertTitle;
                tbl.content_date = AlertDate;
                tbl.content_file = AlertFile;

                int i = db.Insert(tbl);
                return i;
            }

            catch (Exception ex)
            { return 0; }
        }


        public List<ExamAlert_Model> Get_ExamAlertData()
        {
            try
            {
                List<ExamAlert_Model> alert_data = db.Query<ExamAlert_Model>("SELECT * from [ExamAlert_Model]");
                return alert_data;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public int insertRegistrationData(string userName, string Mobile_No, string E_mail, string passWord)
        {
            try
            {
                LoginModel tbl = new LoginModel();
                tbl.Username = userName;
                tbl.Mobile = Mobile_No;
                tbl.Email = E_mail;
                tbl.Password = passWord;

                int i = db.Insert(tbl);
                return i;
            }

            catch (Exception ex)
            { return 0; }
        }

    }
}
