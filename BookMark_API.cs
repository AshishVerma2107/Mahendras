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
using Refit;

namespace ImageSlider.API_Interface
{
    interface BookMark_API
    {
        [Headers("User-Agent: :request:")]
        [Get("/api-mob-22-oct/mah-exam.php?api_key=HASH647MAH&action=get_down&student_id=S20190401DPaym&device_id=3f4fc73&exam_category_id=203&page_index=0")]
        Task<BookMarkAPI_Response> GetBookMarkList();
    }
}