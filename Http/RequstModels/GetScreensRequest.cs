using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkControllerAdmin.Http.RequstModels.Base;

namespace WorkController.Admin.Http.RequstModels
{
    internal class GetScreensRequest: BaseRequest
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("Date")]
        public DateTime Date { get; set; }
    }
}
