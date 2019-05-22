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
    class GridModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public GridModel(string name)
        {
            this.Name = name;
            // this.ImageUrl = image;
        }
    }
}