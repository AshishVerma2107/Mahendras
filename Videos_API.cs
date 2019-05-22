using System.Threading.Tasks;
using Refit;

namespace ImageSlider.Interface
{
    interface Video_API

    {
        [Headers("User-Agent: :request:")]
        // [Get("/api-mob-22-oct/mah-content.php?api_key=HASH647MAH&action=gettop5vid&keyword=Banking")]

        [Get("/api-mob-22-oct/mah-content.php?api_key=HASH647MAH&action=getplaylistvideo&playlist_id=PLPlACV9U2YPG0siztR9c8XYQRvLxvncmq&max_results=10")]

        Task<VideoAPI_Response> GetVideoList();
    }
}

