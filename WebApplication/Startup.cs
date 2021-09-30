using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment evn)
        {
            Configuration = configuration;
            Evn = evn;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Evn { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            //services.AddControllersWithViews(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //      .RequireAuthenticatedUser()
            //      .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //});
            services.AddRazorPages();
            services.AddControllersWithViews();

            //services.AddRazorPages();
            //services.AddRazorPages().AddMvcOptions(options =>
            //{
            //    //var policy = new AuthorizationPolicyBuilder()
            //    //    .RequireAuthenticatedUser()
            //    //    .Build();
            //    //options.Filters.Add(new AuthorizeFilter(policy));
            //});
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            //IMvcBuilder builder = services.AddRazorPages();

            //if (Evn.IsDevelopment())
            //{
            //    builder.AddRazorRuntimeCompilation();
            //}
            services.AddDbContext<DataBaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DataBaseContext")));
           services.AddAuthentication(AzureADDefaults.AuthenticationScheme).AddAzureAD(options => Configuration.Bind("AzureAd", options));
            //services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DataBaseContext>();
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Identity/Account/Login";
            //});
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseRewriter(
    new RewriteOptions().Add(
        context => {
            if (context.HttpContext.Request.Path == "/AzureAD/Account/SignedOut")
            { context.HttpContext.Response.Redirect("/Home/MainPage"); }
        })
);
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=MainPage}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
