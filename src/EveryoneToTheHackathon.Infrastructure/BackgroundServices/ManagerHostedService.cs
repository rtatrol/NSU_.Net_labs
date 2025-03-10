using System.Net.Http.Json;
using EveryoneToTheHackathon.Infrastructure.BackgroundServices.TaskQueues;
using EveryoneToTheHackathon.Infrastructure.Services;
using EveryoneToTheHackathon.Infrastructure.BackgroundServices.TaskQueues.Models;
using EveryoneToTheHackathon.Infrastructure.ServiceOptions;
using log4net;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace EveryoneToTheHackathon.Infrastructure.BackgroundServices;

public class ManagerHostedService(
    IOptions<ConfigOptions> options,
    IBackgroundTaskQueue<BaseTaskModel> backgroundTaskQueue,
    IHttpClientFactory httpClientFactory,
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(ManagerHostedService));
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var managerService = scope.ServiceProvider.GetRequiredService<IManagerService>();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await backgroundTaskQueue.DequeueAsync(stoppingToken);
                    var dreamTeams = managerService.ManageTeams();
                    var response = await httpClientFactory
                        .CreateClient()
                        .PostAsJsonAsync(
                            options.Value.Services!.BaseUrl!.DirectorUrl + "/api/teams",
                            new { DreamTeamDtos = dreamTeams },
                            stoppingToken
                        );
                    if (!response.IsSuccessStatusCode)
                    {
                        Logger.Fatal("ExecuteAsync: Got bad response.");
                        Logger.Fatal($"Response: {await response.Content.ReadAsStringAsync(stoppingToken)}");
                        Environment.Exit(15);
                    }
                    else
                    {
                        Logger.Info("ExecuteAsync: Teams successfully posted.");
                    }
                    await Task.CompletedTask;
                }
                catch (Exception e)
                {
                    Logger.Fatal("ExecuteAsync: Exception thrown");
                    Logger.Fatal("Exception:", e);
                    Environment.Exit(15);
                }
            }
        }
    }
}
