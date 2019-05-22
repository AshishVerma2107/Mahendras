using System.Threading.Tasks;
using ImageSlider.Model;
using Refit;

namespace ImageSlider.API_Interface
{
    interface NoticeBoard_API

    {
        [Headers("User-Agent: :request:")]
        // [Get("/api-mob-22-oct/mah-exam.php?api_key=HASH647MAH&action=noticeboard&exam_category_id=203&content_date=2019-04-24&count=0")]

        [Get("/api-mob-22-oct/mah-exam.php?api_key=HASH647MAH&action=noticeboard&exam_category_id=203&content_date={date}&count=0")]

        Task<NoticeBoadrAPI_Response> GetNoticeBoardList(string date);
    }
}