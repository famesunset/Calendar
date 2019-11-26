using System.IO.Compression;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Calendar.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Business.Services.Calendar;
using Business.Services.Event;
using Business.Services.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Data.Repository;
using Hangfire;
using Hangfire.SqlServer;
using System;

namespace Calendar
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
            // Dependency Injection
            services.AddSingleton<ICalendarService>(provider =>
                new CalendarService(
                    new CalendarRepo(),
                    new UserRepo(),
                    new AllDataRepo(),
                    new ColorRepo()
                    )
                );

            services.AddSingleton<IEventService>(provider =>
                new EventService(
                    new EventRepo(),
                    new CalendarRepo(),
                    new AllDataRepo(),
                    new UserRepo()
                    )
                );

            services.AddSingleton<IUserService>(provider =>
                new UserService(new UserRepo()));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();
            services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = System.TimeSpan.FromDays(14);
                })
                .AddGoogle(options =>
                {

                    options.ClientId = config["Authentication:Google:CI"] + ".apps.google" + "usercontent.com";
                    options.ClientSecret = config["Authentication:Google:CS"];
                    options.SaveTokens = true;
                    options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                    options.ClaimActions.Clear();
                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                    options.ClaimActions.MapJsonKey("urn:google:profile", "link");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    options.ClaimActions.MapJsonKey("picture", "picture");
                });

            services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            // hangfire
            app.UseHangfireDashboard();
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
