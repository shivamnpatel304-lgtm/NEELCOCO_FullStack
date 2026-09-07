using System;
using System.Collections.Generic;

namespace Neelcoco.API.Agents;

public record AgentAlert(
    string Level, // "Info", "Warning", "Critical"
    string Title,
    string Message,
    DateTime CreatedAt
);

public record AgentActionItem(
    string Action,
    string Priority, // "High", "Medium", "Low"
    string Rationale,
    string ExpectedImpact
);

public class AgentExecutionResult
{
    public bool Success { get; set; } = true;
    public string Summary { get; set; } = string.Empty;
    public List<string> KeyMetrics { get; set; } = new();
    public List<AgentActionItem> RecommendedActions { get; set; } = new();
    public List<AgentAlert> Alerts { get; set; } = new();
    public Dictionary<string, object> DataPayload { get; set; } = new();
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
    public double ExecutionDurationMs { get; set; }
}

public record AgentSummaryDto(
    int Id,
    string Name,
    string Category,
    string MainResponsibility,
    string Icon,
    string Status,
    DateTime? LastRunAt,
    string? Summary,
    List<string> KeyMetrics,
    int AlertCount,
    int ActionCount
);

public record AgentOverviewDto(
    int TotalAgents,
    int ActiveAlerts,
    int PendingActions,
    Dictionary<string, int> CategoryCounts,
    List<AgentAlert> CriticalAlerts,
    List<AgentActionItem> TopRecommendations,
    DateTime GeneratedAt
);
