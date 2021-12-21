using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkControllerAdmin.Http.RequstModels.Base;

namespace WorkController.Admin.Http.RequstModels
{
    internal class SetMoney:BaseRequest
    {
        [JsonProperty("money")]
        public int Money { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
    }
}
