using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ImageSlider.Model
{
    public class LoginModel
    {

        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public bool IsAuth { get; set; }
        public string DisplayName { get; set; }
        public string ImageURL { get; set; }
        public string LoginType { get; set; } //google, facebook ,// stportal, 
        public string hash { get; set; }
        public string ts { get; set; }



        public string GetHash()
        {
            ts = DateTime.Now.ToString("yyyyMMddHHmmss");
            string tmp = this.Password + ts + this.Username;
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(tmp));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
}
