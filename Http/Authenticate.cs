using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkControllerAdmin.Http.RequstModels;

namespace WorkControllerAdmin.Http
{
    class Authenticate
    {
        private readonly IHttpClientFactory _clientFactory;
        public Authenticate(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task OnPost()
        {
            var payload = new Register
            {
                Email="test@mail.ru",
                Password="test",
                FirstName="sanya",
                LastName="karikov",
                IsAdmin = true
                
            };

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post,
                "User/register");

            var client = _clientFactory.CreateClient("WorkController");

            var response = await client.PostAsync(request.RequestUri, httpContent);
            MessageBox.Show(await response.Content.ReadAsStringAsync());
            //StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            //txtBlock.Text = readStream.ReadToEnd();
            //if (response.IsSuccessStatusCode)
            //{
            //    using var responseStream = await response.Content.ReadAsStreamAsync();
            //    PullRequests = await JsonSerializer.DeserializeAsync
            //            <IEnumerable<GitHubPullRequest>>(responseStream);
            //}
            //else
            //{
            //    GetPullRequestsError = true;
            //    PullRequests = Array.Empty<GitHubPullRequest>();
            //}
        }
    }
}
