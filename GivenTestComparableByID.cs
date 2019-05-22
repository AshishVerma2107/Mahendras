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
    class GivenTestComparableByID : IEqualityComparer<AllTestModelData>
    {
        public bool Equals(AllTestModelData x, AllTestModelData y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.ID == y.ID;
        }

        public int GetHashCode(AllTestModelData obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            // int hashProductName = obj.CourseID == null ? 0 : obj.CourseID.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = obj.ID.GetHashCode();
            //Get hash code for the Code field.
            
            //Calculate the hash code for the product.
            return hashProductCode ;
        }
    }
}