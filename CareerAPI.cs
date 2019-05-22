using System.Threading.Tasks;
using ImageSlider.Model;
using Refit;

namespace ImageSlider.API_Interface
{
    interface CareerAPI
    {

        [Headers("User-Agent: :request:")]
        [Get("/api-mob-22-oct/mah-content.php?api_key=HASH647MAH&action=career")]
        Task<CareerModel> GetCareerList();
    }
}