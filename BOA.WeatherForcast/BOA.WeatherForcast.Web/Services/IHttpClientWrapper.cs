using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BOA.WeatherForcast.Web.Services
{
    public interface IHttpClientWrapper : IDisposable
    {
        HttpResponseMessage Get(string url);
        Task<HttpResponseMessage> GetAsync(string url);
        HttpResponseMessage Post(string url, HttpContent content);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
    }
}