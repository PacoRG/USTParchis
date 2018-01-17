using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Tests.Utils
{
    public static class HttpClientExtension
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, object obj, string url)
        {
            var jsonInString = JsonConvert.SerializeObject(obj);

            return  client.PostAsync(url, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string jsonInString, string url)
        {
            return client.PostAsync(url, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
        }
    }
}
