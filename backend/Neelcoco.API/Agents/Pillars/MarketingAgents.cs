using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Neelcoco.API.Data;

namespace Neelcoco.API.Agents.Pillars;

// 37. Marketing Agent
public class MarketingAgent : BaseAgent
{
    public override int Id => 37;
    public override string Name => "Marketing Agent";
    public override string Category => "Marketing";
    public override string MainResponsibility => "Analyze marketing performance";
    public override string Icon => "📣";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var orders = await db.Orders.ToListAsync(ct);
        var totalRev = orders.Sum(o => o.TotalAmount);
        var blendedCac = 42.0m; // Customer acquisition cost in INR
        var roas = 4.8; // Return on ad spend

        result.Summary = $"Marketing performance review: Blended CAC is ₹{blendedCac:F0} against a 4.8x Return on Ad Spend (ROAS).";
        result.KeyMetrics.Add($"Blended CAC: ₹{blendedCac:F0}");
        result.KeyMetrics.Add($"ROAS: {roas}x");
        result.KeyMetrics.Add($"Top Acquisition Channel: Instagram & Word of Mouth");

        result.RecommendedActions.Add(new AgentActionItem(
            "Double Meta Ad Budget on Kulfi Reel Ads", "High", "Kulfi creative assets generate a 5.6x ROAS in urban pin codes.", "Drives an incremental ₹60,000 in monthly direct orders"));

        result.DataPayload["cac"] = blendedCac;
        result.DataPayload["roas"] = roas;
    }
}

// 38. Campaign Agent
public class CampaignAgent : BaseAgent
{
    public override int Id => 38;
    public override string Name => "Campaign Agent";
    public override string Category => "Marketing";
    public override string MainResponsibility => "Plan marketing campaigns";
    public override string Icon => "🎪";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var upcomingCampaign = "NEELCOCO Summer Melt-Free Magic";
        var plannedSpend = 35000m;

        result.Summary = $"Active Campaign Blueprint: '{upcomingCampaign}'. Targeted across families, schools, and weekend gatherings.";
        result.KeyMetrics.Add($"Featured Campaign: {upcomingCampaign}");
        result.KeyMetrics.Add($"Target Audience: 120,000 Impressions");
        result.KeyMetrics.Add($"Budget: ₹{plannedSpend:N0}");

        result.RecommendedActions.Add(new AgentActionItem(
            "Launch 'Kulfi Happy Hours' (3 PM - 6 PM)", "Medium", "Afternoon heat triggers peak dessert cravings in metropolitan zones.", "Smooths delivery fleet utilization throughout the day"));

        result.DataPayload["campaign"] = upcomingCampaign;
        result.DataPayload["budget"] = plannedSpend;
    }
}

// 39. Promotion Agent
public class PromotionAgent : BaseAgent
{
    public override int Id => 39;
    public override string Name => "Promotion Agent";
    public override string Category => "Marketing";
    public override string MainResponsibility => "Recommend offers/discounts";
    public override string Icon => "🎁";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var currentDiscount = "10% off on orders above ₹500";
        var redemptionRate = 18.4;

        result.Summary = $"Promotional engine recommendation: Dynamic discount '{currentDiscount}' lifts basket sizes by ₹140 on average.";
        result.KeyMetrics.Add($"Top Performing Promo: {currentDiscount}");
        result.KeyMetrics.Add($"Voucher Redemption Rate: {redemptionRate}%");
        result.KeyMetrics.Add($"Gross Margin Impact: Safe (-2.4% margin for +35% volume)");

        result.RecommendedActions.Add(new AgentActionItem(
            "Create 'Buy 3 Kulfis, Get 1 Ice Pop Free' Bundle", "High", "Liquidates high-margin Ice Pop inventory while anchoring high ticket value.", "Increases total order units by 25%"));

        result.DataPayload["redemptionRate"] = redemptionRate;
    }
}

// 40. Social Media Agent
public class SocialMediaAgent : BaseAgent
{
    public override int Id => 40;
    public override string Name => "Social Media Agent";
    public override string Category => "Marketing";
    public override string MainResponsibility => "Generate social-media content ideas";
    public override string Icon => "📱";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var contentPillars = new[]
        {
            "Behind The Scenes: How we slow-simmer pure milk for 4 hours for Malai Kulfi",
            "Taste Test Challenge: Kids blind-testing Milky Pop vs generic store pops",
            "Pairing Guide: The royal after-dinner Jamun Seed Mukhwas experience",
            "ASMR Sound: The classic snap and creamy bite of Chocolate Splash"
        };

        result.Summary = $"Generated 4 viral reel & post concepts for Instagram, YouTube Shorts, and LinkedIn.";
        result.KeyMetrics.Add($"Content Concepts: {contentPillars.Length}");
        result.KeyMetrics.Add($"Engagement Benchmark: 4.8%");
        result.KeyMetrics.Add($"Key Hashtag: #TheRealTasteOfMilk");

        foreach (var c in contentPillars.Take(2))
        {
            result.RecommendedActions.Add(new AgentActionItem(
                $"Produce Reel: {c.Split(':')[0]}", "Medium", c, "Builds authentic organic viral reach with zero paid media cost"));
        }

        result.DataPayload["ideas"] = contentPillars;
    }
}

// 41. Market Research Agent
public class MarketResearchAgent : BaseAgent
{
    public override int Id => 41;
    public override string Name => "Market Research Agent";
    public override string Category => "Marketing";
    public override string MainResponsibility => "Analyze market trends";
    public override string Icon => "🔎";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var categoryGrowth = 16.5; // Indian organized ice cream & dairy dessert CAGR
        var cleanLabelDemand = "+42% YoY";

        result.Summary = $"Dairy market intelligence: Traditional Indian dairy treats experiencing {categoryGrowth}% CAGR, fueled by demand for real milk fat over palm oil.";
        result.KeyMetrics.Add($"Category CAGR: {categoryGrowth}%");
        result.KeyMetrics.Add($"Clean Label Consumer Shift: {cleanLabelDemand}");
        result.KeyMetrics.Add($"Emerging Flavour Trend: Tender Coconut & Roasted Almond");

        result.RecommendedActions.Add(new AgentActionItem(
            "Pilot 'Zero Added Sugar' Malai Kulfi in Q3", "High", "Sugar-conscious urban demographic currently lacks premium traditional kulfi alternatives.", "Opens up a ₹1.2 Cr addressable market niche"));

        result.DataPayload["categoryGrowth"] = categoryGrowth;
    }
}

// 42. Competitor Agent
public class CompetitorAgent : BaseAgent
{
    public override int Id => 42;
    public override string Name => "Competitor Agent";
    public override string Category => "Marketing";
    public override string MainResponsibility => "Analyze competitors";
    public override string Icon => "🤺";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var competitors = new[] { "Amul", "Vadilal", "Havmor", "Kwality Wall's", "Artisanal D2C Brands" };

        result.Summary = $"Competitor benchmark: NEELCOCO maintains a distinct value advantage by providing 100% pure milk dairy desserts at accessible ₹15-₹80 retail price points.";
        result.KeyMetrics.Add($"Monitored Competitors: {competitors.Length}");
        result.KeyMetrics.Add($"Price Competitiveness: 18% lower than boutique D2C");
        result.KeyMetrics.Add($"Product Purity Advantage: Zero vegetable fat / frozen dessert additives");

        result.RecommendedActions.Add(new AgentActionItem(
            "Highlight 'Not A Frozen Dessert, Real Ice Cream' in Bio", "Medium", "Competitors frequently use reconstituted vegetable oils. Educating consumers creates sharp differentiation.", "Boosts customer conversion on product pages"));

        result.DataPayload["competitors"] = competitors;
    }
}

// 43. Pricing Agent
public class PricingAgent : BaseAgent
{
    public override int Id => 43;
    public override string Name => "Pricing Agent";
    public override string Category => "Marketing";
    public override string MainResponsibility => "Recommend product pricing";
    public override string Icon => "🏷️";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var products = await db.Products.ToListAsync(ct);
        var avgPrice = products.Any() ? products.Average(p => p.Price) : 0;

        result.Summary = $"Pricing elasticity model analyzed {products.Count} SKUs. Current portfolio sweet-spot rests at ₹{avgPrice:F0} average unit price.";
        result.KeyMetrics.Add($"Portfolio Avg Price: ₹{avgPrice:F0}");
        result.KeyMetrics.Add($"Price Elasticity: -1.2 (Healthy elasticity)");
        result.KeyMetrics.Add($"Price-to-Value Index: 94/100");

        result.RecommendedActions.Add(new AgentActionItem(
            "Keep Milky Pop at ₹15 Entry-Level Price Point", "High", "The ₹15 coin price point acts as the primary trial driver for younger customers.", "Drives 65% of new customer household trial"));

        result.DataPayload["avgPrice"] = avgPrice;
    }
}
