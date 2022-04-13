using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Services;
using RapidNewsReportWebApp.Models;


namespace RapidNewsReportWebApp.Pages.Reports
{
    public class EditModel : PageModel
    {

        private readonly NewsReportAPIClient _newsReportApiClient;
        private readonly NewsCommentAPIClient _newsCommentApiClient;

        public EditModel(NewsReportAPIClient newsReportApiClient, NewsCommentAPIClient newsCommentApiClient)
        {
            _newsReportApiClient = newsReportApiClient;
            _newsCommentApiClient = newsCommentApiClient;
        }


        public string responseContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int ID)
        {

            myReport = await _newsReportApiClient.GetReport(ID);
            reportId = ID;
            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            bool success = await _newsReportApiClient.PutReport(myReport);

            if (!success)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("../Index");
            }

        }

        public int reportId { get; set; }
        
        //public Report myReport { get; set; }

        [BindProperty]
        public Report myReport { get; set; }
        public enum CategoryType
        {
            LocalNews = 0,
            WorldNews = 1,
            Sport = 2,
            Entertainment = 3,
            Weather = 4,
            Politics = 5,
            Opinion = 6,
            FoodDrink = 7,
        }



    }


}



