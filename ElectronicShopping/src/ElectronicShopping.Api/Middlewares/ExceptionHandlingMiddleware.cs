using ElectronicShopping.Api.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IList<string> contentTypes = new List<string>()
        {
            "application/json",
            "application/x-www-form-urlencoded"
        };
        public ExceptionHandlingMiddleware(
            RequestDelegate next
        )
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context?.Request?.ContentType) && !contentTypes.Contains(context.Request.ContentType))
                    throw new NotAcceptableException();
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                context.Response.ContentType = "application/problem+json";
                var baseException = exception is BaseException ? (BaseException)exception : new InternalServerError();
                baseException.ProblemDetailsModel.Instance = context.Request.Path;
                baseException.ProblemDetailsModel.Type = context.Request.Path;
                context.Response.StatusCode = baseException.ProblemDetailsModel.Status.Value;

                DateTime end = DateTime.Now;
                context.Request.EnableBuffering();
                var token = context.Request.Headers["Authorization"].FirstOrDefault();
                var body = context.Request.Body;
                var payload = "";
                if (body.CanSeek)
                {
                    body.Seek(0, SeekOrigin.Begin);
                    payload = new StreamReader(body).ReadToEnd();
                }
                var log = new
                {
                    Token = token,
                    QueryString = context.Request.QueryString.Value,
                    Path = context.Request.Path,
                    RequestorIpAddress = context.Connection.RemoteIpAddress.ToString(),
                    Url = context.Request.GetDisplayUrl(),
                    RequestData = GetRequestData(context)
                };
                Log.Fatal(exception.Message, "{@Log} {@Payload} {@baseException}", log, payload, baseException);

                await context.Response.WriteAsync(JsonSerializer.Serialize(baseException.ProblemDetailsModel));
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
