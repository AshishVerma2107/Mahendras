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

namespace ImageSlider
{
    class UserStPackageComparable : IEqualityComparer<UserPackageModel>
    {
        public bool Equals(UserPackageModel x, UserPackageModel y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.CourseID == y.CourseID;
        }

        public int GetHashCode(UserPackageModel obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
           // int hashProductName = obj.CourseID == null ? 0 : obj.CourseID.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = obj.CourseID.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductCode;
        }
    }
}