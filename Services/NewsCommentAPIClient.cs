using RapidNewsReportWebApp.Models;

namespace RapidNewsReportWebApp.Services
{
    public class NewsCommentAPIClient
    {
        public HttpClient Client { get; set; }

        public NewsCommentAPIClient(HttpClient client)
        {

            client.BaseAddress = new System.Uri("https://localhost:7061/");

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;
        }

        public async Task<IEnumerable<Comment>> GetCommentList()
        {
            return await Client.GetFromJsonAsync<IEnumerable<Comment>>("api/Comments");
        }
    }
}

