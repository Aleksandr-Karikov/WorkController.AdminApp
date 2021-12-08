using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkControllerAdmin.Http.RequstModels.Base
{
    internal class BaseRequest
    {
        [JsonProperty("isAdmin")]
        public bool IsAdmin = true;
    }
}
