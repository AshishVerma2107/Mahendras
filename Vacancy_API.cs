using System.Threading.Tasks;
using Refit;

namespace ImageSlider.API_Interface
{

    interface Vacancy_API
    {
        [Headers("User-Agent: :request:")]
        [Get("/api-mob-22-oct/mah-exam.php?api_key=HASH647MAH&action=data_list_menu&menu_id=23&student_id=StudentID&exam_category_id=ExamID&device_id=DeviceID&content_read=ReadType&page_index=0")]
        Task<ResDataVacancy> GetVacancyList();
    }
}