using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Neelcoco.API.Data;

namespace Neelcoco.API.Agents;

public enum AgentStatus
{
    Idle,
    Running,
    Success,
    Warning,
    Error
}

public interface IAgent
{
    int Id { get; }
    string Name { get; }
    string Category { get; }
    string MainResponsibility { get; }
    string Icon { get; }
    AgentStatus Status { get; }
    DateTime? LastRunAt { get; }
    AgentExecutionResult? LastResult { get; }

    Task<AgentExecutionResult> ExecuteAsync(NeelcocoDbContext db, CancellationToken ct = default);
}
