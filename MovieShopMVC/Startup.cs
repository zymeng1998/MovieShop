using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using MovieShopMVC.Services;

namespace MovieShopMVC
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
            services.AddControllersWithViews();
            services.AddScoped<IMovieServices, MovieServices>();
            // ionjfect connection string from appsettings.json to movieshopdbcontext.
            services.AddDbContext<MovieShopDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection"))
                );
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();

            services.AddHttpContextAccessor();
            services.AddAuthentication(CookieAuthenticationDefaults
                .AuthenticationScheme).AddCookie(option => 
                {
                    option.Cookie.Name = "MovieShopAuthCookie";
                    option.ExpireTimeSpan = TimeSpan.FromHours(2);
                    option.LoginPath = "/account/login";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            // step 1 validate email/password
            // 2 put useAuth middleware
            // Inject cookie name, expiration, in configue services
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
