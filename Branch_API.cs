using System.Threading.Tasks;
using Refit;

namespace ImageSlider.Interface

{
   interface Branch_API
    {
        [Headers("User-Agent: :request:")]
        [Get("/api-mob-22-oct/mah-master.php?api_key=HASH647MAH&action=get_branches_state")]
        Task<BranchAPI_Response> GetBranchList();
    }
}