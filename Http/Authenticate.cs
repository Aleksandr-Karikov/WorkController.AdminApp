using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkControllerAdmin.Http.Helper;
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
            var request = new HttpRequestMessage(HttpMethod.Post,
                "User/register");
            var client = _clientFactory.CreateClient("WorkController");
            var response = await client.PostAsync(request.RequestUri, RequestHelper.GetNewHttpContent(payload));
            MessageBox.Show(await response.Content.ReadAsStringAsync());
            
        }
    }
}
