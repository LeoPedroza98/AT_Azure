using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AzureWeb.MVC.ApiClientHelperCountryState
{
    public class ApiClientCountryState
    {
        public HttpClient Client { get; set; }

        public ApiClientCountryState()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44307/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
