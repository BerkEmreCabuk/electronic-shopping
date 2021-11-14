using ElectronicShopping.Api.Constants;
using ElectronicShopping.Api.Helpers;
using ElectronicShopping.Api.Infrastructure.Cache;
using ElectronicShopping.Api.Models;
using ElectronicShopping.Api.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICacheService _cacheService;
        private readonly AppSettingsModel _appSettings;

        public TokenMiddleware(
            RequestDelegate next,
            ICacheService cacheService,
            IOptions<AppSettingsModel> appSettings)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _cacheService = cacheService;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
                throw new UnauthorizedException("Token not found");

            var token = context?.Request.Headers.First(e => e.Key == "Authorization").Value.ToString();
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedException("Token is null or empty");

            var userModel = SecurityHelper.ValidateToken(token, _appSettings.Secret);

            var cacheToken = await _cacheService.Get<string>($"{CacheKeyConstant.USER_INFO}{userModel?.Id}");
            if (string.IsNullOrEmpty(cacheToken) || cacheToken != token)
                throw new UnauthorizedException("Token is expired or invalid");

            await _next(context);
        }
    }
}
