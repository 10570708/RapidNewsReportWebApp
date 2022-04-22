using RapidNewsReportWebApp.Models;
using System.Collections;
using Microsoft.Extensions.Configuration;

namespace RapidNewsReportWebApp.Services
{
    public class NewsReportAPIClient
    {
        private IConfiguration configuration;

        public HttpClient Client { get; set; }

        public NewsReportAPIClient(HttpClient client, IConfiguration iConfig)
        {

            // Read in config setting for ReportAPI base url for the httpclient
            String strBase = iConfig.GetValue<string>("ReportAPI");
            client.BaseAddress = new System.Uri(strBase);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client = client;
        }


        // Get Single Report
        // Http Get to api/Reports/{id}

        public async Task<Report> GetReport(int ID)
        {
            string myPath = $"api/Reports/{ID}";

            var myReport = await Client.GetFromJsonAsync<Report>(myPath);

            if (myReport == null)
            {
                throw new Exception("The Report could not be found.");
            }
            else
            {
                return myReport;
            }
        }



        // Create Report
        // Http Post to api/Reports passing Report
        
        public async Task<bool> PostReport(Report myReport)
        {
            var response = await Client.PostAsJsonAsync<Report>("api/Reports", myReport);
            return response.IsSuccessStatusCode;
        }



        // Update Report
        // Http Put to api/Reports/{id} 
        
        public async Task<bool> PutReport(Report myReport)
        {

            string myPath = $"api/Reports/{myReport.Id}";

            //string myPath = $"api/Reports/44";
            var response = await Client.PutAsJsonAsync<Report>(myPath, myReport);

            if (!response.IsSuccessStatusCode)
            {
                var code = (int)response.StatusCode;
                throw new Exception(code.ToString());
            }
            else
            {
                return response.IsSuccessStatusCode;
            }

        }


        
        // Publish a Report
        // Http Put to api/Reports/Publish/{ID} 
        
        public async Task<bool> PublishReport(int ID)
        {
            string myPath = $"api/Reports/Publish/{ID}";

            //string myPath = $"api/Reports/44";
            var response = await Client.PutAsJsonAsync(myPath, ID);

            if (!response.IsSuccessStatusCode)
            {
                var code = (int)response.StatusCode;
                throw new Exception(code.ToString());
            }
            else
            {
                return response.IsSuccessStatusCode;
            }
        }


        
        // Delete a Report
        // Http Delete to api/Reports/{id} 
        
        public async Task<bool> DeleteReport(int id)
        {
            string myPath = $"api/Reports/{id}";
            var response = await Client.DeleteAsync(myPath);
            return response.IsSuccessStatusCode;
        }


        
        // Get Reports by category
        // Http Get to api/Reports passing category(search criteria) and bool(sort order)
        
        public async Task<IEnumerable<Report>> GetReportsbyCategory(int category, bool desc)
        {

            string myPath = $"api/Reports?category={category}&desc={desc}";

            var reports = await Client.GetFromJsonAsync<IEnumerable<Report>>(myPath);

            return (reports == null ? Enumerable.Empty<Report>() : reports);
        }


        
        // Get Reports by User 
        // Http Get to api/Reports passing guid
        
        public async Task<IEnumerable<Report>> GetReportsbyUser(Guid id, bool desc)
        {
            string myPath = $"api/Reports?guid={id}&desc={desc}";

            var reports = await Client.GetFromJsonAsync<IEnumerable<Report>>(myPath);

            return (reports == null ? Enumerable.Empty<Report>() : reports);

        }


        
        // Get reports by user and category and sort order 
        // Http Get to api/Reports passing guid, category and sort desc parameters
        
        public async Task<IEnumerable<Report>> GetReportsbyUserCategory(Guid id, int category, bool desc)
        {
            string myPath = $"api/Reports?guid={id}&category={category}&desc={desc}";

            var reports = await Client.GetFromJsonAsync<IEnumerable<Report>>(myPath);

            return (reports == null ? Enumerable.Empty<Report>() : reports);

        }



        // Get full lit of reports 
        // http get to /api/Reports 
        
        public async Task<IEnumerable<Report>> GetReports()
        {
            var result = await Client.GetAsync("/api/Reports");

            if (result.IsSuccessStatusCode)
            {
                var reports = await result.Content.ReadFromJsonAsync<IEnumerable<Report>>();
                return (reports == null ? Enumerable.Empty<Report>() : reports);
            }
            else
            {
                string msg = await result.Content.ReadAsStringAsync();
                var code = (int)result.StatusCode;
                throw new Exception("["+code.ToString()+"]" + msg);
            }
        }
    }
}
