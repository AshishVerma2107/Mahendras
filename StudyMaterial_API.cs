using System.Threading.Tasks;
using Refit;

namespace ImageSlider.API_Interface
{
    interface StudyMaterial_API
    {
        [Headers("User-Agent: :request:")]
        // [Get("/api-mob-22-oct/mah-content.php?api_key=HASH647MAH&action=gettop5vid&keyword=Banking")]

        [Get("/api-mob-22-oct/mah-master.php?api_key=HASH647MAH&action=get_banner&menu_id=3")]

        Task<StudyMaterialAPI_Response> GetStudyMaterialList();
    }
}