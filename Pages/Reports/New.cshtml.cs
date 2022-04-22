using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Models;
using RapidNewsReportWebApp.Services;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using RapidNewsReportWebApp.Common;

namespace RapidNewsReportWebApp.Pages.Reports
{
    public class NewModel : PageModel
    {
        [TempData]
        public string? FormResult { get; set; }



        private readonly NewsReportAPIClient _newsReportApiClient;

        public NewModel(NewsReportAPIClient newsReportApiClient)
        {
            _newsReportApiClient = newsReportApiClient;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost(Report newReport)
        {
            if (!ModelState.IsValid)
            {
                errorRMessage = "There were errors with the Report Details you entered.";
                return Page();
            }
            else
            {
                try
                {
                    bool success = await _newsReportApiClient.PostReport(newReport);

                    if (!success)
                    {
                        return Page();
                    }
                    else
                    {
                        FormResult = "Your new Report has been created successfully.";
                        return RedirectToPage("../Index");
                    }
                }
                catch (Exception ex)
                {
                    errorRMessage = ReportErrorHandler.GetErrorMessages(ex.Message);
                    return Page();
                }
            }
        }

        public string errorRMessage { get; set; } = "";

        [BindProperty]
        public Report newReport { get; set; }
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
