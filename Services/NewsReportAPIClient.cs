﻿using RapidNewsReportWebApp.Models;
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

        public async Task<IEnumerable<Report>> GetReportList()
        {
            return await Client.GetFromJsonAsync<IEnumerable<Report>>("api/Reports/1");
        }

        public async Task<Report> GetReport(int ID)
        {
            string myPath = $"api/Reports/{ID}";
            return await Client.GetFromJsonAsync<Report>(myPath);
        }


        public async Task<bool> PostReport(Report myReport)
        {
            var response = await Client.PostAsJsonAsync<Report>("api/Reports", myReport);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> PutReport(Report myReport)
        {
            string myPath = $"api/Reports/{myReport.Id}";
            var response = await Client.PutAsJsonAsync<Report>(myPath, myReport);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> DeleteReport(int id)
        {
            string myPath = $"api/Reports/{id}";
            var response = await Client.DeleteAsync(myPath);
            return response.IsSuccessStatusCode;
        }



        public async Task<IEnumerable<Report>> GetReportsbyCategory(int category)
        {
            string myPath = $"api/Reports?category={category}";
        
            return await Client.GetFromJsonAsync<IEnumerable<Report>>(myPath);

        }


        public async Task<IEnumerable<Report>> GetReportsbyUser(Guid id)
        {
            string myPath = $"api/Reports?guid={id}";
        
            return await Client.GetFromJsonAsync<IEnumerable<Report>>(myPath);

        }



        public async Task<IEnumerable<Report>> GetReportsbyUserCategory(Guid id, int category)
        {
            string myPath = $"api/Reports?guid={id}&category={category}";
        
            return await Client.GetFromJsonAsync<IEnumerable<Report>>(myPath);

        }



        public async Task<IEnumerable<Report>> GetReports()
        {
            return await Client.GetFromJsonAsync<IEnumerable<Report>>("api/Reports");
/*            var response = await Client.GetAsync("api/Reports");

            response.EnsureSuccessStatusCode();
            string data = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<List<Report>>(data);
*/
        }


    }
}
