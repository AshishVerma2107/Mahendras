using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Preferences;
using Android.Util;
using ImageSlider.Model;
using Java.Lang;
using Newtonsoft.Json;

namespace ImageSlider
{
    class ServiceHelper
    {
        HttpClient client;
        HttpWebRequest request;
        Cryptography cryptography;
        Geolocation geo;
        string licenceId, UserId, AppDateTime, geolocation;
        ISharedPreferences prefs;

        public ServiceHelper()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            cryptography = new Cryptography();
            geo = new Geolocation();

        }

        public void init(Context context)
        {
            prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            licenceId = prefs.GetString("LicenceId", "");
            UserId = prefs.GetString("DesignationId", "");
            AppDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            geolocation = geo.GetGeoLocation(context);
        }

        public async Task<string> GetServiceMethod(Context context, string methodName, string jsonData)
        {

            prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            init(context);

            if (jsonData.Equals(""))
            {
                jsonData = "w3ZUSfhMYXujjSK28/4kfw==";
            }

            string url = "http://mobileapi.work121.com/api/Comtax/GetMethod?associateID=8B280FFF-BFDD-4F62-8D46-08BA937DB981&associatepwd=mA121&licenceId=" +
                licenceId + "&UserId=" + UserId + "&methodname=" + methodName + "&jsonData=" + jsonData + "&AppDateTime=" + AppDateTime;

            request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Headers["userName"] = "uYfVkoP5BDouLkCBZ971sNZhzocdFLhmAvULyvsDnBo=";
            request.Headers["password"] = "/I/tmrWuA6AxGV6CiFgD/1AaOcV+2zzhS6OabhGQXVs=";
            try
            {
                using (WebResponse response = await request.GetResponseAsync().ConfigureAwait(false))
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        string text = reader.ReadToEnd();
                        //  string decrypted_Text = await OnCryptoAsync(text, "Decryption");
                        ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(text);
                        if (responseModel.ResponseCode.Equals("Ok"))
                        {
                            string decrypted_Text = await OnCryptoAsync(responseModel.ResponseValue, "Decryption");
                            reader.Close();
                            stream.Close();
                            return decrypted_Text;
                        }
                        else
                        {
                            return responseModel.ResponseCode;
                        }

                    }
                }
            }
            catch (Java.Lang.Exception e)
            {
                return null;
            }

        }

        public async Task<string> OnCryptoAsync(string textValue, string encDesc)
        {
            string text = "";
            if (encDesc.Equals("Encryption"))
            {
                text = textValue;
            }
            else
            {
                text = textValue.Substring(1, textValue.Length - 2);
            }
            string decryptedString = "";

            try
            {
                decryptedString = await cryptography.FunctionAsync(textValue, encDesc);
            }
            catch (Java.Lang.Exception e)
            {
                // Debug.WriteLine("error", e.Message);
            }

            return decryptedString;
        }


        public async Task<string> PostServiceMethod(LoginModel L_model)
        {

            string isoJson = JsonConvert.SerializeObject(L_model);

            var request = (HttpWebRequest)WebRequest.Create(Utility.login_registartion_url+"/DesktopModules/stpapi/API/Auth/login");

            //var postData = "thing1=hello";
            //postData += "&thing2=world";
            var data = Encoding.ASCII.GetBytes(isoJson);

            request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json; charset=utf-8";
            request.ContentLength = data.Length;
            var responseString = "";



            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response1 = (HttpWebResponse)request.GetResponse();
            if (response1.StatusCode == HttpStatusCode.OK)
            {
                responseString = new StreamReader(response1.GetResponseStream()).ReadToEnd();
            }
            else
            {
                responseString = "Error: " + response1.StatusCode.ToString();
            }




            return responseString;



        }

        public async Task<string> PostServiceMethodRegistration(LoginModel L_model)
        {
            string isoJson = JsonConvert.SerializeObject(L_model);

            var request = (HttpWebRequest)WebRequest.Create(Utility.login_registartion_url+"/DesktopModules/stpapi/API/Auth/register");

            //var postData = "thing1=hello";
            //postData += "&thing2=world";
            var data = Encoding.ASCII.GetBytes(isoJson);

            request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json; charset=utf-8";
            request.ContentLength = data.Length;
            request.UseDefaultCredentials = true;
            var responseString = "";



            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {

                var response1 = (HttpWebResponse)request.GetResponse();
                if (response1.StatusCode == HttpStatusCode.OK)
                {
                    responseString = new StreamReader(response1.GetResponseStream()).ReadToEnd();
                }

            }

            catch (WebException we)
            {
                var errresponse = (HttpWebResponse)we.Response;
                using (StreamReader errreader = new StreamReader(errresponse.GetResponseStream()))
                {
                    responseString = errreader.ReadToEnd();
                }

            }

            //var response1 = (HttpWebResponse)request.GetResponse();
            //if (response1.StatusCode == HttpStatusCode.OK)
            //{
            //    responseString = new StreamReader(response1.GetResponseStream()).ReadToEnd();
            //}
            //else
            //{
            //    responseString = "Error: " + response1.StatusCode.ToString();
            //}




            return responseString;

        }

        public async Task<string> ServiceAuthentication(STPLoginSocial L_model)
        {

            string isoJson = JsonConvert.SerializeObject(L_model);

            var request = (HttpWebRequest)WebRequest.Create(Utility.login_registartion_url+"/DesktopModules/stpapi/API/Auth/sociallogin");


            var data = Encoding.ASCII.GetBytes(isoJson);

            request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json; charset=utf-8";
            request.ContentLength = data.Length;
            var responseString = "";



            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {

                var response1 = (HttpWebResponse)request.GetResponse();
                if (response1.StatusCode == HttpStatusCode.OK)
                {
                    responseString = new StreamReader(response1.GetResponseStream()).ReadToEnd();
                }

            }

            catch (WebException we)
            {
                var errresponse = (HttpWebResponse)we.Response;
                using (StreamReader errreader = new StreamReader(errresponse.GetResponseStream()))
                {
                    responseString = errreader.ReadToEnd();
                }

            }


            //else if (response1.StatusCode == HttpStatusCode.Forbidden)
            //{
            //    responseString = new StreamReader(response1.GetResponseStream()).ReadToEnd();
            //}
            //else
            //{
            //    responseString = "Error: " + response1.StatusCode.ToString();
            //}




            return responseString;



        }
    }
}