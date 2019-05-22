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

namespace ImageSlider.Model
{
    
    public class BasketModel
    {
        
        public Int64 Id { get; set; }
        public int Image { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Spin { get; set; }
        public string OriginalPrice { get; set; }
        public string NewPrice { get; set; }
        public int Quant { get; set; }
        public bool SelectItem { get; set; }
    }
}