using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkControllerAdmin.Http.RequstModels;

namespace WorkControllerAdmin.Http.Helper
{
    static class RequestHelper
    {
        public static async HttpContent GetNewHttpContent(IRequest request)
        {
            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        }
    }
}
