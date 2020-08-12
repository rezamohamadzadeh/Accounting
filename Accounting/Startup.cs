using Accounting.Hangfire;
using Accounting.Jobs;
using Accounting.Utility;
using AutoMapper;
using DAL;
using DAL.Models;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Repository;
using Repository.InterFace;
using Service;
using System;

namespace Accounting
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("Local")));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddHttpClient();
            services.AddControllersWithViews()
                   .AddRazorRuntimeCompilation()
                   .AddJsonOptions(options =>
                   {
                       options.JsonSerializerOptions.IgnoreNullValues = true;
                   })
                   .AddNewtonsoftJson(options =>
                   {
                       options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                   });

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConneciton"),
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    }));

            services.AddHangfireServer();
            #region identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, MyClaimsPrincipalFactory>();

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(1);  // for set time out cookie authentication
            });
            #endregion
            #region AutoMapper
            services.AddAutoMapper(typeof(Startup));
            #endregion
            #region Gmail Service
            services.AddTransient<IEmailSender, EmailSender>(); // Dependecy injection For Email Service
            #endregion

            services.AddTransient<IHanfireJobs, HanfireJobs>();

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
