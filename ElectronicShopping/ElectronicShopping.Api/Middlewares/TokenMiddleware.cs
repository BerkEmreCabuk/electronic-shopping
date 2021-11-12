using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }
}
