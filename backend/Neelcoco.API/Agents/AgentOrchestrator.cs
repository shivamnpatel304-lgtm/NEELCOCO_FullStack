using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Neelcoco.API.Agents.Pillars;
using Neelcoco.API.Data;

namespace Neelcoco.API.Agents;

public class AgentOrchestrator
{
    private readonly List<IAgent> _agents = new();

    public AgentOrchestrator()
    {
        // 1. Core Business Agents (1-7)
        _agents.Add(new InventoryAgent());
        _agents.Add(new SalesAgent());
        _agents.Add(new OrderAgent());
        _agents.Add(new ProductAgent());
        _agents.Add(new CustomerAgent());
        _agents.Add(new ForecastAgent());
        _agents.Add(new RecommendationAgent());

        // 2. Logistics & Distribution Agents (8-15)
        _agents.Add(new LogisticsAgent());
        _agents.Add(new RouteOptimizationAgent());
        _agents.Add(new DispatchAgent());
        _agents.Add(new VehicleAgent());
        _agents.Add(new DriverAgent());
        _agents.Add(new EtaAgent());
        _agents.Add(new DeliveryTrackingAgent());
        _agents.Add(new WarehouseAgent());

        // 3. Sales & CRM Agents (16-22)
        _agents.Add(new LeadAgent());
        _agents.Add(new LeadScoringAgent());
        _agents.Add(new DistributorAgent());
        _agents.Add(new SalesTerritoryAgent());
        _agents.Add(new FollowUpAgent());
        _agents.Add(new SalesTargetAgent());
        _agents.Add(new ChurnPredictionAgent());

        // 4. Finance Agents (23-29)
        _agents.Add(new PaymentAgent());
        _agents.Add(new CollectionAgent());
        _agents.Add(new CreditAgent());
        _agents.Add(new CashFlowAgent());
        _agents.Add(new ProfitAgent());
        _agents.Add(new ExpenseAgent());
        _agents.Add(new FinancialForecastAgent());

        // 5. Manufacturing & Production Agents (30-36)
        _agents.Add(new ProductionAgent());
        _agents.Add(new RawMaterialAgent());
        _agents.Add(new ProductionForecastAgent());
        _agents.Add(new QualityAgent());
        _agents.Add(new BatchAgent());
        _agents.Add(new ExpiryAgent());
        _agents.Add(new WasteAgent());

        // 6. Marketing Agents (37-43)
        _agents.Add(new MarketingAgent());
        _agents.Add(new CampaignAgent());
        _agents.Add(new PromotionAgent());
        _agents.Add(new SocialMediaAgent());
        _agents.Add(new MarketResearchAgent());
        _agents.Add(new CompetitorAgent());
        _agents.Add(new PricingAgent());

        // 7. Communication Agents (44-49)
        _agents.Add(new EmailAgent());
        _agents.Add(new WhatsAppAgent());
        _agents.Add(new NotificationAgent());
        _agents.Add(new CustomerSupportAgent());
        _agents.Add(new ComplaintAgent());
        _agents.Add(new FeedbackAgent());

        // 8. Management / Decision Agents (50-56)
        _agents.Add(new DecisionAgent());
        _agents.Add(new RiskAgent());
        _agents.Add(new AnomalyDetectionAgent());
        _agents.Add(new KpiAgent());
        _agents.Add(new ExecutiveAgent());
        _agents.Add(new ReportAgent());
        _agents.Add(new AlertAgent());
    }

    public IReadOnlyList<IAgent> GetAllAgents() => _agents.AsReadOnly();

    public IAgent? GetAgentById(int id) => _agents.FirstOrDefault(a => a.Id == id);

    public IEnumerable<IAgent> GetAgentsByCategory(string category) =>
        _agents.Where(a => string.Equals(a.Category, category, StringComparison.OrdinalIgnoreCase));

    public async Task<AgentExecutionResult> ExecuteAgentAsync(int id, NeelcocoDbContext db, CancellationToken ct = default)
    {
        var agent = GetAgentById(id);
        if (agent == null)
            throw new KeyNotFoundException($"Agent with ID {id} not found.");

        return await agent.ExecuteAsync(db, ct);
    }

    public async Task<List<AgentExecutionResult>> ExecuteCategoryAsync(string category, NeelcocoDbContext db, CancellationToken ct = default)
    {
        var targetAgents = GetAgentsByCategory(category).ToList();
        var results = new List<AgentExecutionResult>();

        foreach (var agent in targetAgents)
        {
            var r = await agent.ExecuteAsync(db, ct);
            results.Add(r);
        }

        return results;
    }

    public async Task<List<AgentExecutionResult>> ExecuteAllAsync(NeelcocoDbContext db, CancellationToken ct = default)
    {
        var results = new List<AgentExecutionResult>();
        foreach (var agent in _agents)
        {
            var r = await agent.ExecuteAsync(db, ct);
            results.Add(r);
        }
        return results;
    }

    public AgentOverviewDto GetOverview()
    {
        var alerts = _agents.SelectMany(a => a.LastResult?.Alerts ?? Enumerable.Empty<AgentAlert>()).ToList();
        var recommendations = _agents.SelectMany(a => a.LastResult?.RecommendedActions ?? Enumerable.Empty<AgentActionItem>()).ToList();
        var categoryCounts = _agents.GroupBy(a => a.Category).ToDictionary(g => g.Key, g => g.Count());

        var criticalAlerts = alerts.Where(a => a.Level == "Critical" || a.Level == "Warning").Take(5).ToList();
        var topRecommendations = recommendations.Where(r => r.Priority == "High").Take(6).ToList();

        return new AgentOverviewDto(
            _agents.Count,
            alerts.Count,
            recommendations.Count,
            categoryCounts,
            criticalAlerts,
            topRecommendations,
            DateTime.UtcNow
        );
    }
}
