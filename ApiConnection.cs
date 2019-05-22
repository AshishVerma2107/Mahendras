using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ImageSlider.Model;
using ImageSlider.MyTest;
using Refit;

namespace ImageSlider.Connection
{
    [Headers("User-Agent: :request:")]
    interface ApiConnection
    {
        [Get("/api/ost/GetOnlineSTCourses")]
        Task<UserStPackageModel> GetOnlineSTCoursesr();

        [Get("/api/ost/GetUserSTPackages?userid={userid}")]
        Task<UserStPackageModel> GetUserSTPackages(string userid);

        
    }
   
}