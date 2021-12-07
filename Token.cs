using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkControllerAdmin
{
    static class Token
    {
        static private string token;
        static public string AcceptToken { get=> token; set=>token =value; }
    }
}
