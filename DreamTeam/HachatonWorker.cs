using Microsoft.Extensions.Hosting;

namespace DreamTeam
{
    public class HackathonWorker : BackgroundService
    {
        Hackaton _hackaton;
        IHostApplicationLifetime _lifetime;
        public HackathonWorker(Hackaton hackaton, IHostApplicationLifetime lifetime)
        {
            _hackaton = hackaton;
            _lifetime = lifetime;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var parser = new Utils.FileParser();

                List<Junior> juniors = parser.ParseJuniors("data/Juniors20.csv");
                List<TeamLead> teamLeaders = parser.ParseTeamLeads("data/Teamleads20.csv");

                int hackatonsNum = 1000;
                double summaryResult = 0;
                double[] harmonyResults = new double[hackatonsNum];

                await Task.Run(() =>
                {
                    for (int i = 0; i < hackatonsNum; i++)
                    {
                        double harmony = _hackaton.Run(juniors, teamLeaders);
                        summaryResult += harmony;
                        harmonyResults[i] = harmony;
                    }
                }, stoppingToken);

                for (int i = 0; i < hackatonsNum; i++)
                {
                    Console.WriteLine($"Hackaton№ {i + 1}, result {harmonyResults[i]}");
                }
                Console.WriteLine($"MEAN Harmony {summaryResult / hackatonsNum}");
                _lifetime.StopApplication();
            }
        }
    }

}
