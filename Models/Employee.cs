using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorkController.Admin.Http.RequstModels;
using WorkController.Common.Helper;
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
        public int MoneyPerHour { get; set; }
        public int ScreenShotPeriod { get; set; }
        public async Task<List<byte[]>> GetScreenshots(IHttpClientFactory factory, string Token,DateTime date)
        {

            var response = await RequestHelper.SendPostAuthRequest(ApiHelperUri.GetScreens, factory, Token, new GetScreensRequest() { Date = date.Date, UserId = ID });

            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<List<byte[]>>(content);
            }
            catch
            {
                return null;
            }
           
        }
        internal async Task<IEnumerable<Time>> GetTimes(IHttpClientFactory factory, string Token)
        {

            var response = await RequestHelper.SendPostAuthRequest(ApiHelperUri.GetTimesUri + $"?ID={ID}", factory, Token);
            var content = await response.Content.ReadAsStringAsync();
            var rezult = JsonConvert.DeserializeObject<List<Time>>(content);
            foreach (var record in rezult)
            {
                var time = TimeSpan.FromMilliseconds(record.Milleseconds);
                record.TimeString = String.Format("{0:00}:{1:00}:{2:00}",
            time.Hours, time.Minutes, time.Seconds);
            }
            return rezult;
        }
        internal async Task SetMoney(IHttpClientFactory factory, string Token,int money)
        {
            await RequestHelper.SendPostAuthRequest(ApiHelperUri.SetMoney, factory, Token, new SetMoney() { id = ID, Money = money });
        }
        internal async Task SetPeriod(IHttpClientFactory factory, string Token, int time)
        {
            await RequestHelper.SendPostAuthRequest(ApiHelperUri.SetPeriod, factory, Token, new SetPeriod() { id = ID, Time = time });
        }
    }
}
