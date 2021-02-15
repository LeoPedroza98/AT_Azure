using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AzureWeb.MVC.ApiClientHelperPerson
{
    public class ApiClientPerson
    {
        public HttpClient Client { get; set; }

        public ApiClientPerson()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44304/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
