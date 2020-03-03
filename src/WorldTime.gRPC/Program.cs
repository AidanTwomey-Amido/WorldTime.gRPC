using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WorldTime.gRPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(c =>
                    {
                        c.Listen(IPAddress.Any, 8001);
                        c.ConfigureHttpsDefaults(https =>
                        {
                            https.ServerCertificate = new X509Certificate2("WorldTime.gRPC.pfx", "Donkey1!");
                        });
                    })
                    .UseStartup<Startup>();
                });
    }
}
