using AutoMapper;
using ElectronicShopping.Api.Constants;
using ElectronicShopping.Api.Helpers;
using ElectronicShopping.Api.Infrastructure.Cache;
using ElectronicShopping.Api.Infrastructure.Cache.Redis;
using ElectronicShopping.Api.Infrastructure.Mapper;
using ElectronicShopping.Api.Models;
using ElectronicShopping.Api.Repositories;
using ElectronicShopping.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ElectronicShopping.Api.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddRedisManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheConfigModel>(configuration.GetSection(CommonKeyConstant.CACHE_CONFIG_KEY));
            services.AddSingleton<RedisServer>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICartDetailRepository, CartDetailRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddApiVersionManager(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = ApiVersion.Default;
            });

            return services;
        }

        public static IServiceCollection AddUserModel(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var settings = new AppSettingsModel();

            configuration.Bind("AppSettings", settings);

            services.AddScoped(sp =>
            {
                var httpContext = (sp.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor)?.HttpContext;
                if (httpContext == null || !httpContext.Request.Headers.ContainsKey("Authorization"))
                    return null;

                string token = httpContext.Request.Headers.FirstOrDefault(e => e.Key == "Authorization").Value.ToString();
                var model = SecurityHelper.ValidateToken(token, settings.Secret);

                return model;
            });

            return services;
        }
    }
}
