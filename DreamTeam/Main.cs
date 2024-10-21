using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DreamTeam
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<HackathonWorker>();
                    services.AddTransient<Hackaton>();
                    services.AddTransient<HRManager>();
                    services.AddTransient<HRDirector>();
                    services.AddTransient<IWishlistGenStrategy, RandomGenStrategy>();
                    services.AddTransient<ITeamsGenStrategy, FirstPriorityStrategy>();

                }).Build();
            host.Run();
        }
    }
}
