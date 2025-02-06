using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtils
{
    public interface IHttpRequest
    {
        Task<string> GetAsync(string url);

        Task<TResult> GetAsync<TResult>(string url, Dictionary<string, string> urlParam = null);

        Task<string> PostAsync(string url, object input);

        Task<TResult> PostAsync<TResult>(string url, object input, Dictionary<string, string> urlParam = null);

        Task<TResult> PostAsync<TResult>(string url, MultipartFormDataContent input, Dictionary<string, string> urlParam = null);

        Task<TResult> PatchAsync<TResult>(string url, MultipartFormDataContent input, Dictionary<string, string> urlParam = null);

        Task<TResult> PatchAsync<TResult>(string url, object input, Dictionary<string, string> urlParam = null);

        Task<string> PutAsync(string url, object input);

        Task<TResult> PutAsync<TResult>(string url, object input, Dictionary<string, string> urlParam = null);

        Task<string> PutAsync(string url, HttpContent content);

        Task<string> DeleteAsync(string url);

        String BaseUrl { set; }
        String Token { set; get; }
    }
}