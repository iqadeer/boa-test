using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BOA.WeatherForecast.Api.Services
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }

        public HttpResponseMessage Post(string url, HttpContent content)
        {
            return PostAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync(url);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            using (var client = new HttpClient())
            {
                return await client.PostAsync(url, content);
            }
        }
    }
}
