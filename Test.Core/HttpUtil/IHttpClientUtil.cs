using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core.HttpUtl
{
    public interface IHttpClientUtil
    {
        Task<HttpResult> SendAsync(dynamic param, string url, HttpMethod httpMethod, MediaTypeEnum mediaType, List<string> cookieList = null, string userAgent = null);

        Task<HttpFileResult> GetFileStreamAsync(dynamic param, string url, string httpMethodStr, MediaTypeEnum mediaType, string userAgent = null);
    }
}
