using System.Threading.Tasks;
using Refit;

namespace ImageSlider.API_Interface
{
    interface Download_API
    
   {
        [Headers("User-Agent: :request:")]
        [Get("/api-mob-22-oct/mah-exam.php?api_key=HASH647MAH&action=get_book&user_id=0&student_id=S20190401DPaym&exam_category_id=203&page_index=0")]
        Task <DownloadAPI_Response> GetDownloadList();
    }
}