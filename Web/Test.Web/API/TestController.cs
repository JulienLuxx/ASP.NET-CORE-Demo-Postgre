using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Test.Core.HttpUtl;
using Test.Core.Map;
using Test.Service.Interface;
using Test.Service.QueryModel;
using Test.Web.Base;
using Test.Web.Filter;

namespace Test.Web.API
{
    [Authorize]
    [Produces("application/json")]
    [Route("API/Test")]
    [ServiceFilter(typeof(CustomerExceptionFilter))]
    public class TestController : BaseController
    {
        private readonly ICommentSvc _commentSvc;

        private IHttpClientFactory _clientFactory { get; set; }

        private IHttpClientUtil _httpClientUtil { get; set; }

        private IMapUtil _mapUtil { get; set; }

        public TestController(ICommentSvc commentSvc, IHttpClientFactory clientFactory, IMapUtil mapUtil, IHttpClientUtil httpClientUtil) 
        {
            _commentSvc = commentSvc;
            _clientFactory = clientFactory;
            _mapUtil = mapUtil;
            _httpClientUtil = httpClientUtil;
        }

        [HttpGet("Page")]
        public async Task<JsonResult> GetPageAsync(CommentQueryModel qModel)
        {
            var id=UserId;
            var name = UserName;

            var res = await _commentSvc.GetPageDataAsync(qModel);
            return Json(res);
        }

        [AllowAnonymous]
        [HttpPost("PostTest")]
        public async Task<JsonResult> PostTest(string Msg)
        {
            return Json(Msg);
        }

        [AllowAnonymous]
        [HttpGet("LogTest")]
        public async Task<JsonResult> LogTest()
        {
            throw new Exception("Error!");
            return Json(new { value1 = "", value2 = "" });
        }

        [AllowAnonymous]
        [HttpGet("HttpClientDownloadTest")]
        public async Task<dynamic> HttpClientDownloadTestAsync()
        {
            var urlStr = @"http://localhost:54238/API/ArticleType/Page";
            var httpMethod = new HttpMethod("GET");
            var param = new ArticleTypeQueryModel() { PageSize = 1 };
            var paramDict = _mapUtil.DynamicToDictionary(param);
            var url = QueryHelpers.AddQueryString(urlStr, paramDict);
            var request = new HttpRequestMessage(httpMethod, url);
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var memoryStream = new MemoryStream();
                if (stream.Length > 0)
                {
                    using (stream)
                    {
                        //var fileStream = new FileStream(@"D:/ProjecDoc/result.json", FileMode.Create);
                        //await stream.CopyToAsync(fileStream);
                        //fileStream.Close();
                        await stream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    Response.Headers.Add("Content-Disposition", "attachment; filename=result.json");
                    return new FileStreamResult(memoryStream, "text/plain; charset=utf-8");
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                return response.StatusCode;
            }
        }

        [AllowAnonymous]
        [HttpGet("HttpClientDownloadTest2")]
        public async Task<IActionResult> HttpClientDownloadTest2Async()
        {
            var urlStr = @"http://localhost:54238/API/ArticleType/Page";
            var param = new ArticleTypeQueryModel() { PageSize = 1 };
            var httpFileResult = await _httpClientUtil.GetFileStreamAsync(param, urlStr, "get", MediaTypeEnum.UrlQuery);
            if (httpFileResult.IsSuccess)
            {
                Response.Headers.Add("Content-Disposition", "attachment; filename=result.json");
                return new FileStreamResult(httpFileResult.Stream, "text/plain; charset=utf-8");
            }
            else
            {
                return Json(httpFileResult);
            }
        }

        [AllowAnonymous]
        [HttpGet("HttpClientDownloadTest3")]
        public async Task<IActionResult> HttpClientDownloadTest3Async()
        {
            var urlStr = @"http://localhost:54238/API/Log/Page";
            var param = new LogQueryModel() { PageSize = 100 };
            var httpFileResult = await _httpClientUtil.GetFileStreamAsync(param, urlStr, "get", MediaTypeEnum.UrlQuery);
            if (httpFileResult.IsSuccess)
            {
                //Response.Headers.Add("Content-Disposition", "attachment; filename=result.json");

                if (httpFileResult.Stream.Length > 0)
                {
                    using (httpFileResult.Stream)
                    {
                        var fileStream = new FileStream(@"D:/ProjecDoc/result.json", FileMode.Create);
                        await httpFileResult.Stream.CopyToAsync(fileStream);
                        fileStream.Close();
                        return Json(httpFileResult.IsSuccess);
                    }
                }
                else
                {
                    return Json(httpFileResult);
                }
                //return new FileStreamResult(httpFileResult.Stream, "text/plain; charset=utf-8");
            }
            else
            {
                return Json(httpFileResult);
            }
        }
    }
}