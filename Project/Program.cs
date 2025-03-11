using Microsoft.Extensions.Options;
using netGrpcOne.Services;

namespace netGrpcOne;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            //serverOptions.ConfigureEndpointDefaults(listenOptions =>
            //{
            //    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2; 
            //});
            serverOptions.ListenAnyIP(6000, listenOptions =>
            {
                listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2; 
                listenOptions.UseHttps();
            });
            // Plaintext endpoint
            serverOptions.ListenAnyIP(5000, listenOptions => 
            {
                listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
            });
        });



        // Add services to the container.
        builder.Services.AddGrpc();
        builder.Services.AddGrpcReflection();
        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseRouting();
        app.MapControllers();
        app.MapGrpcService<ProductGRPCService>();
        app.MapGrpcReflectionService();

        app.Run();
    }
}