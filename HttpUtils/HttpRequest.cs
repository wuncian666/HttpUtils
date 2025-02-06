using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtils
{
    internal class HttpRequest : IHttpRequest, IDisposable
    {
        private string _baseUrl = "";
        private readonly HttpClient _httpClient;
        private string _token = string.Empty;

        public string BaseUrl
        {
            get => _baseUrl;
            set
            {
                _baseUrl = NormalizeBaseUrl(value);
                _httpClient.BaseAddress = new Uri(_baseUrl);
            }
        }

        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                this.UpdateAuthorizationHeader();
            }
        }

        public HttpRequest()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> DeleteAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public void Dispose()
        {
            this._httpClient.Dispose();
        }

        public async Task<string> GetAsync(string url)
        {
            url = url.TrimStart('/');
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }

            return null;
        }

        public async Task<TResult> GetAsync<TResult>(string url, Dictionary<string, string> urlParam = null)
        {
            if (urlParam != null && urlParam.Count > 0)
            {
                var queryString = string.Join("&", urlParam.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
                url = $"{url}?{queryString}";
            }

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                TResult data = JsonConvert.DeserializeObject<TResult>(content);
                return data;
            }

            return default;
        }

        public async Task<TResult> PatchAsync<TResult>(string url, MultipartFormDataContent input, Dictionary<string, string> urlParam = null)
        {
            var request = new HttpRequestMessage(new HttpMethod("Patch"), url);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                TResult data = JsonConvert.DeserializeObject<TResult>(result);
                return data;
            }

            return default;
        }

        public async Task<TResult> PatchAsync<TResult>(string url, object input, Dictionary<string, string> urlParam = null)
        {
            if (urlParam != null && urlParam.Count > 0)
            {
                var queryString = string.Join("&", urlParam.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
                url = $"{url}?{queryString}";
            }
            var request = new HttpRequestMessage(new HttpMethod("Patch"), url);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(json);
            }
            return default;
        }

        public async Task<string> PostAsync(string url, object input)
        {
            var json = JsonConvert.SerializeObject(input);
            var content = new StringContent(json);
            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }

            return null;
        }

        public async Task<TResult> PostAsync<TResult>(string url, object input, Dictionary<string, string> urlParam = null)
        {
            if (urlParam != null && urlParam.Count > 0)
            {
                var queryString = string.Join("&", urlParam.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
                url = $"{url}?{queryString}";
            }

            var json = JsonConvert.SerializeObject(input);
            var content = new StringContent(json);
            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                TResult data = JsonConvert.DeserializeObject<TResult>(result);
                return data;
            }

            return default;
        }

        public async Task<TResult> PostAsync<TResult>(string url, MultipartFormDataContent input, Dictionary<string, string> urlParam = null)
        {
            var response = await _httpClient.PostAsync(url, input);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                TResult data = JsonConvert.DeserializeObject<TResult>(result);
                return data;
            }

            return default;
        }

        public async Task<string> PutAsync(string url, object input)
        {
            var json = JsonConvert.SerializeObject(input);
            var content = new StringContent(json);
            var response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }

            return null;
        }

        public async Task<TResult> PutAsync<TResult>(string url, object input, Dictionary<string, string> urlParam = null)
        {
            if (urlParam != null && urlParam.Count > 0)
            {
                var queryString = string.Join("&", urlParam.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
                url = $"{url}?{queryString}";
            }

            var json = JsonConvert.SerializeObject(input);
            var content = new StringContent(json);
            var response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                TResult data = JsonConvert.DeserializeObject<TResult>(result);
                return data;
            }
            return default;
        }

        public async Task<string> PutAsync(string url, HttpContent content)
        {
            var response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        private static string NormalizeBaseUrl(string url)
        {
            return url.EndsWith("/") ? url : url + "/";
        }

        private void UpdateAuthorizationHeader()
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            if (!string.IsNullOrEmpty(_token))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", _token);
            }
        }
    }
}