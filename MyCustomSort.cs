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
using ImageSlider.Model;

namespace ImageSlider.MyTest
{
    class MyCustomSort : IEqualityComparer<TestInfoListRecord>
    {
        public bool Equals(TestInfoListRecord x, TestInfoListRecord y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

          
            return x.SeqNo == y.SeqNo;
        }

        public int GetHashCode(TestInfoListRecord obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashProductCode = obj.SeqNo.GetHashCode();
           
            return hashProductCode;
        }
    }
}