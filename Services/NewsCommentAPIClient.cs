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
        
        public async Task<IEnumerable<Comment>> GetComments(int Id, bool desc = true)
        {
            string path = $"api/Comments?reportId={Id}&desc={desc}";
            return await Client.GetFromJsonAsync<IEnumerable<Comment>>(path);
        }

        public async Task<IEnumerable<Comment>> GetComments(int Id, Guid createdBy, bool desc = true)
        {
            string path = $"api/Comments?reportId={Id}&createdBy={createdBy}&desc={desc}";
            return await Client.GetFromJsonAsync<IEnumerable<Comment>>(path);
        }



        public async Task<bool> PostComment(Comment myComment)
        {
            var response = await Client.PostAsJsonAsync<Comment>("api/Comments", myComment);
            return response.IsSuccessStatusCode;
        }


	// Update Comment
	// Http Put to api/Comments/{id} 

        public async Task<bool> PutComment(Comment myComment)
        {

            string myPath = $"api/Comments/{myComment.Id}";
            
            //string myPath = $"api/Reports/44";
            var response = await Client.PutAsJsonAsync<Comment>(myPath, myComment);

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

	// Delete Comment
	// Http Delete to api/Comments/{id} 

        public async Task<bool> DeleteComment(int id)
        {
            string myPath = $"api/Comments/{id}";
            var response = await Client.DeleteAsync(myPath);
            return response.IsSuccessStatusCode;
        }        
        
    }
}

