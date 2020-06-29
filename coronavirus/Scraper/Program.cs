using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Scraper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddDbContext<ApplicationDbContext>(builder => 
                        builder.UseNpgsql("Host=localhost;Database=coronavirus-test;Username=postgres;Password=V3LzCmVY6D")
                    , ServiceLifetime.Singleton);

                    services.AddHostedService<FetchCountriesService>();
                });
    }
}
