using ElectronicShopping.Api.Constants;
using ElectronicShopping.Api.Extensions;
using ElectronicShopping.Api.Helpers;
using ElectronicShopping.Api.Infrastructure.Database;
using ElectronicShopping.Api.Middlewares;
using ElectronicShopping.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

namespace ElectronicShopping.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettingsModel>(Configuration.GetSection("AppSettings"));
            services.AddDbContext<ElectronicShoppingDbContext>(options => options.UseInMemoryDatabase(databaseName: "ElectronicShoppingDb"));
            services
                .AddRedisManager(Configuration)
                .AddAutoMapper()
                .AddRepositories()
                .AddApiVersionManager()
                .AddUserModel(Configuration)
                .AddMediatR(Assembly.Load(CommonKeyConstant.SERVICE_NAME))
                .AddControllers();

            Log.Logger = LoggingHelper.CustomLoggerConfiguration(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = CommonKeyConstant.SERVICE_NAME, Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                        }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{CommonKeyConstant.SERVICE_NAME} v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<TokenMiddleware>();
            app.UseMiddleware<RequestPerformanceMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
