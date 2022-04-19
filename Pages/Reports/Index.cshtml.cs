using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Services;
using RapidNewsReportWebApp.Models;


namespace RapidNewsReportWebApp.Pages.Reports
{
    public class IndexModel : PageModel
    { 
    
 	[TempData]   
 	public string FormResult { get; set; }
 	
    

	public string Message { get; set; } = "Initial Request";
        private readonly NewsReportAPIClient _newsReportApiClient;
        private readonly NewsCommentAPIClient _newsCommentApiClient;

        public IndexModel(NewsReportAPIClient newsReportApiClient, NewsCommentAPIClient newsCommentApiClient)
        {
            _newsReportApiClient = newsReportApiClient;
            _newsCommentApiClient = newsCommentApiClient;
        }

        public string responseContent { get; set; }
        

    	public async Task<IActionResult> OnPostAddComment(int id, Guid createdBy)  
    	{  
            myComment.ReportId = id;
            myComment.CreatedBy = createdBy;
            
            bool success = await _newsCommentApiClient.PostComment(myComment);
            myReport = await _newsReportApiClient.GetReport(id);
	    Comments = await _newsCommentApiClient.GetComments(myReport.Id);            
	    progressMessage = "Your comment has been added";            
            return Page();
	}        


    	public async Task<IActionResult> OnPostPutComment(int id, int reportId, Guid createdBy)  
    	{  
            myComment.ReportId = reportId;
            myComment.CreatedBy = createdBy;
            myComment.Id = id;
            
            
            bool success = await _newsCommentApiClient.PutComment(myComment);
            myReport = await _newsReportApiClient.GetReport(reportId);
            Comments = await _newsCommentApiClient.GetComments(reportId);
	    progressMessage = "Your comment has been updated"; 
            return Page();
	}        


    	public async Task<IActionResult> OnPostViewComments(int id)  
    	{  
            Comments = await _newsCommentApiClient.GetComments(id);
            myReport = await _newsReportApiClient.GetReport(id);
            return Page();
	}        



    	public async Task<IActionResult> OnPostDelete(int id)  
    	{  
            bool success = await _newsReportApiClient.DeleteReport(id);

            if (!success)
            {
                return Page();
            }
            else
            {
		progressMessage = "Your Report has been successfully deleted.";            
                return RedirectToPage("../Index");
            }

	}        
	
	public async Task<IActionResult> OnPostDeleteComment(int id, int reportId)  
    	{  
            bool success = await _newsCommentApiClient.DeleteComment(id);

            if (!success)
            {
                return Page();
            }
            else
            {
		FormResult = "Your Comment has been successfully deleted.";            
		myReport = await _newsReportApiClient.GetReport(reportId);
		Comments = await _newsCommentApiClient.GetComments(reportId);
		progressMessage = "Your Comment has been successfully deleted."; 
                return Page();
            }

	}        	

 	public async Task<IActionResult> OnGetAsync(int ID)
        {
            myReport = await _newsReportApiClient.GetReport(ID);
            return Page();
        }
        

        public async Task<IActionResult> OnPost()
        {
	        if (viewAll)
	        {
	    	    
            	Comments =  await _newsCommentApiClient.GetComments(ID, viewDesc);
            }
            else
            {
            		Comments =  await _newsCommentApiClient.GetComments(ID, createdBy, viewDesc);
            }

	    myReport = await _newsReportApiClient.GetReport(ID);

            return Page();
        }


        [BindProperty]
        public string CommentText { get; set; }


        [BindProperty]
        public bool viewAll { get; set; } = true;
        
	[BindProperty]
        public int ID { get; set; }        
        
	[BindProperty]
        public Guid createdBy { get; set; }        


        [BindProperty]
        public bool viewDesc { get; set; } = true;
        

	[BindProperty]
	public Report myReport { get; set; }
	
	[BindProperty]
	public Comment myComment { get; set; }	

        public string errorRMessage { get; set; }
        
        public string progressMessage { get; set; }

        public string errorCMessage { get; set; } = "";
        public IEnumerable<Report> Reports { get; set; } = Enumerable.Empty<Report>();
        public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();

    }
}
