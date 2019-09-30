using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Map;

namespace Test.Core.HttpUtl
{
    public class HttpResult
    {
        public HttpResult(string result, List<string> cookies, bool isSuccess)
        {
            Result = result;
            Cookies = cookies;
            IsSuccess = isSuccess;
        }
        public List<string> Cookies { get; set; }

        public string Result { get; set; }

        public bool IsSuccess { get; set; }
    }

    public class HttpFileResult : HttpResult
    {
        public HttpFileResult(string result, List<string> cookies, bool isSuccess, MemoryStream stream) : base(result, cookies, isSuccess)
        {
            Stream = stream;
        }

        public MemoryStream Stream { get; set; }
    }

    public class HttpClientUtil : IHttpClientUtil 
    {
        private IHttpClientFactory _clientFactory { get; set; }
        private IMapUtil _mapUtil { get; set; }
        public HttpClientUtil(IHttpClientFactory clientFactory,IMapUtil mapUtil)
        {
            _clientFactory = clientFactory;
            _mapUtil = mapUtil;
        }

        public async Task<HttpResult> SendAsync(dynamic param, string url, HttpMethod httpMethod, MediaTypeEnum mediaType, List<string> cookieList= null, string userAgent = null) 
        {
            var request = new HttpRequestMessage(httpMethod, @url);
            if ((HttpMethod.Get.Equals(httpMethod)))
            {
                var dict = _mapUtil.DynamicToDictionary(param);
                switch (mediaType)
                {
                    case MediaTypeEnum.UrlQuery:
                        var paramUrl = QueryHelpers.AddQueryString(@url, dict);
                        request.RequestUri = new Uri(paramUrl);
                        break;
                    case MediaTypeEnum.ApplicationFormUrlencoded:
                        request.Content = new FormUrlEncodedContent(dict);
                        break;
                }

            }
            else if (HttpMethod.Post.Equals(httpMethod))
            {
                var dict = _mapUtil.DynamicToDictionary(param);
                switch (mediaType)
                {
                    case MediaTypeEnum.ApplicationFormUrlencoded:                        
                        request.Content = new FormUrlEncodedContent(dict);
                        break;
                    case MediaTypeEnum.ApplicationJson:
                        var jsonParam = JsonConvert.SerializeObject(param);
                        request.Content = new StringContent(jsonParam, Encoding.UTF8, "application/json");
                        break;
                    case MediaTypeEnum.MultipartFormData:
                        var content = new MultipartFormDataContent();                        
                        foreach (var item in dict)
                        {
                            content.Add(new StringContent(item.Value), item.Key);
                        }
                        request.Content = content;
                        break;
                }
            }
            else
            {
                throw new NotImplementedException();
            }
            if (null != cookieList && cookieList.Any())
            {
                request.Headers.Add("Set-Cookie", cookieList);
            }
            if (!string.IsNullOrEmpty(userAgent)&&!string.IsNullOrWhiteSpace(userAgent))
            {
                request.Headers.UserAgent.ParseAdd(userAgent);
            }
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var cookieFlag = response.Headers.TryGetValues("Set-Cookie", out var setCookies);
                if (cookieFlag)
                {
                    return new HttpResult(result, setCookies.ToList(), true);
                }
                else
                {
                    return new HttpResult(result, new List<string>(), true);
                }
            }
            else
            {
                return new HttpResult(response.StatusCode.ToString(), new List<string>(), false);
            }
        }

        public async Task<HttpResult> GetFileStreamAsync(dynamic param, string url, string httpMethodStr, MediaTypeEnum mediaType,string userAgent=null) 
        {
            httpMethodStr = httpMethodStr.ToUpper();
            var httpMethod = new HttpMethod(httpMethodStr);
            var paramDict = _mapUtil.DynamicToDictionary(param);
            var request = new HttpRequestMessage(httpMethod, url);
            if (HttpMethod.Get.Equals(httpMethod))
            {
                switch (mediaType)
                {
                    case MediaTypeEnum.UrlQuery:
                        url = QueryHelpers.AddQueryString(url, paramDict);
                        request.RequestUri = new Uri(url);
                        break;
                    case MediaTypeEnum.ApplicationFormUrlencoded:
                        request.Content = new FormUrlEncodedContent(paramDict);
                        break;
                }
            }
            else
            {
                throw new NotImplementedException();
            }
            if (!string.IsNullOrEmpty(userAgent) && !string.IsNullOrWhiteSpace(userAgent))
            {
                request.Headers.UserAgent.ParseAdd(userAgent);
            }
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var httpStream = await response.Content.ReadAsStreamAsync();
                var memoryStream = new MemoryStream();
                if (httpStream.Length > 0)
                {
                    using (httpStream)
                    {
                        await httpStream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return new HttpFileResult(string.Empty, new List<string>(), true, memoryStream);
                }
                else
                {
                    return new HttpFileResult(string.Empty, new List<string>(), false, null);
                }

            }
            else
            {
                return new HttpResult(response.StatusCode.ToString(), new List<string>(), false);
            }
        }
    }
}
