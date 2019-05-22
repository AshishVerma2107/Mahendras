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

namespace ImageSlider.MyTest
{
    class AnswerPlatee
    {

        private string text;
        private int colorcode;
        private int textColor;
        private Boolean isSelected;
        private int stripecolor;

        public string Text { get => text; set => text = value; }
        public int Colorcode { get => colorcode; set => colorcode = value; }
        public int TextColor { get => textColor; set => textColor = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public int Stripecolor { get => stripecolor; set => stripecolor = value; }
    }
}