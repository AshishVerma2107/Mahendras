using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ImageSlider.Model;
using ImageSlider.MyTest;
using Refit;

namespace ImageSlider.Connection
{
    [Headers("User-Agent: :request:")]
    interface ApiConnectionForTestPackage
    {
        [Post("/v1/app/getusertestgivenpackagewise")]
        Task<UserTestGivenPackageWise> Getusertestgivenpackagewise([Body] List<UserTestPostObject> userTestPostObject);

        [Get("/v1/app/activetestlist/Exam")]
        Task<AllTestModel> Activetestlist();

        [Get("/v1/app/activetestlist/Practice")]
        Task<AllTestModel> ActivetestlistMock();

        [Get("/v1/app/getgiventests/{UserId}/{Duration}")]
        Task<AllTestModel> GetGivenTest(string UserId,string Duration);

        [Get("/v1/app/getstinfo/{TestID}")]
        Task<TestInfoModal> GetSTInfo(string TestID);

        [Get("/v1/app/getstbundle/{TestId}/{langcode}")]
        Task<string> GetStBundle(string TestId, string langcode);

        [Get("/v1/app/getstsummery/{UserId}/{TestId}")]
        Task<TestSummaryModel> GetTestSummary(string UserId, string TestId);

        [Post("/v1/app/submittestdata")]
        Task<string> SubmitTestRecord([Body] SubmitTestData submittestdata);
    }
}