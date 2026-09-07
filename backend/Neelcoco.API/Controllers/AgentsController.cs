using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neelcoco.API.Agents;
using Neelcoco.API.Data;

namespace Neelcoco.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgentsController(AgentOrchestrator orchestrator, NeelcocoDbContext db) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] string? category)
    {
        var agents = string.IsNullOrWhiteSpace(category)
            ? orchestrator.GetAllAgents()
            : orchestrator.GetAgentsByCategory(category);

        var dtos = agents.Select(a => ((BaseAgent)a).ToSummary()).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var agent = orchestrator.GetAgentById(id);
        if (agent == null) return NotFound(new { message = $"Agent #{id} not found." });

        return Ok(new
        {
            agent.Id,
            agent.Name,
            agent.Category,
            agent.MainResponsibility,
            agent.Icon,
            Status = agent.Status.ToString(),
            agent.LastRunAt,
            agent.LastResult
        });
    }

    [HttpPost("{id:int}/run")]
    public async Task<IActionResult> RunAgent(int id)
    {
        try
        {
            var result = await orchestrator.ExecuteAgentAsync(id, db);
            var agent = orchestrator.GetAgentById(id)!;
            return Ok(new
            {
                agent.Id,
                agent.Name,
                agent.Category,
                Status = agent.Status.ToString(),
                agent.LastRunAt,
                Result = result
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("category/{category}/run")]
    public async Task<IActionResult> RunCategory(string category)
    {
        var decodedCategory = Uri.UnescapeDataString(category);
        var results = await orchestrator.ExecuteCategoryAsync(decodedCategory, db);
        return Ok(new
        {
            category = decodedCategory,
            executedCount = results.Count,
            results
        });
    }

    [HttpPost("run-all")]
    public async Task<IActionResult> RunAll()
    {
        var results = await orchestrator.ExecuteAllAsync(db);
        return Ok(new
        {
            totalExecuted = results.Count,
            overview = orchestrator.GetOverview()
        });
    }

    [HttpGet("overview")]
    public IActionResult GetOverview()
    {
        return Ok(orchestrator.GetOverview());
    }

    [HttpGet("categories")]
    public IActionResult GetCategories()
    {
        var categories = orchestrator.GetAllAgents()
            .GroupBy(a => a.Category)
            .Select(g => new
            {
                Name = g.Key,
                AgentCount = g.Count(),
                AgentIds = g.Select(x => x.Id).ToList()
            })
            .ToList();

        return Ok(categories);
    }
}
