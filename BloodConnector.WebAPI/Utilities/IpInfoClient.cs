using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace BloodConnector.WebAPI.Utilities
{
    public class IpInfoClient : HttpClient
    {
        public IpInfoClient()
        {
            BaseAddress = new Uri(@"https://ipinfo.io");
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}