using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Models;
using RapidNewsReportWebApp.Services;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RapidNewsReportWebApp.Pages.Reports
{
    public class NewModel : PageModel
    {
        private readonly NewsReportAPIClient _newsReportApiClient;

        public NewModel(NewsReportAPIClient newsReportApiClient)
        {
            _newsReportApiClient = newsReportApiClient;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
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
        public enum CategoryType
        {
            [Display(Name = "Local News")]
            LocalNews = 0,
            [Display(Name = "World News")]
            WorldNews = 1,
            Sport = 2,
            Entertainment = 3,
            Weather = 4,
            Politics = 5,
            Opinion = 6,
            [Display(Name = "Food & Drink")]
            FoodDrink = 7,
        }
    }
}
