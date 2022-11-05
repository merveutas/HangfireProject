using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScheduleControl.BackgroundJob;
using ScheduleControl.BackgroundJob.Schedules;
using ScheduleControl.Business.Abstract;
using ScheduleControl.Business.Abstract.Auth;
using ScheduleControl.Business.Abstract.Mail;
using ScheduleControl.Business.Concrete.Managers;
using ScheduleControl.Business.Concrete.Managers.Auth;
using ScheduleControl.Business.Concrete.Managers.Mail;
using ScheduleControl.DataAccess.Abstract;
using ScheduleControl.DataAccess.Concrete.EntityFramework;
using ScheduleControl.DataAccess.Concrete.EntityFramework.Context;
using ScheduleControl.Entities.Dtos.DeveloperApp;
using ScheduleControl.Entities.Dtos.Mail;
using System;

namespace ScheduleControl.WebUI
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
            var connectionString = Configuration["ConnectionStrings:ProjectDev"];
            services.AddDbContext<ScheduleProjectDbContext>(option => option.UseSqlServer(connectionString));

            var hangfireConnectionString = Configuration["ConnectionStrings:HangfireDev"];
            services.AddHangfire(config =>
            {
                var option = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };

                config.UseSqlServerStorage(hangfireConnectionString, option)
                      .WithJobExpirationTimeout(TimeSpan.FromHours(6));
           
              });


            // dependency
            services.AddScoped<IDevAppDal, EfDevAppDal>();
            services.AddScoped<IDevAppService, DevAppManager>();

            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDal, EfUserDal>();

         

            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IMailService, MailManager>();
           
            // configuration configure options
            services.Configure<SmtpConfigDto>(Configuration.GetSection("SmtpConfig"));
          

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        [Obsolete]

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
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseHangfireDashboard();  
            app.UseHangfireDashboard("/merveutashangfire", new DashboardOptions
            {
                DashboardTitle = "MerveUtas Hangfire DashBoard",  // Dashboard sayfasına ait Başlık alanını değiştiririz.
                AppPath = "/Home/Index",                     // Dashboard üzerinden "back to site" button
                Authorization = new[] { new HangfireDashboardAuthorizationFilter() },   // Güvenlik için Authorization İşlemleri
            });
            //app.UseHangfireServer();
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                /*  Hangfire Server, planlanan işleri sıralarına göre sıralamak için zamanlamayı düzenli olarak denetler ve 
                    çalışanların bunları yürütmesine olanak tanır. 
                    Varsayılan olarak, kontrol aralığı 15 saniyeye eşittir, ancak BackgroundJobServer yapıcısına ilettiğiniz seçeneklerde 
                    SchedulePollingInterval özelliğini ayarlayarak değiştirebilirsiniz    */
                SchedulePollingInterval = TimeSpan.FromSeconds(30),

                //Arkaplanda çalışacak Job sayısını değiştirebiliriz.
                WorkerCount = Environment.ProcessorCount *3 
            });

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 10 });

            // Tanımlanan zaman diliminde sürekli çalıştığı için tetiklenmesine gerek yok, 
            // burada tanımlayabiliriz. tanımlayabiliriz.  

            RecurringJobs.AppListenCheckOperation();  //istek atma
            RecurringJobs.AppStatusCheckOperation();  //mail gönderme


        }
    }
}