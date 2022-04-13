using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Services;
using RapidNewsReportWebApp.Models;


namespace RapidNewsReportWebApp.Pages
{
    public class IndexModel : PageModel
    {

        private readonly NewsReportAPIClient _newsReportApiClient;
        private readonly NewsCommentAPIClient _newsCommentApiClient;

        public IndexModel(NewsReportAPIClient newsReportApiClient, NewsCommentAPIClient newsCommentApiClient)
        {
            _newsReportApiClient = newsReportApiClient;
            _newsCommentApiClient = newsCommentApiClient;
        }

        public string responseContent { get; set;  }



        public async Task<IActionResult> OnGetAsync()
        {
            Reports =  await _newsReportApiClient.GetReports();
            Comments = await _newsCommentApiClient.GetCommentList();

            return Page();

        }
        

        public async Task<IActionResult> OnPost()
        {
            errorRMessage = "Submitted and value is  " + viewCategory.ToString();


            Reports =  await _newsReportApiClient.GetReportsbyCategory(viewCategory);
            Comments = await _newsCommentApiClient.GetCommentList();


            return Page();
        }






        public async Task<IActionResult> OnPostDelete(int id)
        {
            errorRMessage = "Article Deleted " + id.ToString();

            bool success = await _newsReportApiClient.DeleteReport(id);

            Reports =  await _newsReportApiClient.GetReports();
            Comments = await _newsCommentApiClient.GetCommentList();


            if (!success)
            {
                errorRMessage = "Failed to Delete " + id.ToString();
            }
            else
            {
                errorRMessage = "Article Deleted " + id.ToString();            
                return Page();
            }
            

            Reports =  await _newsReportApiClient.GetReports();
            Comments = await _newsCommentApiClient.GetCommentList();

            return Page();
        }


        
        public string errorRMessage { get; set; }  = "Test";
        

	[BindProperty]        
	public int viewCategory {get; set; } = 0;

        public string errorCMessage { get; set; }  = "";
        public IEnumerable<Report> Reports { get; set; } = Enumerable.Empty<Report>();
        public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();

    }
}