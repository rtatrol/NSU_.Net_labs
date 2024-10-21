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
                    services.AddTransient<IHackaton, Hackaton>();
                    services.AddTransient<IHRManager, HRManager>();
                    services.AddTransient<IHRDirector, HRDirector>();

                }).Build();
            host.Run();
        }
    }
}
