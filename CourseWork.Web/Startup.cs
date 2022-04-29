using System;
using CourseWork.Business.Interfaces;
using CourseWork.Business.Services;
using CourseWork.Domain.Data;
using CourseWork.Domain.Interfaces;
using CourseWork.Domain.Models;
using CourseWork.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Collections
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
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("CourseWork.Domain")
                ));
            
            services.AddIdentity<User, IdentityRole>(options => 
                {
                    options.User.AllowedUserNameCharacters = string.Empty;
                })
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICollectionService, CollectionService>();
            // services.AddScoped<IAdminService, AdminService>();
            // services.AddScoped<IItemService, ItemService>();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });
            
            services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddAuthentication()
                .AddCookie()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    facebookOptions.SignInScheme = IdentityConstants.ExternalScheme;
                });
            // .AddReddit(redditOptions =>
                // {
                //     redditOptions.ClientId = Configuration["Authentication:Reddit:ClientId"];
                //     redditOptions.ClientSecret = Configuration["Authentication:Reddit:ClientSecret"];
                //     redditOptions.SignInScheme = IdentityConstants.ExternalScheme;
                // });
            
            // services.AddSignalR();
            
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/";
                options.AccessDeniedPath = "/";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRequestLocalization();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.Use((context, next) =>
            {
                if (context.Request.Headers["x-forwarded-proto"] == "https")
                {
                    context.Request.Scheme = "https";
                }
                return next();
            });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
                // endpoints.MapHub<CommentHub>("/comments");
            });
        }
    }
}