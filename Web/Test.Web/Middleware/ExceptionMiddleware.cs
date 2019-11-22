using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Internal;
using System.Text;
using System.IO;

namespace Test.Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger _logger;
        private IHostingEnvironment _environment;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="requestDelegate"></param>
        /// <param name="logger"></param>
        /// <param name="environment"></param>
        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger, IHostingEnvironment environment)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            var guid = Guid.NewGuid();
            try
            {
                await _requestDelegate.Invoke(context);
                //var features = context.Features;
                await HandleLog(guid, context);
            }
            catch (Exception ex)
            {
                await HandleExcetion(guid, context, ex);
            }
        }

        private async Task HandleLog(Guid guid, HttpContext context)
        {
            var info = string.Empty;
            var param = new Dictionary<string, string>();
            if (context.Request.ContentLength.HasValue)
            {
                if (context.Request.HasFormContentType)
                {
                    var form = context.Request.Form;
                    param = form.ToDictionary(x => x.Key, y => JsonConvert.SerializeObject(y.Value));
                }
                else if (context.Request.ContentLength > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (context.Request.Body)
                        {
                            await context.Request.Body.CopyToAsync(memoryStream);
                        }
                        var byteArray = memoryStream.GetBuffer();
                        var jsonParam = Encoding.UTF8.GetString(byteArray);
                        param = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonParam);
                    }
                }
            }
            if (context.Request.QueryString.HasValue)
            {
                param = context.Request.Query.ToDictionary(x => x.Key, y => JsonConvert.SerializeObject(y.Value));
            }
            info = JsonConvert.SerializeObject(new { Id = guid, ClientAddress = context.Connection.RemoteIpAddress.ToString() + ":" + context.Connection.RemotePort.ToString(), RequestUrl = context.Request.Host + context.Request.Path, Param = param });            
            _logger.LogInformation(info);
        }

        private async Task HandleExcetion(Guid guid,HttpContext context, Exception exception)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/json;charset=utf-8;";
            string error = string.Empty;

            void ReadException(Exception ex)
            {
                error += string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, ex.InnerException);
                if (null != ex.InnerException)
                {
                    ReadException(ex.InnerException);
                }
            }

            ReadException(exception);

            error = JsonConvert.SerializeObject(new
            {
                Id = guid,
                message = exception.Message,
                detail = error
            });
            if (_environment.IsDevelopment())
            {
                _logger.LogError(error);
            }
            else
            {
                _logger.LogError(error);
                error = "抱歉，出错了";
            }

            await context.Response.WriteAsync(error);
        }
    }
}
