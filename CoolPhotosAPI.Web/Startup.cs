using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using CoolPhotosAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CoolPhotosAPI.Web
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("MainCookie", options =>
                {
                    options.LoginPath = new PathString("/api/Account/SignInGoogle");
                    options.LogoutPath = new PathString("/api/Account/SignOut");
                    options.ReturnUrlParameter = "redirectUrl";
                })
                .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
                {
                    options.SaveTokens = true;
                    options.SignInScheme = "MainCookie";
                    options.ClientSecret = "hr01KhHZ5Ydcc8URZBfOTmaB";
                    options.ClientId = "134246088596-uaku6mupl8fiogf778uvfi6rkbqoiibd.apps.googleusercontent.com";
                });
            services.AddDbContext<CoolDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultDb")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
