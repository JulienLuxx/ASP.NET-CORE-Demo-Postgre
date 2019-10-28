using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test.Service.Interface;
using Test.Service.QueryModel;

namespace Test.Web.API
{
    [Produces("application/json", new string[] { "multipart/form-data", "application/x-www-form-urlencoded" })]
    [Route("API/Log")]
    public class LogController : Controller
    {
        private readonly ILogSvc _logSvc;
        public LogController(ILogSvc logSvc)
        {
            _logSvc = logSvc;
        }

        [HttpGet("Page")]
        public async Task<IActionResult> GetPageAsync(LogQueryModel qModel)
        {
            var result= await _logSvc.GetPageDataAsync(qModel);
            var json = JsonConvert.SerializeObject(result);
            return Json(result);
        }
    }
}