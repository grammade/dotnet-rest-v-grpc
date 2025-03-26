using log4net;
using log4net.Config;
using Microsoft.Extensions.Options;
using netGrpcOne.Services;
using System.Reflection;

namespace netGrpcOne;

public static class Program
{
    public static void Main(string[] args)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo(@"log4net.config"));

        ILog logger = LogManager.GetLogger(typeof(Program));
        logger.Info("app starting");
        var builder = WebApplication.CreateBuilder(args);
        //builder.WebHost.ConfigureKestrel(serverOptions =>
        //{
        //    serverOptions.ListenAnyIP(6001, listenOptions =>
        //    {
        //        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2; 
        //        listenOptions.UseHttps();
        //    });
        //    // Plaintext endpoint
        //    serverOptions.ListenAnyIP(5000, listenOptions => 
        //    {
        //        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
        //    });
        //});



        // Add services to the container.
        builder.Services.AddGrpc();
        builder.Services.AddGrpcReflection();
        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        //app.UseMiddleware<ExceptionHandlingMiddleware>();

        // Configure the HTTP request pipeline.
        app.UseRouting();
        app.MapControllers();
        app.MapGrpcService<ProductGRPCService>();
        app.MapGrpcReflectionService();

        app.UseSwagger();
        app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
        {
            options.SwaggerEndpoint("v1/swagger.json", "v1");
            //options.RoutePrefix = string.Empty;
        });

        logger.Info("app started");
        app.Run();
    }
}