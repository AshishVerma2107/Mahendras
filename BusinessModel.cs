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
using SQLite;
namespace ImageSlider.Model
{
    public class BusinessModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string BusinessTypeID { get; set; }
        public string BusinessType { get; set; }
        public string BusinessSubType { get; set; }
    }
}