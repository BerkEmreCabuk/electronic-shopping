using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Middlewares
{
    public class RequestPerformanceMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestPerformanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            DateTime start = DateTime.Now;
            await _next(context);
            DateTime end = DateTime.Now;

            if ((end - start).TotalMilliseconds > 1000)
            {
                context.Request.EnableBuffering();
                var token = context.Request.Headers["Authorization"].FirstOrDefault();
                var body = context.Request.Body;
                var payload = "";
                if (body.CanSeek && body.Length > 0)
                {
                    body.Seek(0, SeekOrigin.Begin);
                    payload = new StreamReader(body).ReadToEnd();
                }

                var log = new
                {
                    RequestTime = start,
                    EndTime = end,
                    Duration = (end - start).TotalMilliseconds,
                    Token = token,
                    Payload = payload,
                    QueryString = context.Request.QueryString.Value,
                    Path = context.Request.Path,
                    RequestorIpAddress = context.Connection.RemoteIpAddress.ToString(),
                    Url = context.Request.GetDisplayUrl(),
                    RequestData = GetRequestData(context)
                };

                Log.Warning("Long Running Request {@Log}", log);
            }
        }

        private static string GetRequestData(HttpContext context)
        {
            var sb = new StringBuilder();

            if (context.Request.HasFormContentType && context.Request.Form.Any())
            {
                sb.Append("Form variables:");
                foreach (var x in context.Request.Form)
                    sb.AppendFormat("Key={0}, Value={1}<br/>", x.Key, x.Value);
            }
            sb.AppendLine("Method: " + context.Request.Method);
            return sb.ToString();
        }
    }
}
