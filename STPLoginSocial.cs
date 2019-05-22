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
   public class STPLoginSocial
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string hash { get; set; }
        public string ts { get; set; }

        public string GetHashforSocial (string myts)
        {
            
            string tmp = this.Password + myts + this.Username;
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