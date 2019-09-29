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

        private IMapUtil _mapUtil { get; set; }

        public TestController(ICommentSvc commentSvc,IHttpClientFactory clientFactory,IMapUtil mapUtil)
        {
            _commentSvc = commentSvc;
            _clientFactory = clientFactory;
            _mapUtil = mapUtil;
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
        [HttpGet("HttpClientDownloadTestAsync")]
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
                    throw new NullReferenceException();
                }
            }
            else
            {
                return response.StatusCode;
            }
        }
    }
}