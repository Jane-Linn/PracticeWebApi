using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using WebApplication3.Cache;
using WebApplication3.Service;

namespace WebApplication3
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                
            });
            services.AddMemoryCache();
            services.AddSingleton<ISearchStockInfo, SearchStockInfo>();
            services.AddSingleton<ICache, CacheModual>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //.AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                   // name: ���� SwaggerDocument �� URL ��m�C
                   name: "practice1",
                   // info: �O�Ω� SwaggerDocument ������T�����(���e�D����)�C
                   info: new OpenApiInfo
                   {
                       Title = "Practice WebApi 1",
                       Version = "v1",
                       Description = "�m��1"
                   }
               );
               
            }
            );
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
            c.SwaggerEndpoint(url: "/swagger/practice1/swagger.json", name:"Practice WebApi v1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
