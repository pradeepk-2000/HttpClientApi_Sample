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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            builder.Services.AddWebTaskMasterAPI();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var _logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
            builder.Services.AddLogging().AddSerilog(_logger);

            builder.Services.AddCors();
           // builder.Services.AddHttpClient();

          //  builder.Services.AddLogging();
           // builder.Services.AddAutoMapper(typeof(Program));


            builder.Services.AddSwaggerGen();

            #region not in use
            //Log.Logger = new LoggerConfiguration()
            //.WriteTo.File("Logs/log.txt")
            //.CreateLogger();
            #endregion

            var app = builder.Build();

            //var loggerFactory = app.Services.GetService<ILoggerFactory>();
            //var logFilePath = builder.Configuration["Logging:LogFilePath"].ToString();
            //loggerFactory.AddFile(logFilePath);


            // Configure the HTTP request pipeline....

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();    
            }
            app.UseSerilogRequestLogging();
            app.UsePathBase(new Microsoft.AspNetCore.Http.PathString("/perfect/api"));

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true));
            //app.UseCors(MyAllowSpecificOrigins);

            app.UseOpenApi(settings =>
                settings.PostProcess = (document, request) =>
                {
                    document.Info.Title = "Perfect API";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Email = Constants.SWAGGER_CONTACT_EMAIL,
                        Name = Constants.SWAGGER_CONTACT_NAME,
                    };
                    document.Info.Version = Constants.SWAGGER_VERSION;
                    document.BasePath = Constants.CONTEXT_URI;
                });

           app.UseMiddleware<BasicAuthenticationMiddleware>();
            app.UseSwaggerUi(x => x.DefaultModelExpandDepth = -1);
            app.MapControllers();

            app.Run();
        }
    }
}
