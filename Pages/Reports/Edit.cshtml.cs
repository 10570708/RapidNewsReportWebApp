using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Services;
using RapidNewsReportWebApp.Models;
using System.ComponentModel.DataAnnotations;



namespace RapidNewsReportWebApp.Pages.Reports
{
    public class EditModel : PageModel
    {
 	[TempData]   
 	public string FormResult { get; set; }

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
            reportId = myReport.Id;
            try 
            {
	            bool success = await _newsReportApiClient.PutReport(myReport);
		    if (!success)
		    {
			return Page();
		    }
		    else
		    {
            		FormResult = "Your new Report has been updated successfully.";
			return RedirectToPage("Index", new { ID = myReport.Id });
		    }
	    }
	    catch (Exception e)
	    {
            	FormResult = "There was an error writing your report.";	    	
		return Page();                
	    }
        }

	public string tester {get; set; }
        public int reportId { get; set; }
        
        //public Report myReport { get; set; }

        [BindProperty]
        public Report myReport { get; set; }
        public enum CategoryType
        {
            [Display(Name = "Local News")]
            LocalNews = 1,
            [Display(Name = "World News")]
            WorldNews = 2,
            Sport = 3,
            Entertainment = 4,
            Weather = 5,
            Politics = 6,
            Opinion = 7,
            [Display(Name = "Food & Drink")]
            FoodDrink = 8
        }



    }


}



