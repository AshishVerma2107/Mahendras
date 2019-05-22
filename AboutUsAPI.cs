using System.Threading.Tasks;
using Refit;

namespace ImageSlider.Model
{
    interface AboutUsAPI
    {
  
        [Headers("User-Agent: :request:")]
        [Get("/api-mob-22-oct/mah-master.php?api_key=HASH647MAH&action=get_alias_content&menu_name=about-us")]
        Task<AboutUsModel> GetAboutExamList();
    }
}