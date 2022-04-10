using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Models;
using RapidNewsReportWebApp.Services;

namespace RapidNewsReportWebApp.Pages.Reports
{
    public class NewModel : PageModel
    {
        private readonly NewsReportAPIClient _newsReportApiClient;
        public NewModel(NewsReportAPIClient newsReportApiClient)
        {
            _newsReportApiClient = newsReportApiClient;
        }
        public async Task<IActionResult> OnPost()
        {
            newReport.Category = 0;

            bool success = await _newsReportApiClient.PostReport(newReport);

            if (!success)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("../Index");
            }
            
        }

        [BindProperty]
        public PReport newReport { get; set; }
    }
}
