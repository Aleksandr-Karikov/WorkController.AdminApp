using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkController.Common.Http.Helper;
using WorkController.Common.Http.Helper.ApiHelper;

namespace WorkController.Admin.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        internal async Task<IEnumerable<Time>> GetTimes(IHttpClientFactory factory, string Token)
        {

            var response = await RequestHelper.SendPostAuthRequest(ApiHelperUri.GetTimesUri + $"?ID={ID}", factory, Token);
            var content = await response.Content.ReadAsStringAsync();
            var rezult = JsonConvert.DeserializeObject<List<Time>>(content);
            return rezult;
        }
    }
}
