using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Services;
using RapidNewsReportWebApp.Models;
using System.Security.Claims;

namespace RapidNewsReportWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public String UserId { get; set; } = Guid.NewGuid().ToString();

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
            try
            {
                Reports = await _newsReportApiClient.GetReports();
            }
            catch (Exception e)
            {
                 if (e.Message.Contains("No connection"))
                {
                    errorRMessage = "The Report Service is not running. Please try again.";
                }
                else
                {
                	errorRMessage = e.Message;                
               	}
            }
            Comments = await _newsCommentApiClient.GetCommentList();

            return Page();

        }
        

        public async Task<IActionResult> OnPost()
        {
            //errorRMessage = "Submitted and value is  " + viewCategory.ToString() + " and user is "  + createdBy.ToString() + " and viel all ";

	        if (viewAll)
	        {
	    	    //errorRMessage += "viewing all ...";
	    	    
            	Reports =  await _newsReportApiClient.GetReportsbyCategory(viewCategory, viewDesc);
            }
            else
            {
            	if (viewCategory == 0)
            	{
            		Reports =  await _newsReportApiClient.GetReportsbyUser(createdBy, viewDesc);
            	}
            	else
            	{
            		Reports =  await _newsReportApiClient.GetReportsbyUserCategory(createdBy,viewCategory, viewDesc);
            	}
            }
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
            
            return Page();
        }


        [BindProperty]
        public string errorRMessage { get; set; }
        

	    [BindProperty]        
	    public int viewCategory {get; set; } = 0;
	
	    [BindProperty]    
	    public Guid createdBy {get; set; }

        [BindProperty]
        public bool viewAll { get; set; } = true;
        
        [BindProperty]
        public bool viewDesc { get; set; } = true;


        [BindProperty]
        public string errorCMessage { get; set; }  = "";
        [BindProperty]
        public IEnumerable<Report> Reports { get; set; } = Enumerable.Empty<Report>();
        [BindProperty]
        public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();

    }
}