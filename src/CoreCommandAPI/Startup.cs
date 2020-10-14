using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using NLog;
using System.IO;

using CoreCommandEntities.Data;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CoreCommandContracts;

namespace CoreCommandAPI
{
    public class Startup
    {
        public IConfiguration Configuration {get;}
        public Startup(IConfiguration configuration)
        {
            //Load nlog.config from root folder
            LogManager.LoadConfiguration(
                string.Concat(Directory.GetCurrentDirectory(),
                "/nlog.config"));
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLogger();
            
            services.ConfigureIISIntegration();
            services.ConfigureCors();
            services.ConfigurePostgreSql(Configuration);
            //services.ConfigureAuthentication(Configuration);
            services.ConfigureRepository();
            
            services.AddControllers(c => {
                //content negotiation
                c.RespectBrowserAcceptHeader = true;
                c.ReturnHttpNotAcceptable = true;
            })
            .AddNewtonsoftJson( c => {
                c.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                c.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .AddXmlDataContractSerializerFormatters()
            .AddCustomDelimeterFormatter();


            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
        IWebHostEnvironment env,
        CoreCommandContext context, 
        ILoggerManager logger)
        {
            context.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");      

            //redirect all proxy headers to current requests
            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            });

            app.UseRouting();
            
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
