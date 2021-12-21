using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkControllerAdmin.Http.RequstModels.Base;

namespace WorkController.Admin.Http.RequstModels
{
    internal class SetPeriod:BaseRequest
    {
        [JsonProperty("Time")]
        public int Time { get; set; }
        [JsonProperty("userId")]
        public int id { get; set; }
    }
}
