using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Identity.Dapper;
using Identity.Dapper.Entities;
using Identity.Dapper.SqlServer.Connections;
using Identity.Dapper.Models;
using Identity.Dapper.SqlServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Identity.Dapper.Connections;
using Microsoft.Extensions.Options;
using Identity.Dapper.Cryptography;
using CoffeeAppRevisted.Service;
using CoffeeAppRevisted.Config;
using Microsoft.AspNetCore.Routing.Patterns;

namespace CoffeeAppRevisted
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            //    var builder = new ConfigurationBuilder()
            //        .SetBasePath(env.ContentRootPath)
            //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //    Configuration = builder.Build(); 
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var connectionStringValue = Configuration["DatabaseConfig:ConnectionString:DefaultConnection"];

            var dapperIdentity = Configuration.GetSection("DatabaseConfig:Crypto");


            var connectionString = Configuration.GetSection("DatabaseConfig:ConnectionBoy:DefaultConnection").Value;
            services.Configure<ConnectionProviderOptions>(x => { x.ConnectionString = connectionString; });
            services.AddScoped<IConnectionProvider, SqlServerConnectionProvider>();
            // var configureNamedOptions = new ConfigureNamedOptions<AESKeys>();

            var aesKeys = new AESKeys
            {
                IV = Configuration.GetSection("DatabaseConfig:Crypto:IV").Value,
                Key = Configuration.GetSection("DatabaseConfig:Crypto:Key").Value
            };

            var option = new Option<AESKeys>
            {
                Value = aesKeys
            };

            services.AddSingleton<IOptions<AESKeys>>(option);
            services.ConfigureDapperIdentityOptions(new DapperIdentityOptions { UseTransactionalBehavior = false });
            services.AddSingleton<EncryptionHelper>();

            services.AddIdentity<DapperIdentityUser, DapperIdentityRole>(identityOptions =>
            {
                identityOptions.Password.RequireDigit = false;
                identityOptions.Password.RequiredLength = 1;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireUppercase = false;
            })
            .AddDapperIdentityFor<SqlServerConfiguration>()
            .AddDefaultTokenProviders();

            // Add application services.


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<ICoffeeRepository, CoffeeRepository>();
            services.Configure<DatabaseConfig>(Configuration.GetSection("DatabaseConfig:ConnectionBoy:DefaultConnection")); //This used to be "ConnectionString"
            services.AddControllersWithViews();
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



public class Option<T> : IOptions<T> where T : class, new()
{
    public T Value { get; set; }
}
