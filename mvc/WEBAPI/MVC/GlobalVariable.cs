using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace MVC
{
    public static class GlobalVariable
    {
        public static HttpClient WebApiClient = new HttpClient();
        static GlobalVariable()
        {
            WebApiClient.BaseAddress = new Uri("https://localhost:44392/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applicatiob/json"));

        }
    }
}