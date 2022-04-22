using RapidNewsReportWebApp.Models;
using Microsoft.Extensions.Configuration;

namespace RapidNewsReportWebApp.Services
{
    public class NewsCommentAPIClient
    {
        private IConfiguration configuration;

        public HttpClient Client { get; set; }

        public NewsCommentAPIClient(HttpClient client, IConfiguration iConfig)
        {
            // Get Comment API URL from configuration settings for http client
            configuration = iConfig;
            String strBase = iConfig.GetValue<string>("CommentAPI");
            client.BaseAddress = new System.Uri(strBase);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client = client;
        }


        // Get list of Comments by ReportId and optional sort order 
        // HttpClient Get to /api/Comments passing param int Id and bool desc 
        public async Task<IEnumerable<Comment>> GetComments(int Id, bool desc = true)
        {
            string path = $"api/Comments?reportId={Id}&desc={desc}";
            return await Client.GetFromJsonAsync<IEnumerable<Comment>>(path);
        }

        // Get list of Comments by ReportId, User and optional sort order 
        // HttpClient Get to /api/Comments passing param int Id, Guid createdBy and bool desc 
        public async Task<IEnumerable<Comment>> GetComments(int Id, Guid createdBy, bool desc = true)
        {
            string path = $"api/Comments?reportId={Id}&createdBy={createdBy}&desc={desc}";
            return await Client.GetFromJsonAsync<IEnumerable<Comment>>(path);
        }

        // Add a Comment
        // HttpClient Post call to Comment API
        public async Task<bool> PostComment(Comment myComment)
        {
            var response = await Client.PostAsJsonAsync<Comment>("api/Comments", myComment);
            return response.IsSuccessStatusCode;
        }


        // Update Comment
        // HttpClient Put to api/Comments/{id} 
        public async Task<bool> PutComment(Comment myComment)
        {

            string myPath = $"api/Comments/{myComment.Id}";

            //string myPath = $"api/Reports/44";
            var response = await Client.PutAsJsonAsync<Comment>(myPath, myComment);

            if (!response.IsSuccessStatusCode)
            {
                var code = (int)response.StatusCode;
                throw new Exception(code.ToString() + " - testing return code  ");
            }
            else
            {
                return response.IsSuccessStatusCode;
            }

        }


        // Delete Comment by Comment Id
        // Http Delete to api/Comments/{id} 
        public async Task<bool> DeleteComment(int id)
        {
            string myPath = $"api/Comments/{id}";
            var response = await Client.DeleteAsync(myPath);
            return response.IsSuccessStatusCode;
        }

        // Delete All Comments for a Report 
        // Http Delete to api/Comments/Reports/{id} 
        public async Task<bool> DeleteComments(int reportId)
        {
            string myPath = $"api/Comments/Reports/{reportId}";
            var response = await Client.DeleteAsync(myPath);
            return response.IsSuccessStatusCode;
        }

    }
}

