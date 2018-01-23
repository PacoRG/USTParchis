using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Tests.Utils
{
    public static class HttpClientExtension
    {

        public static Task<HttpResponseMessage> MakeRequestWithHeader(
            this HttpClient client,
            HttpMethod method,
            object content,
            string url,
            string language = null)
        {
            var jsonInString = JsonConvert.SerializeObject(content);
            var jsonContent = new System.Net.Http.StringContent(jsonInString, Encoding.UTF8, "application/json");

            var message = new HttpRequestMessage(method, url);
            message.Content = jsonContent;

            if (language != null)
            {
                message.Headers.Add("Accept-Language", language + ";q=1.0");
            }
            return client.SendAsync(message);

        }

        public static Task<HttpResponseMessage> MakeRequestWithCookie(
            this HttpClient client,
            HttpMethod method,
            object content,
            string url,
            string language = null)
        {

            var jsonInString = JsonConvert.SerializeObject(content);
            var jsonContent = new System.Net.Http.StringContent(jsonInString, Encoding.UTF8, "application/json");
      
            var message = new HttpRequestMessage(method, url);
            message.Content = jsonContent;

            if (language != null)
            {
                message.Headers.Add("Cookie", new CookieHeaderValue(".AspNetCore.Culture", "c=es|uic=es").ToString());
            }
            return client.SendAsync(message);

        }
    }
}
