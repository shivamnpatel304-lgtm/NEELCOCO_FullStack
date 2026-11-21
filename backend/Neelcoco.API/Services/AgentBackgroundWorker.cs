using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Neelcoco.API.Data;

namespace Neelcoco.API.Agents;

public class AgentBackgroundWorker(
    IServiceScopeFactory scopeFactory,
    AgentOrchestrator orchestrator,
    ILogger<AgentBackgroundWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Autonomous Multi-Agent Worker initialized. Deploying 56 AI Agents...");

        // Initial launch delay to allow DB migration and server startup
        await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<NeelcocoDbContext>();

                var results = await orchestrator.ExecuteAllAsync(db);
                logger.LogInformation("Successfully synchronized and executed {Count} Autonomous AI Agents across all 8 pillars.", results.Count);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during periodic autonomous agent execution.");
            }

            // Periodic cadence: run every 15 minutes
            await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
        }
    }
}
