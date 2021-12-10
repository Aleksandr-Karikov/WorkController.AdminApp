using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkControllerAdmin.Http.RequstModels;
using WorkControllerAdmin.Http.RequstModels.Base;

namespace WorkControllerAdmin.Http.Helper
{
    static class RequestHelper
    {
        private static async Task<HttpContent> GetNewHttpContent(BaseRequest request)
        {
            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            return new StringContent(stringPayload, Encoding.UTF8, "application/json");
        }
        public static async Task<HttpResponseMessage> SendPostRequest(string URI, IHttpClientFactory _clientFactory, BaseRequest requestModel)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, URI);
             var client = _clientFactory.CreateClient("WorkController");
            return await client.PostAsync(request.RequestUri, await RequestHelper.GetNewHttpContent(requestModel));
        }
    }
}
