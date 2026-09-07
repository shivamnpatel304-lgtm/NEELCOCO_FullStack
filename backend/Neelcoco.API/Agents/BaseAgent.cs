using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Neelcoco.API.Data;

namespace Neelcoco.API.Agents;

public abstract class BaseAgent : IAgent
{
    public abstract int Id { get; }
    public abstract string Name { get; }
    public abstract string Category { get; }
    public abstract string MainResponsibility { get; }
    public virtual string Icon => "⚡";

    public AgentStatus Status { get; protected set; } = AgentStatus.Idle;
    public DateTime? LastRunAt { get; protected set; }
    public AgentExecutionResult? LastResult { get; protected set; }

    public async Task<AgentExecutionResult> ExecuteAsync(NeelcocoDbContext db, CancellationToken ct = default)
    {
        Status = AgentStatus.Running;
        var sw = Stopwatch.StartNew();
        var result = new AgentExecutionResult();

        try
        {
            await RunInternalAsync(db, result, ct);
            sw.Stop();
            result.ExecutionDurationMs = Math.Round(sw.Elapsed.TotalMilliseconds, 2);
            result.ExecutedAt = DateTime.UtcNow;

            Status = result.Alerts.Exists(a => a.Level == "Critical") ? AgentStatus.Warning : AgentStatus.Success;
            LastResult = result;
            LastRunAt = result.ExecutedAt;
        }
        catch (Exception ex)
        {
            sw.Stop();
            Status = AgentStatus.Error;
            result.Success = false;
            result.Summary = $"Execution failed: {ex.Message}";
            result.ExecutionDurationMs = Math.Round(sw.Elapsed.TotalMilliseconds, 2);
            result.Alerts.Add(new AgentAlert("Critical", "Execution Failure", ex.Message, DateTime.UtcNow));
            LastResult = result;
            LastRunAt = DateTime.UtcNow;
        }

        return result;
    }

    protected abstract Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct);

    public AgentSummaryDto ToSummary()
    {
        return new AgentSummaryDto(
            Id,
            Name,
            Category,
            MainResponsibility,
            Icon,
            Status.ToString(),
            LastRunAt,
            LastResult?.Summary,
            LastResult?.KeyMetrics ?? new List<string>(),
            LastResult?.Alerts.Count ?? 0,
            LastResult?.RecommendedActions.Count ?? 0
        );
    }
}
