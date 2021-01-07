using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Hosting;

namespace Traders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseKestrel(options =>
                    {
                        int port = 5002;
                        options.ListenAnyIP(port);

                    });
                }).UseWindowsService();
    }
}
