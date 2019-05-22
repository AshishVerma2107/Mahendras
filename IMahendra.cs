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
using Refit;

namespace ImageSlider.Connection
{
    //[Headers("User-Agent: :request:")]
     [Headers("Content-Type: application/json")]
    interface IMahendra
    {
        [Post("/DesktopModules/stpapi/API/Auth/register")]
        Task<string> Registration ([Body] LoginModel lmodel);
    }
}