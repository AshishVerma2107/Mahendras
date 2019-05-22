using System.Threading.Tasks;
using ImageSlider.Model;
using Refit;

namespace ImageSlider
{
    interface Gallery_API

    {
        [Headers("User-Agent: :request:")]
        [Get("/api-mob-22-oct/mah-master.php?api_key=HASH647MAH&action=get_gallery_all")]
        Task<GalleryAPI_Response> GetGalleryList();
    }
}
