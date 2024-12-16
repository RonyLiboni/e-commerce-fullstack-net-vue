using Hangfire;
using Microsoft.AspNetCore.Hosting;

namespace Rony.Store.WorkerService;
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
                var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");

                services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
                services.AddHangfireServer();

                services.AddSingleton<TemporaryStorageCleanUpJob>();

                services.AddHostedService<Worker>();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.Configure(app =>
                {
                    app.UseHangfireDashboard();

                    var recurringJobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();

                    recurringJobManager.AddOrUpdate<TemporaryStorageCleanUpJob>("TemporaryStorageCleanUpJob",
                        job => job.Execute(),
                        Cron.Hourly);
                });
            });
}