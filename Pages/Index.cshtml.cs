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
            /*
             using var client = new HttpClient();

             client.BaseAddress = new Uri("https://localhost:7166/");
             client.DefaultRequestHeaders.Add("Accept","application/json");


             HttpResponseMessage response = await client.GetAsync("/api/Reports/1");
             if (response.IsSuccessStatusCode)
             {
                 errorCMessage = await response.Content.ReadAsStringAsync();
                 //Report myreport = JsonConvert.DeserializeObject<Report>(errorCMessage);
             }


             foreach (var rep in await _newsReportApiClient.GetTodos())
             {
                 errorRMessage += "Adding error msg ... ";
             }


             Report report = await client.GetFromJsonAsync<Report>("api/Reports/1");

            */
            Reports =  await _newsReportApiClient.GetTodos();
            Comments = await _newsCommentApiClient.GetCommentList();

            //Reports.Append(report);

            return Page();

        }

        
        public string errorRMessage { get; set; }  = "Test";
        
        public string errorCMessage { get; set; }  = "";
        public IEnumerable<Report> Reports { get; set; } = Enumerable.Empty<Report>();
        public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();

    }
}