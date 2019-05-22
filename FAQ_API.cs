using System.Threading.Tasks;
using ImageSlider.Model;
using Refit;

namespace ImageSlider.API_Interface
{
    interface FAQ_API
    {
        [Headers("User-Agent: :request:")]


        [Get("/api-mob-22-oct/mah-content.php?api_key=HASH647MAH&action=faqs&exam_category_id=203&page_index=0")]

        Task<FAQ_ResData> GetFAQList();
    }
}