using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoreCommandEntities.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CoreCommandContracts;
using CoreCommandRepositories;
using CoreCommandLogger;
using Npgsql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CoreCommandAPI.Formatters;

public static class ServiceExtensions {

    public static void ConfigureIISIntegration(this IServiceCollection service) 
    => service.Configure<IISOptions>(option => {});

    public static void ConfigureCors(this IServiceCollection service) 
    => service.AddCors(option => 
            option.AddPolicy("CorsPolicy", 
            b => b.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod()));

    //using postgressql instead of sqlserver for database
    //secrets.json are generated and stored locally in appdata folder
    //its not important to add MigrationAssembly but i added it here 
    public static void ConfigurePostgreSql(this IServiceCollection service, 
    IConfiguration configuration)  {
      var builder = new NpgsqlConnectionStringBuilder();
      builder.ConnectionString = configuration.GetConnectionString("postgresConnection");
      builder.Username =configuration["USERID"];
      builder.Password = configuration["PASSWORD"];
     service.AddDbContext<CoreCommandContext>
        (o => o.UseNpgsql(builder.ConnectionString,
        b => b.MigrationsAssembly("CoreCommandAPI")));  
    }

    public static void ConfigureAuthentication(this IServiceCollection service,
    IConfiguration configuration) =>
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt => {
                opt.Audience = configuration["RESOURCEID"];
                opt.Authority = $"{configuration["INSTANCE"]}{configuration["TENANTID"]}";
            });
    

    public static void ConfigureRepository(this IServiceCollection service) 
    => service.AddScoped<IRepositoryWrapper,RepositoryWrapper>();

    //configure logging using NLog. Implementation is using LogManager.GetCurrentClassLogger()
    public static void ConfigureLogger(this IServiceCollection service) 
    => service.AddSingleton<ILoggerManager,LoggingManager>();     

    //content negotiation
    public static IMvcBuilder AddCustomDelimeterFormatter(this IMvcBuilder builder)
    => builder.AddMvcOptions(c => c.OutputFormatters.Add(new DelimeterOutputFormatter()));
}