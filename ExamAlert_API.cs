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

namespace ImageSlider.API_Interface
{
    interface ExamAlert_API
    {
        [Headers("User-Agent: :request:")]
        [Get("/api-mob-22-oct/mah-exam.php?api_key=HASH647MAH&action=data_list_menu&menu_id=59&student_id=StudentID&exam_category_id=ExamID&device_id=DeviceID&content_read=ReadType&page_index=0")]
        Task<ExamAlertAPI_Response> GetExamAlertList();
    }
}