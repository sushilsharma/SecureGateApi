using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SecureGate.WebApi.SignalRHub;

namespace SecureGateApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        //public static string ConnectionString { get; private set; }
        // This method gets called by the runtime. Use this method to add services to the container.



        public void ConfigureServices(IServiceCollection services)
        {
            string IsUseCors = GetConfigSettings("IsUseCors");
            string CorsSites = GetConfigSettings("CorsSites");
            if (IsUseCors == "true")
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins(CorsSites).AllowAnyHeader().AllowAnyMethod();
                    });
                });
            }
            else
            {
                services.AddCors();
            }


            services.AddSignalR();
            services.AddMvc();
            //services.AddSingleton<IConfiguration>(Configuration);
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();
            //services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            //services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddControllers().AddNewtonsoftJson(options => { options.UseMemberCasing(); });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //app.UseMiddleware<grRequestLogger>(); // Log every request 
            app.UseStaticFiles();
            app.UseRouting();

            string IsUseCors = GetConfigSettings("IsUseCors");
            if (IsUseCors == "true")
            {
                app.UseCors(MyAllowSpecificOrigins);
            }
            else
            {
                app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());
            }


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotifyHub>("/notify");  // the SignalrDemoHub url
            });
        }

        private string GetConfigSettings(string configValue)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);


            var root = configurationBuilder.Build();
            var ConfigSettings = root.GetSection(configValue).Value;

            return ConfigSettings;
        }
    }
}
