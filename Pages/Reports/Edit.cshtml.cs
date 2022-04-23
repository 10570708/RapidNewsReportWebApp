using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Services;
using RapidNewsReportWebApp.Models;
using System.ComponentModel.DataAnnotations;
using RapidNewsReportWebApp.Common;
using Microsoft.AspNetCore.Authorization;

namespace RapidNewsReportWebApp.Pages.Reports
{
    [Authorize]
    public class EditModel : PageModel
    {
        [TempData]
        public string? FormResult { get; set; }

        private readonly NewsReportAPIClient _newsReportApiClient;
        private readonly NewsCommentAPIClient _newsCommentApiClient;

        public EditModel(NewsReportAPIClient newsReportApiClient)
        {
            _newsReportApiClient = newsReportApiClient;
        }


        // OnGet handler for Edit Page
        // Loads the Report to be Edited in the Edit Page
        public async Task<IActionResult> OnGetAsync(int ID)
        {
            try
            {
                myReport = await _newsReportApiClient.GetReport(ID);
                reportId = ID;
                return Page();
            }
            catch (Exception ex)
            {
                FormResult = ReportErrorHandler.GetErrorMessages(ex.Message);
                return RedirectToPage("../Index");
            }
        }


        // OnPost handler for Edit Page
        // Calls Report API to save the updated Report
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorRMessage = "There were errors with the News Report Details you entered.";
                return Page();
            }
            else
            {
                reportId = myReport.Id;
                try
                {
                    bool success = await _newsReportApiClient.PutReport(myReport);
                    if (!success)
                    {
                        errorRMessage = "There was an error updating your News Report. ";
                        return Page();
                    }
                    else
                    {
                        FormResult = "Your News Report has been updated successfully.";
                        return RedirectToPage("Index", new { ID = myReport.Id });
                    }
                }
                catch (Exception ex)
                {
                    errorRMessage = "There was an error updating your News Report. " + ReportErrorHandler.GetErrorMessages(ex.Message);
                    return Page();
                }

            }

        }


        // OnPostPublish handler for Edit Page
        // Calls Report API to updated the IsPublished value for the Report

        public async Task<IActionResult> OnPostPublish(int ID)
        {
            try
            {
                bool success = await _newsReportApiClient.PublishReport(ID);
                if (!success)
                {
                    FormResult = "There was an error publishing your report.";
                }
                else
                {
                    FormResult = "Your News Report has been published successfully.";
                }
            }
            catch (Exception ex)
            {
                FormResult = "There was an error publishing your News Report. " + ReportErrorHandler.GetErrorMessages(ex.Message); 
            }

            return RedirectToPage("Index", new { ID = ID });

        }


        public int reportId { get; set; }

        public string errorRMessage { get; set; } = "";

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



