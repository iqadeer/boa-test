using System.Net.Http;
using AutoMapper;
using BOA.WeatherForecast.Api.Services;
using BOA.WeatherForecast.Data;
using BOA.WeatherForecast.Data.Entities;
using BOA.WeatherForecast.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BOA.WeatherForecast.Api
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
            services.AddAutoMapper();
            services.AddCors(options =>
            {
                options.AddPolicy("dev",
                    policy => policy.WithOrigins("http://localhost:8880"));
            });
            services.AddMvc();

            services.AddScoped<IRepository<City>, CityRepository>();
            services.AddTransient<IForecastService, ForecastService>();
            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();
            services.AddTransient(typeof(IWeatherAppLogger<>), typeof(WeatherAppLogger<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("dev");
            app.UseMvc();
        }
    }
}
