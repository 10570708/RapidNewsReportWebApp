using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Services;
using RapidNewsReportWebApp.Models;
using System.Security.Claims;
using RapidNewsReportWebApp.Common;

namespace RapidNewsReportWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly NewsReportAPIClient _newsReportApiClient;

        public IndexModel(NewsReportAPIClient newsReportApiClient)
        {
            _newsReportApiClient = newsReportApiClient;
        }


        // OnGet handler for Index Page
        // Load Reports and return Page
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var reports = await _newsReportApiClient.GetReports();
                if (reports != null) Reports = reports;
            }
            catch (Exception e)
            {
                errorRMessage = ReportErrorHandler.GetErrorMessages(e.Message);
            }

            return Page();

        }


        // OnPost handler for Index Page - used to post filter parameters for Report listings 
        // Load Reports by search criteria and return Page
        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (viewAll)
                {
                    Reports = await _newsReportApiClient.GetReportsbyCategory(viewCategory, viewDesc);

                }
                else
                {
                    if (viewCategory == 0)
                    {
                        Reports = await _newsReportApiClient.GetReportsbyUser(createdBy, viewDesc);
                    }
                    else
                    {
                        Reports = await _newsReportApiClient.GetReportsbyUserCategory(createdBy, viewCategory, viewDesc);
                    }
                }
            }
            catch (Exception e)
            {
                errorRMessage = ReportErrorHandler.GetErrorMessages(e.Message);
            }

            return Page();


        }


        // OnPostDelete handler for Index Page - used to delete a Report and re-render Index Page
        public async Task<IActionResult> OnPostDelete(int id)
        {
            try
            {
                bool success = await _newsReportApiClient.DeleteReport(id);
                var reports = await _newsReportApiClient.GetReports();
                if (reports != null) Reports = reports;

                if (!success)
                {
                    errorRMessage = "There was a problem deleting your News Article.";
                }
                else
                {
                    infoRMessage = "Your News Article has been deleted.";
                }
            }
            catch (Exception e)
            {
                errorRMessage = ReportErrorHandler.GetErrorMessages(e.Message);
            }

            return Page();
        }


   
        public string? responseContent { get; set; }

        public String UserId { get; set; } = Guid.NewGuid().ToString();


        [BindProperty]
        public string? errorRMessage { get; set; }
        [BindProperty]
        public string? infoRMessage { get; set; }


        [BindProperty]
        public int viewCategory { get; set; } = 0;

        [BindProperty]
        public Guid createdBy { get; set; }

        [BindProperty]
        public bool viewAll { get; set; } = true;

        [BindProperty]
        public bool viewDesc { get; set; } = true;


        [BindProperty]
        public IEnumerable<Report> Reports { get; set; } = Enumerable.Empty<Report>();
        [BindProperty]
        public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();

    }
}