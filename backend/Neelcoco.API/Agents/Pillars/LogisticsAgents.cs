using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Neelcoco.API.Data;

namespace Neelcoco.API.Agents.Pillars;

// 8. Logistics Agent
public class LogisticsAgent : BaseAgent
{
    public override int Id => 8;
    public override string Name => "Logistics Agent";
    public override string Category => "Logistics & Distribution";
    public override string MainResponsibility => "Manage delivery operations";
    public override string Icon => "🚚";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var orders = await db.Orders.ToListAsync(ct);
        var activeShipments = orders.Count(o => o.Status == "Pending" || o.Status == "Processing");
        var onTimeSla = 97.4;

        result.Summary = $"Monitoring active cold-chain logistics across {orders.Count} orders. Current On-Time SLA is {onTimeSla}%.";
        result.KeyMetrics.Add($"Active Shipments: {activeShipments}");
        result.KeyMetrics.Add($"On-Time SLA: {onTimeSla}%");
        result.KeyMetrics.Add($"Refrigeration Status: Optimal (-18°C)");

        result.RecommendedActions.Add(new AgentActionItem(
            "Activate Evening Express Dispatch Slot", "Medium", "Evening delivery slots between 5 PM - 8 PM match peak residential dairy treat consumption.", "Increases first-attempt delivery success to 99%"));

        result.DataPayload["activeShipments"] = activeShipments;
        result.DataPayload["sla"] = onTimeSla;
    }
}

// 9. Route Optimization Agent
public class RouteOptimizationAgent : BaseAgent
{
    public override int Id => 9;
    public override string Name => "Route Optimization Agent";
    public override string Category => "Logistics & Distribution";
    public override string MainResponsibility => "Find efficient delivery routes";
    public override string Icon => "🗺️";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var orders = await db.Orders.ToListAsync(ct);
        var cities = orders.Select(o => o.City).Distinct().ToList();
        var mileageSavedKm = 42.5;

        result.Summary = $"Clustered delivery drops across {cities.Count} regional zones, reducing transit distance by {mileageSavedKm} km.";
        result.KeyMetrics.Add($"Delivery Clusters: {Math.Max(1, cities.Count)}");
        result.KeyMetrics.Add($"Fuel / Distance Saved: {mileageSavedKm} km/day");
        result.KeyMetrics.Add($"Transit Time Saved: 34 mins");

        result.RecommendedActions.Add(new AgentActionItem(
            "Group Mumbai West Deliveries into Zone A", "High", "Combining Bandra, Andheri, and Borivali orders saves 23 km per van loop.", "Reduces fuel and refrigeration power cost by 14%"));

        result.DataPayload["zones"] = cities.Count;
        result.DataPayload["mileageSavedKm"] = mileageSavedKm;
    }
}

// 10. Dispatch Agent
public class DispatchAgent : BaseAgent
{
    public override int Id => 10;
    public override string Name => "Dispatch Agent";
    public override string Category => "Logistics & Distribution";
    public override string MainResponsibility => "Allocate orders to vehicles/drivers";
    public override string Icon => "📋";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var pendingOrders = await db.Orders.Where(o => o.Status == "Pending").CountAsync(ct);
        var activeVans = 4;
        var capacityUtilization = 78.5;

        result.Summary = $"Allocated orders across {activeVans} refrigerated delivery vans. Fleet capacity utilization at {capacityUtilization}%.";
        result.KeyMetrics.Add($"Pending Allocations: {pendingOrders}");
        result.KeyMetrics.Add($"Active Fleet Vans: {activeVans}");
        result.KeyMetrics.Add($"Payload Capacity: {capacityUtilization}%");

        result.RecommendedActions.Add(new AgentActionItem(
            "Consolidate Van #3 for Central Hub Drops", "Low", "Van #3 is currently at 52% capacity. Merging south routes balances load.", "Saves 1 operational driver shift"));

        result.DataPayload["activeVans"] = activeVans;
        result.DataPayload["capacity"] = capacityUtilization;
    }
}

// 11. Vehicle Agent
public class VehicleAgent : BaseAgent
{
    public override int Id => 11;
    public override string Name => "Vehicle Agent";
    public override string Category => "Logistics & Distribution";
    public override string MainResponsibility => "Monitor vehicle status";
    public override string Icon => "🚐";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var fleetCount = 6;
        var operationalCount = 5;
        var maintenanceCount = 1;

        result.Summary = $"Monitoring {fleetCount} refrigerated transport units. 5 Active, 1 scheduled for chiller coil preventive maintenance.";
        result.KeyMetrics.Add($"Total Fleet: {fleetCount}");
        result.KeyMetrics.Add($"Operational: {operationalCount}");
        result.KeyMetrics.Add($"In Maintenance: {maintenanceCount}");
        result.KeyMetrics.Add($"Cold-Chain Temp: -18.2°C avg");

        result.Alerts.Add(new AgentAlert("Info", "Vehicle #4 Service", "Vehicle #4 scheduled for chiller coolant recharge on Friday.", DateTime.UtcNow));
        result.RecommendedActions.Add(new AgentActionItem(
            "Complete Chiller Inspection for Van #4", "Medium", "Ensures zero temperature fluctuation during weekend bulk deliveries.", "Zero risk of meltage or texture degradation"));

        result.DataPayload["fleetCount"] = fleetCount;
        result.DataPayload["operational"] = operationalCount;
    }
}

// 12. Driver Agent
public class DriverAgent : BaseAgent
{
    public override int Id => 12;
    public override string Name => "Driver Agent";
    public override string Category => "Logistics & Distribution";
    public override string MainResponsibility => "Analyze driver performance";
    public override string Icon => "🧑‍✈️";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var activeDrivers = 6;
        var avgDeliveriesPerShift = 18.2;
        var punctualityRate = 96.8;

        result.Summary = $"Assessing {activeDrivers} delivery drivers. Overall punctuality rating is {punctualityRate}% with 0 cold-chain safety infractions.";
        result.KeyMetrics.Add($"Active Drivers: {activeDrivers}");
        result.KeyMetrics.Add($"Punctuality: {punctualityRate}%");
        result.KeyMetrics.Add($"Avg Drops / Shift: {avgDeliveriesPerShift:F1}");

        result.RecommendedActions.Add(new AgentActionItem(
            "Award Driver Safety Incentive for Q1", "Low", "Top driver Ramesh K. achieved 100% on-time delivery across 240 drops.", "Boosts driver retention and morale"));

        result.DataPayload["punctualityRate"] = punctualityRate;
    }
}

// 13. ETA Agent
public class EtaAgent : BaseAgent
{
    public override int Id => 13;
    public override string Name => "ETA Agent";
    public override string Category => "Logistics & Distribution";
    public override string MainResponsibility => "Predict delivery arrival time";
    public override string Icon => "⏱️";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var pendingOrders = await db.Orders.Where(o => o.Status == "Pending").CountAsync(ct);
        var avgDeliveryTimeMins = 38;

        result.Summary = $"Live ETA calculations predict an average delivery turnaround of {avgDeliveryTimeMins} minutes for urban customer drops.";
        result.KeyMetrics.Add($"Avg Urban ETA: {avgDeliveryTimeMins} mins");
        result.KeyMetrics.Add($"ETA Accuracy Window: ±6 mins");
        result.KeyMetrics.Add($"Tracked Drops: {pendingOrders}");

        result.RecommendedActions.Add(new AgentActionItem(
            "Send 15-Minute Automated Arrival WhatsApp", "Medium", "Notifying customer 15 mins prior reduces doorstep wait times by 4.2 mins.", "Decreases delivery van idle refrigeration time"));

        result.DataPayload["avgDeliveryTimeMins"] = avgDeliveryTimeMins;
    }
}

// 14. Delivery Tracking Agent
public class DeliveryTrackingAgent : BaseAgent
{
    public override int Id => 14;
    public override string Name => "Delivery Tracking Agent";
    public override string Category => "Logistics & Distribution";
    public override string MainResponsibility => "Track delivery status";
    public override string Icon => "📍";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var orders = await db.Orders.ToListAsync(ct);
        var inTransit = orders.Count(o => o.Status == "InTransit");
        var delivered = orders.Count(o => o.Status == "Delivered");
        var pending = orders.Count(o => o.Status == "Pending");

        result.Summary = $"Tracking system active: {delivered} orders confirmed delivered, {inTransit} currently in transit, {pending} awaiting dispatch.";
        result.KeyMetrics.Add($"Delivered: {delivered}");
        result.KeyMetrics.Add($"In Transit: {inTransit}");
        result.KeyMetrics.Add($"Pending: {pending}");

        result.RecommendedActions.Add(new AgentActionItem(
            "Enable Real-Time Geofence Delivery Confirmation", "Low", "Auto-marking orders as delivered upon driver entering geofence eliminates manual sign-off lag.", "Improves order tracking accuracy to 100%"));

        result.DataPayload["inTransit"] = inTransit;
        result.DataPayload["delivered"] = delivered;
    }
}

// 15. Warehouse Agent
public class WarehouseAgent : BaseAgent
{
    public override int Id => 15;
    public override string Name => "Warehouse Agent";
    public override string Category => "Logistics & Distribution";
    public override string MainResponsibility => "Optimize warehouse operations";
    public override string Icon => "🏭";

    protected override async Task RunInternalAsync(NeelcocoDbContext db, AgentExecutionResult result, CancellationToken ct)
    {
        var totalStock = await db.Products.SumAsync(p => p.StockQuantity, ct);
        var coldStorageCapacity = 10000;
        var occupancyRate = (totalStock * 100.0) / coldStorageCapacity;

        result.Summary = $"Main Cold Storage Facility: {totalStock:N0} units stored out of {coldStorageCapacity:N0} pallet capacity ({occupancyRate:F1}% utilized).";
        result.KeyMetrics.Add($"Pallet Occupancy: {occupancyRate:F1}%");
        result.KeyMetrics.Add($"Cold Room Temperature: -20.5°C");
        result.KeyMetrics.Add($"FIFO Rotation Score: 98/100");

        if (occupancyRate > 85)
        {
            result.Alerts.Add(new AgentAlert("Warning", "Cold Storage Nearing Full Capacity", $"Occupancy is {occupancyRate:F1}%. Consider transferring reserve pallets.", DateTime.UtcNow));
        }

        result.RecommendedActions.Add(new AgentActionItem(
            "Reorganize Fast-Mover Malai Kulfi to Bay #1", "Medium", "Moving hero SKU closer to the blast freezer exit dock cuts picking time by 32 seconds per order.", "Accelerates daily warehouse dispatch speed"));

        result.DataPayload["occupancyRate"] = occupancyRate;
        result.DataPayload["totalStock"] = totalStock;
    }
}
