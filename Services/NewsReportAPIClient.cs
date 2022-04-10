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

        public async Task<IEnumerable<Report>> GetReportList()
        {
            return await Client.GetFromJsonAsync<IEnumerable<Report>>("api/Reports/1");
        }

        public async Task<Report> GetReport(int ID)
        {
            string myPath = $"api/Reports/{ID}";
            return await Client.GetFromJsonAsync<Report>(myPath);
        }


        public async Task<bool> PostReport(PReport myReport)
        {
            var response = await Client.PostAsJsonAsync<PReport>("api/Reports", myReport);
            return response.IsSuccessStatusCode;
        }


        public async Task<IEnumerable<Report>> GetTodos()
        {
            return await Client.GetFromJsonAsync<IEnumerable<Report>>("api/Reports");
/*            var response = await Client.GetAsync("api/Reports");

            response.EnsureSuccessStatusCode();
            string data = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<List<Report>>(data);
*/
        }

        public async Task<string> GetReportListAsString()
        {
            return await Client.GetStringAsync("/api/Reports");
        }

    }
}
