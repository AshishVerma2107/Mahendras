using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ImageSlider
{
    class MockTestBean
    {
        private String id;
        private String coursename;
        private String language;
        private String date;
        private String attempted;
        private String correct;
        private String obtaind;
        private String result;
        private String course;
        private String totalmarks;
        private String rank;

        public string Id { get => id; set => id = value; }
        public string Coursename { get => coursename; set => coursename = value; }
        public string Language { get => language; set => language = value; }
        public string Date { get => date; set => date = value; }
        public string Attempted { get => attempted; set => attempted = value; }
        public string Correct { get => correct; set => correct = value; }
        public string Obtaind { get => obtaind; set => obtaind = value; }
        public string Result { get => result; set => result = value; }
        public string Course { get => course; set => course = value; }
        public string Totalmarks { get => totalmarks; set => totalmarks = value; }
        public string Rank { get => rank; set => rank = value; }
    }
}