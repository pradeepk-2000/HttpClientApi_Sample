using Serilog;
using PerfectAPI.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using NSwag;
using NSwag.Generation.Processors.Security;
using PerfectAPI.Helpers.CustomAuthenticationMiddleware;
using PerfectAPI.BusinessLayer.Factories;
using PerfectAPI.BusinessLayer.Interfaces;
using PerfectAPI.Clients.ClientInterfaces;
using PerfectAPI.Clients.ClientRepositories;
using Microsoft.AspNetCore.Hosting;
using Polly;
using Polly.Registry;

namespace PerfectAPI
{
    public class Program
    {
       // public static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container...
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(name: MyAllowSpecificOrigins,
            //                      policy =>
            //                      {
            //                          policy.WithOrigins("https://localhost:7125/");
            //                      });
            //});
            builder.Services.AddControllers();
            builder.Services.AddCors();
            builder.Services.AddHttpClient();
            builder.Services.AddLogging();
            builder.Services.AddAutoMapper(typeof(Program));

            // Register Polly policies
           // builder.Services.AddPolicyRegistry();
            //builder.Services.AddScoped(typeof(IAsyncPolicy), typeof(AsyncPolicy));
            //registry =>
            //{
            //// Register your Polly policies here
            //registry.Add("MyRetryPolicy", Policy.Handle.Exception().RetryAsync(3));
            //});

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IInformationFactory, InformationFactory>();
            builder.Services.AddTransient<IEnvironmentSettings, EnvironmentSettings>();

            builder.Services.AddTransient<IEmployeeFactory, EmployeeFactory>();
            builder.Services.AddTransient<IAddNewEmployee, AddNewEmployee>();
            builder.Services.AddTransient<IGetAllEmployeeDetails, GetAllEmployeeDetails>();
             builder.Services.AddTransient<IGetEmployeeDetails, GetEmployeeDetails>();
            builder.Services.AddTransient<IUpdateEmployeeDesignation, UpdateEmployeeDesignation>();

            //Log.Logger = new LoggerConfiguration()
            //.WriteTo.File("Logs/log.txt")
            //.CreateLogger();

            var app = builder.Build();

            var loggerFactory = app.Services.GetService<ILoggerFactory>();
            var logFilePath = builder.Configuration["Logging:LogFilePath"].ToString();
            loggerFactory.AddFile(logFilePath);


            // Configure the HTTP request pipeline....

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          //  app.UsePathBase(new Microsoft.AspNetCore.Http.PathString(Constants.CONTEXT_URI));

           // app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true));
            //app.UseCors(MyAllowSpecificOrigins);

            //app.UseOpenApi(settings =>
            //    settings.PostProcess = (document, request) =>
            //    {
            //        document.Info.Title = "Perfect API";
            //        document.Info.Contact = new NSwag.OpenApiContact
            //        {
            //            Email = Constants.SWAGGER_CONTACT_EMAIL,
            //            Name = Constants.SWAGGER_CONTACT_NAME,
            //        };
            //        document.Info.Version = Constants.SWAGGER_VERSION;
            //        document.BasePath = Constants.CONTEXT_URI;
            //    });

            //app.UseMiddleware<BasicAuthenticationMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
