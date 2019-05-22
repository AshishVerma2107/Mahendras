using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ImageSlider
{
    class Utility
    {
        public static void intalizejson()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };
        }

#if (DEBUG)
        public static string stapibaseUrl = "https://ts02.mrsdigitech.com:8050";
        public static string mibsapiBaseUrl = "http://wsa.mrsdigitech.com";
        public static string login_registartion_url = "https://stp.mrsdigitech.com";



#else
         public static string stapibaseUrl = "https://stapi.mahendras.org:8050";
        public static string mibsapiBaseUrl = "https://wsa.mahendras.org/mibsws";
        public static string login_registartion_url = "https://stportal.mahendras.org";
       

#endif


        public static bool IsNetworkConnected(Context ac)
        {
           var cm = (ConnectivityManager)ac.GetSystemService(Context.ConnectivityService);
           return cm.ActiveNetworkInfo == null ? false : cm.ActiveNetworkInfo.IsConnected;
        }

    }

   
}