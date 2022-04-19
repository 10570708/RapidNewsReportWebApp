using RapidNewsReportWebApp.Models;
using System.Collections;

namespace RapidNewsReportWebApp.Services
{
    public class NewsReportAPIClient
    {
        public HttpClient Client { get; set; }

        public NewsReportAPIClient(HttpClient client)
        {
            client.BaseAddress = new System.Uri("https://localhost:7166/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client = client;
        }


	// Get Single Report
	// Http Get to api/Reports/{id}


        public async Task<Report> GetReport(int ID)
        {
            string myPath = $"api/Reports/{ID}";
            return await Client.GetFromJsonAsync<Report>(myPath);
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
		throw new Exception(code.ToString()+" - testing return code  ");	    
	    }
	    else
	    {
	            return response.IsSuccessStatusCode;
	    }
            
        }

	// Delete Report
	// Http Delete to api/Reports/{id} 

        public async Task<bool> DeleteReport(int id)
        {
            string myPath = $"api/Reports/{id}";
            var response = await Client.DeleteAsync(myPath);
            return response.IsSuccessStatusCode;
        }


	// Get reports by category
	// Http Get to api/Reports passing category

        public async Task<IEnumerable<Report>> GetReportsbyCategory(int category, bool desc)
        {
            string myPath = $"api/Reports?category={category}&desc={desc}";
        
            return await Client.GetFromJsonAsync<IEnumerable<Report>>(myPath);

        }


	// Get reports by user
	// Http Get to api/Reports passing guid

        public async Task<IEnumerable<Report>> GetReportsbyUser(Guid id, bool desc)
        {
            string myPath = $"api/Reports?guid={id}&desc={desc}";
        
            return await Client.GetFromJsonAsync<IEnumerable<Report>>(myPath);

        }


	// Get reports by user and category
	// Http Get to api/Reports passing guid and category parameters

        public async Task<IEnumerable<Report>> GetReportsbyUserCategory(Guid id, int category, bool desc)
        {
            string myPath = $"api/Reports?guid={id}&category={category}&desc={desc}";
        
            return await Client.GetFromJsonAsync<IEnumerable<Report>>(myPath);

        }



	// Get full lit of reports 
	// http get to /api/Reports 
	
        public async Task<IEnumerable<Report>> GetReports()
        {
            var result = await Client.GetAsync("/api/Reports");

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<IEnumerable<Report>>();
            }
            else
            {
                string msg = await result.Content.ReadAsStringAsync();
                var code = (int)result.StatusCode;
                throw new Exception(code.ToString() + msg);
            }
        }

    }
}
