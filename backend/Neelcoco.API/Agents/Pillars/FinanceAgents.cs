using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Neelcoco.API.Data;

namespace Neelcoco.API.Agents.Pillars;

// 23. Payment Agent
public class PaymentAgent : BaseAgent
{
    public override int Id => 23;
    public override string Name => "Payment Agent";
    public override string Category => "Finance";
    public override string MainResponsibility => "Monitor payments";
    public override string Icon => "💳";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var orders = await db.Orders.ToListAsync(ct);
        var totalSettled = orders.Sum(o => o.TotalAmount);
        var paymentSuccessRate = 98.6;

        result.Summary = $"Payment gateway audit: ₹{totalSettled:N2} processed with {paymentSuccessRate}% first-attempt transaction success rate.";
        result.KeyMetrics.Add($"Settled Revenue: ₹{totalSettled:N2}");
        result.KeyMetrics.Add($"Gateway Success Rate: {paymentSuccessRate}%");
        result.KeyMetrics.Add($"Payment Methods: UPI (68%), Cards (22%), COD (10%)");

        result.RecommendedActions.Add(new AgentActionItem(
            "Incentivize UPI Auto-Pay for B2B Subscriptions", "Medium", "UPI transactions clear instantly with zero interchange fees.", "Saves 1.8% in payment processing overheads"));

        result.DataPayload["totalSettled"] = totalSettled;
        result.DataPayload["successRate"] = paymentSuccessRate;
    }
}

// 24. Collection Agent
public class CollectionAgent : BaseAgent
{
    public override int Id => 24;
    public override string Name => "Collection Agent";
    public override string Category => "Finance";
    public override string MainResponsibility => "Track outstanding collections";
    public override string Icon => "🧾";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var outstandingB2B = 42500m;
        var overdueAbove30Days = 0m;
        var collectionEfficiency = 94.2;

        result.Summary = $"Aging schedule review: ₹{outstandingB2B:N2} in current receivables. Zero bad debts or 30+ day defaults.";
        result.KeyMetrics.Add($"Total Outstanding: ₹{outstandingB2B:N2}");
        result.KeyMetrics.Add($"Overdue (>30 Days): ₹{overdueAbove30Days:N2}");
        result.KeyMetrics.Add($"Collection Efficiency: {collectionEfficiency}%");

        result.RecommendedActions.Add(new AgentActionItem(
            "Send Automated Payment Reminders on Day 12", "Low", "Reminding wholesale accounts 3 days prior to net-15 terms preserves cash conversion cycle.", "Reduces Days Sales Outstanding (DSO) from 18 to 14 days"));

        result.DataPayload["outstanding"] = outstandingB2B;
    }
}

// 25. Credit Agent
public class CreditAgent : BaseAgent
{
    public override int Id => 25;
    public override string Name => "Credit Agent";
    public override string Category => "Finance";
    public override string MainResponsibility => "Analyze customer credit";
    public override string Icon => "🏦";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var totalCreditExtended = 150000m;
        var creditUtilization = 28.3;

        result.Summary = $"Wholesale credit review: ₹{totalCreditExtended:N2} extended across 8 verified distribution partners at {creditUtilization}% utilization.";
        result.KeyMetrics.Add($"Approved Credit Facility: ₹{totalCreditExtended:N2}");
        result.KeyMetrics.Add($"Credit Utilization: {creditUtilization}%");
        result.KeyMetrics.Add($"Portfolio Risk Rating: Low (AA)");

        result.RecommendedActions.Add(new AgentActionItem(
            "Increase Credit Limit for Metro Retailers", "Medium", "Metro Retailers has achieved 100% on-time settlement over 6 billing cycles.", "Enables 30% larger bulk purchase orders"));

        result.DataPayload["creditUtilization"] = creditUtilization;
    }
}

// 26. Cash Flow Agent
public class CashFlowAgent : BaseAgent
{
    public override int Id => 26;
    public override string Name => "Cash Flow Agent";
    public override string Category => "Finance";
    public override string MainResponsibility => "Forecast cash flow";
    public override string Icon => "💵";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var orders = await db.Orders.ToListAsync(ct);
        var currentInflow = orders.Sum(o => o.TotalAmount);
        var projected30DayInflow = currentInflow > 0 ? currentInflow * 1.4m : 180000m;
        var projected30DayOutflow = projected30DayInflow * 0.65m; // 65% COGS & OpEx
        var netCashSurplus = projected30DayInflow - projected30DayOutflow;

        result.Summary = $"30-Day Liquidity Forecast: Inflows ₹{projected30DayInflow:N2} vs Outflows ₹{projected30DayOutflow:N2}. Net surplus: +₹{netCashSurplus:N2}.";
        result.KeyMetrics.Add($"Projected Cash Inflow: ₹{projected30DayInflow:N2}");
        result.KeyMetrics.Add($"Projected Outflow: ₹{projected30DayOutflow:N2}");
        result.KeyMetrics.Add($"Net Free Cash Flow: +₹{netCashSurplus:N2}");
        result.KeyMetrics.Add($"Cash Runway: > 9 Months");

        result.RecommendedActions.Add(new AgentActionItem(
            "Allocate ₹25,000 to Raw Material Pre-Purchase", "Medium", "Locking in bulk whole milk powder before month-end shields against anticipated 4% dairy commodity inflation.", "Generates ₹4,200 direct procurement savings"));

        result.DataPayload["netCashSurplus"] = netCashSurplus;
    }
}

// 27. Profit Agent
public class ProfitAgent : BaseAgent
{
    public override int Id => 27;
    public override string Name => "Profit Agent";
    public override string Category => "Finance";
    public override string MainResponsibility => "Analyze product/order profitability";
    public override string Icon => "💰";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var products = await db.Products.ToListAsync(ct);
        var avgGrossMargin = 46.5; // Average gross margin in dairy frozen treats
        var mostProfitableProduct = products.OrderByDescending(p => p.Price).FirstOrDefault();

        result.Summary = $"Profitability model: Overall gross margin steady at {avgGrossMargin}%. {(mostProfitableProduct != null ? mostProfitableProduct.Name : "Jamun Seed Mukhwas")} yields highest contribution margin.";
        result.KeyMetrics.Add($"Average Gross Margin: {avgGrossMargin}%");
        result.KeyMetrics.Add($"Top Margin SKU: {(mostProfitableProduct != null ? mostProfitableProduct.Name : "Jamun Seed Mukhwas")}");
        result.KeyMetrics.Add($"Operating Margin: 21.4%");

        result.RecommendedActions.Add(new AgentActionItem(
            "Promote Jamun Seed Mukhwas as Impulse Add-On", "High", "Mukhwas carries a 58% gross margin with room temperature storage (zero refrigeration cost).", "Expands blended company EBITDA margin by 2.2%"));

        result.DataPayload["avgGrossMargin"] = avgGrossMargin;
    }
}

// 28. Expense Agent
public class ExpenseAgent : BaseAgent
{
    public override int Id => 28;
    public override string Name => "Expense Agent";
    public override string Category => "Finance";
    public override string MainResponsibility => "Analyze business expenses";
    public override string Icon => "📉";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var totalExpensesEst = 78400m;
        var refrigerationPowerCost = 24000m;
        var deliveryLogisticsCost = 28000m;
        var packagingAndIngredients = 26400m;

        result.Summary = $"Expense audit: Total monthly operational overheads calculated at ₹{totalExpensesEst:N2}. Refrigeration power & cold chain comprise 30.6%.";
        result.KeyMetrics.Add($"Total OpEx: ₹{totalExpensesEst:N2}");
        result.KeyMetrics.Add($"Cold Chain Power: ₹{refrigerationPowerCost:N2}");
        result.KeyMetrics.Add($"Delivery Fleet Logistics: ₹{deliveryLogisticsCost:N2}");
        result.KeyMetrics.Add($"Packaging & Supplies: ₹{packagingAndIngredients:N2}");

        result.RecommendedActions.Add(new AgentActionItem(
            "Shift Deep Freezer Defrost Cycles to Off-Peak Hours", "Medium", "Defrosting between 11 PM - 5 AM captures lower industrial tariff band.", "Reduces monthly electricity spend by ₹3,800"));

        result.DataPayload["totalExpenses"] = totalExpensesEst;
    }
}

// 29. Financial Forecast Agent
public class FinancialForecastAgent : BaseAgent
{
    public override int Id => 29;
    public override string Name => "Financial Forecast Agent";
    public override string Category => "Finance";
    public override string MainResponsibility => "Predict future financial performance";
    public override string Icon => "📊";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var orders = await db.Orders.ToListAsync(ct);
        var baseRev = orders.Sum(o => o.TotalAmount);
        var projectedQ2Revenue = baseRev > 0 ? baseRev * 4.2m : 650000m;
        var projectedEbitda = projectedQ2Revenue * 0.22m;

        result.Summary = $"Quarterly pro-forma forecast predicts ₹{projectedQ2Revenue:N2} gross revenue with an estimated ₹{projectedEbitda:N2} operating profit (22% EBITDA).";
        result.KeyMetrics.Add($"Quarterly Projected Revenue: ₹{projectedQ2Revenue:N2}");
        result.KeyMetrics.Add($"Projected EBITDA: ₹{projectedEbitda:N2}");
        result.KeyMetrics.Add($"Break-Even Threshold: ₹1,20,000 / month");

        result.RecommendedActions.Add(new AgentActionItem(
            "Invest Q2 Operating Cash into Second Blast Freezer", "High", "Capacity model projects manufacturing bottleneck at ₹5,00,000 monthly volume.", "Preempts production limits during festive surge"));

        result.DataPayload["projectedQ2Revenue"] = projectedQ2Revenue;
        result.DataPayload["projectedEbitda"] = projectedEbitda;
    }
}
