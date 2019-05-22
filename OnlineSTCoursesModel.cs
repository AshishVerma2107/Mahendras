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
using Newtonsoft.Json;

namespace ImageSlider.Model
{
    public class OnlineSTCoursesModel
    {
        [JsonProperty(PropertyName = "ErrCode")]
        public int ErrCode { get; set; }

        [JsonProperty(PropertyName = "ErrText")]
        public string ErrText { get; set; }

        [JsonProperty(PropertyName = "Data")]
        public string Data { get; set; }
    }
    public class STCourseRecord
    {
        [JsonProperty(PropertyName = "CourseID")]
        public int CourseID { get; set; }

        [JsonProperty(PropertyName = "CourseName")]
        public string CourseName { get; set; }

        [JsonProperty(PropertyName = "CourseImage")]
        public string CourseImage { get; set; }

        [JsonProperty(PropertyName = "Approved")]
        public bool Approved { get; set; }
    }

    public class UserStPackageModel
    {
        [JsonProperty(PropertyName = "ErrCode")]
        public int ErrCode { get; set; }

        [JsonProperty(PropertyName = "ErrText")]
        public string ErrText { get; set; }

        [JsonProperty(PropertyName = "Data")]
        public string Data { get; set; }
    }

    public class UserPackageModel
    {
        [JsonProperty(PropertyName = "PackageID")]
        public int PackageID { get; set; }

        [JsonProperty(PropertyName = "CourseID")]
        public int CourseID { get; set; }

        [JsonProperty(PropertyName = "CourseName")]
        public string CourseName { get; set; }

        [JsonProperty(PropertyName = "PackageName")]
        public string PackageName { get; set; }

        [JsonProperty(PropertyName = "NumExamTotal")]
        public int NumExamTotal { get; set; }

        [JsonProperty(PropertyName = "NumPracticeTotal")]
        public int NumPracticeTotal { get; set; }

        [JsonProperty(PropertyName = "StartsFrom")]
        public string StartsFrom { get; set; }

        [JsonProperty(PropertyName = "ValidUptoDays")]
        public int ValidUptoDays { get; set; }

        [JsonProperty(PropertyName = "ExamGiven")]
        public int ExamGiven { get; set; }

        [JsonProperty(PropertyName = "PracticalGiven")]
        public int PracticalGiven { get; set; }

        public int disable_enable { get; set; }

        public float alpha { get; set; }

        public int colorcode { get; set; }

    }
}