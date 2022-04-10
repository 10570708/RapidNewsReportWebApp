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

        public int reportId { get; set; }
        
        public Report myReport { get; set; }
    }
}



