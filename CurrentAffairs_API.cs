using System.Threading.Tasks;
using ImageSlider.Model;
using Refit;

namespace ImageSlider.API_Interface
{
    interface CurrentAffairs_API
    {
        [Headers("User-Agent: :request:")]
        [Get("/api-mob-22-oct/mah-exam.php?api_key=HASH647MAH&action=data_list_menu&menu_id=44&student_id=StudentID&exam_category_id=ExamID&device_id=DeviceID&content_read=ReadType&page_index=0")]
        Task<CurrentAffairsAPI_Response> GetCurrentAffairsList();
    }
}