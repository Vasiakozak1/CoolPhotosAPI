using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using CoolPhotosAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CoolPhotosAPI.BL.Abstract;
using CoolPhotosAPI.BL.Services;
using CoolPhotosAPI.Data.Repositories;
using CoolPhotosAPI.Web.Middlewares;

namespace CoolPhotosAPI.Web
{
    public class Startup
    {
        private const string ALLOWED_CORS_ORIGINS_CONFIG_KEY = "AllowedCorsOrigins";

        private string[] _allowedCorsOrigins;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _allowedCorsOrigins = configuration.GetSection(ALLOWED_CORS_ORIGINS_CONFIG_KEY)
                                               .GetChildren()
                                               .Select(sec => sec.Value)
                                               .ToArray();
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

            
            services.AddCors(options => options.AddPolicy("default"
                , config => config.WithOrigins(_allowedCorsOrigins)
                                  .AllowCredentials()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()));

            services.AddDbContext<CoolDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultDb")));
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPhotoService, PhotoService>();

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

            app.UseCors("default");

            app.UseMiddleware<CatchExceptionsMiddleware>();
            app.UseMvc();
        }
    }
}
