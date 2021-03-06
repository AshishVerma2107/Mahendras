﻿using System;
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

namespace ImageSlider.API_Interface
{
    interface JoinUs_API
    {
        [Headers("User-Agent: :request:")]
        [Get("/api-mob-22-oct/mah-master.php?api_key=HASH647MAH&action=get_alias_content&menu_name=new-batches")]
        Task<JoinUsModel> GetJoinUsList();
    }
}