using InventoryMgmt.Application.Interfaces.IRepositories;
using InventoryMgmt.Persistence.Contexts;
using InventoryMgmt.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMgmt
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
            string connectionString = string.Empty;
            try
            {
                connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTIONSTRING");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading environment  Variable " + ex.Message);
            }
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Configuration.GetConnectionString("SQLSERVER_CONNECTIONSTRING");
            }
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString,
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);//You can set Time   
            });

            services.AddControllers()
            .AddJsonOptions(options =>
               options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages().AddNewtonsoftJson();
            #region Repositories
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           
            #endregion
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
            app.UseSession();
            app.UseRouting();

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
