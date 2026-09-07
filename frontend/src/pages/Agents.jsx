import { useEffect, useState } from "react";
import api from "../services/api";

const CATEGORIES = [
  "All",
  "Core Business",
  "Logistics & Distribution",
  "Sales & CRM",
  "Finance",
  "Manufacturing & Production",
  "Marketing",
  "Communication",
  "Management / Decision"
];

export default function Agents() {
  const [agents, setAgents] = useState([]);
  const [overview, setOverview] = useState(null);
  const [selectedCategory, setSelectedCategory] = useState("All");
  const [search, setSearch] = useState("");
  const [statusFilter, setStatusFilter] = useState("All");
  const [runningAgentId, setRunningAgentId] = useState(null);
  const [runningAll, setRunningAll] = useState(false);
  const [selectedAgent, setSelectedAgent] = useState(null);
  const [inspectModalOpen, setInspectModalOpen] = useState(false);
  const [loadingInspect, setLoadingInspect] = useState(false);

  const loadData = async () => {
    try {
      const [agentsRes, overviewRes] = await Promise.all([
        api.get("/agents"),
        api.get("/agents/overview")
      ]);
      setAgents(agentsRes.data);
      setOverview(overviewRes.data);
    } catch (err) {
      console.error("Failed to load agent data", err);
    }
  };

  useEffect(() => {
    loadData();
  }, []);

  const handleRunAgent = async (id, e) => {
    if (e) e.stopPropagation();
    setRunningAgentId(id);
    try {
      const res = await api.post(`/agents/${id}/run`);
      setAgents((prev) =>
        prev.map((a) =>
          a.id === id
            ? {
                ...a,
                status: res.data.status,
                lastRunAt: res.data.lastRunAt,
                summary: res.data.result?.summary,
                keyMetrics: res.data.result?.keyMetrics || [],
                alertCount: res.data.result?.alerts?.length || 0,
                actionCount: res.data.result?.recommendedActions?.length || 0
              }
            : a
        )
      );

      // Refresh overview
      const ov = await api.get("/agents/overview");
      setOverview(ov.data);

      if (selectedAgent && selectedAgent.id === id) {
        openInspect(id);
      }
    } catch (err) {
      console.error(`Failed to execute agent #${id}`, err);
    } finally {
      setRunningAgentId(null);
    }
  };

  const handleRunAll = async () => {
    setRunningAll(true);
    try {
      const res = await api.post("/agents/run-all");
      if (res.data?.overview) {
        setOverview(res.data.overview);
      }
      await loadData();
    } catch (err) {
      console.error("Failed to run all agents", err);
    } finally {
      setRunningAll(false);
    }
  };

  const openInspect = async (id) => {
    setLoadingInspect(true);
    setInspectModalOpen(true);
    try {
      const res = await api.get(`/agents/${id}`);
      setSelectedAgent(res.data);
    } catch (err) {
      console.error("Failed to inspect agent", err);
    } finally {
      setLoadingInspect(false);
    }
  };

  const filteredAgents = agents.filter((a) => {
    const matchesCat = selectedCategory === "All" || a.category.toLowerCase() === selectedCategory.toLowerCase();
    const matchesSearch =
      !search ||
      a.name.toLowerCase().includes(search.toLowerCase()) ||
      a.mainResponsibility.toLowerCase().includes(search.toLowerCase()) ||
      String(a.id) === search;
    const matchesStatus = statusFilter === "All" || a.status.toLowerCase() === statusFilter.toLowerCase();
    return matchesCat && matchesSearch && matchesStatus;
  });

  return (
    <main className="max-w-7xl mx-auto px-5 py-10 font-sans">
      {/* Header Banner */}
      <div className="bg-gradient-to-br from-neutral-950 via-neutral-900 to-neutral-950 text-white rounded-3xl p-8 md:p-12 mb-10 shadow-2xl border border-white/10 relative overflow-hidden">
        <div className="absolute top-0 right-0 w-96 h-96 bg-amber-500/10 rounded-full blur-3xl pointer-events-none"></div>
        <div className="relative z-10 flex flex-col md:flex-row md:items-center justify-between gap-8">
          <div>
            <div className="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-emerald-500/20 text-emerald-400 text-xs font-semibold tracking-wider uppercase border border-emerald-500/30 mb-4">
              <span className="w-2 h-2 rounded-full bg-emerald-400 animate-pulse"></span>
              Autonomous Enterprise System
            </div>
            <h1 className="font-display text-4xl md:text-6xl tracking-tight leading-none text-white">
              NEELCOCO AI Agents
            </h1>
            <p className="text-neutral-400 mt-3 max-w-xl text-base md:text-lg">
              56 Intelligent domain agents running synchronized analytics, predictive forecasting, cold-chain logistics, and executive decision-making.
            </p>
          </div>

          <div className="flex flex-wrap items-center gap-3">
            <button
              onClick={handleRunAll}
              disabled={runningAll}
              className="bg-white text-black hover:bg-neutral-100 disabled:opacity-60 px-6 py-3.5 rounded-full font-semibold shadow-lg transition-all flex items-center gap-2 text-sm"
            >
              {runningAll ? (
                <>
                  <svg className="animate-spin h-4 w-4 text-black" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                    <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                    <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8v8H4z"></path>
                  </svg>
                  <span>Running All 56 Agents...</span>
                </>
              ) : (
                <>
                  <span>⚡ Execute All 56 Agents</span>
                </>
              )}
            </button>
            <button
              onClick={loadData}
              className="border border-white/20 hover:border-white/40 text-neutral-300 hover:text-white px-5 py-3.5 rounded-full font-medium text-sm transition"
            >
              Refresh
            </button>
          </div>
        </div>

        {/* Quick KPI Counters */}
        <div className="grid grid-cols-2 sm:grid-cols-4 gap-4 mt-8 pt-8 border-t border-white/10">
          <div className="bg-white/5 rounded-2xl p-4 backdrop-blur-sm border border-white/5">
            <div className="text-xs uppercase tracking-wider text-neutral-400">Total Agents</div>
            <div className="text-3xl font-bold text-white mt-1">56</div>
            <div className="text-xs text-neutral-500 mt-1">Across 8 Pillars</div>
          </div>
          <div className="bg-white/5 rounded-2xl p-4 backdrop-blur-sm border border-white/5">
            <div className="text-xs uppercase tracking-wider text-neutral-400">Pillars Covered</div>
            <div className="text-3xl font-bold text-white mt-1">8</div>
            <div className="text-xs text-neutral-500 mt-1">100% Operational</div>
          </div>
          <div className="bg-white/5 rounded-2xl p-4 backdrop-blur-sm border border-white/5">
            <div className="text-xs uppercase tracking-wider text-neutral-400">Active Alerts</div>
            <div className="text-3xl font-bold text-amber-400 mt-1">
              {overview?.activeAlerts ?? 0}
            </div>
            <div className="text-xs text-neutral-500 mt-1">System Wide</div>
          </div>
          <div className="bg-white/5 rounded-2xl p-4 backdrop-blur-sm border border-white/5">
            <div className="text-xs uppercase tracking-wider text-neutral-400">Recommended Actions</div>
            <div className="text-3xl font-bold text-emerald-400 mt-1">
              {overview?.pendingActions ?? 0}
            </div>
            <div className="text-xs text-neutral-500 mt-1">High ROI Potential</div>
          </div>
        </div>
      </div>

      {/* Top Recommendations Banner if available */}
      {overview?.topRecommendations && overview.topRecommendations.length > 0 && (
        <div className="mb-10 bg-amber-500/5 border border-amber-500/20 rounded-3xl p-6">
          <div className="flex items-center gap-2 text-amber-900 font-semibold mb-3">
            <span className="text-xl">💡</span>
            <span className="text-sm uppercase tracking-wider">Top Executive AI Recommendations</span>
          </div>
          <div className="grid md:grid-cols-3 gap-4">
            {overview.topRecommendations.slice(0, 3).map((rec, idx) => (
              <div key={idx} className="bg-white border border-amber-200/60 rounded-2xl p-4 shadow-sm">
                <div className="flex items-center justify-between gap-2">
                  <span className="text-xs px-2 py-0.5 rounded-full font-bold bg-amber-100 text-amber-800 uppercase">
                    {rec.priority} Priority
                  </span>
                </div>
                <div className="font-semibold text-neutral-900 mt-2 text-sm">{rec.action}</div>
                <div className="text-xs text-neutral-600 mt-1">{rec.rationale}</div>
                <div className="text-xs text-emerald-700 font-medium mt-2">→ {rec.expectedImpact}</div>
              </div>
            ))}
          </div>
        </div>
      )}

      {/* Pillar Filter Tabs */}
      <div className="flex items-center gap-2 overflow-x-auto pb-4 mb-6 scrollbar-none">
        {CATEGORIES.map((cat) => {
          const count =
            cat === "All"
              ? agents.length
              : agents.filter((a) => a.category.toLowerCase() === cat.toLowerCase()).length;
          const isSelected = selectedCategory.toLowerCase() === cat.toLowerCase();

          return (
            <button
              key={cat}
              onClick={() => setSelectedCategory(cat)}
              className={`px-4 py-2.5 rounded-full text-xs font-semibold whitespace-nowrap transition-all flex items-center gap-2 ${
                isSelected
                  ? "bg-black text-white shadow-md"
                  : "bg-neutral-100 text-neutral-700 hover:bg-neutral-200"
              }`}
            >
              <span>{cat}</span>
              <span
                className={`px-1.5 py-0.5 rounded-full text-[10px] ${
                  isSelected ? "bg-white/20 text-white" : "bg-neutral-200 text-neutral-800"
                }`}
              >
                {count}
              </span>
            </button>
          );
        })}
      </div>

      {/* Search and Secondary Filter */}
      <div className="flex flex-col sm:flex-row gap-3 mb-8">
        <input
          type="text"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          placeholder="Search by agent name, ID, or responsibility..."
          className="flex-1 border rounded-full px-5 py-3 text-sm outline-none focus:ring-2 focus:ring-black bg-white shadow-sm"
        />
        <select
          value={statusFilter}
          onChange={(e) => setStatusFilter(e.target.value)}
          className="border rounded-full px-5 py-3 text-sm bg-white shadow-sm outline-none focus:ring-2 focus:ring-black"
        >
          <option value="All">All Statuses</option>
          <option value="Success">Success</option>
          <option value="Warning">Warning</option>
          <option value="Idle">Idle (Not yet run)</option>
        </select>
      </div>

      {/* Agents Grid */}
      <div className="grid sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-5">
        {filteredAgents.map((agent) => {
          const isRunning = runningAgentId === agent.id;
          const isSuccess = agent.status === "Success";
          const isWarning = agent.status === "Warning";

          return (
            <div
              key={agent.id}
              onClick={() => openInspect(agent.id)}
              className="group bg-white border border-neutral-200/80 hover:border-black/40 rounded-3xl p-5 shadow-sm hover:shadow-xl transition-all flex flex-col justify-between cursor-pointer"
            >
              <div>
                {/* Header */}
                <div className="flex items-center justify-between gap-2 mb-3">
                  <div className="flex items-center gap-2">
                    <span className="text-2xl">{agent.icon}</span>
                    <span className="text-xs font-mono font-semibold px-2 py-0.5 bg-neutral-100 rounded-md text-neutral-600">
                      #{agent.id}
                    </span>
                  </div>
                  <span
                    className={`text-[11px] font-semibold px-2.5 py-0.5 rounded-full flex items-center gap-1.5 ${
                      isSuccess
                        ? "bg-emerald-50 text-emerald-700 border border-emerald-200"
                        : isWarning
                        ? "bg-amber-50 text-amber-800 border border-amber-200"
                        : "bg-neutral-100 text-neutral-600 border border-neutral-200"
                    }`}
                  >
                    <span
                      className={`w-1.5 h-1.5 rounded-full ${
                        isSuccess ? "bg-emerald-500" : isWarning ? "bg-amber-500" : "bg-neutral-400"
                      }`}
                    ></span>
                    {agent.status}
                  </span>
                </div>

                {/* Title & Category */}
                <div className="text-xs uppercase tracking-wider font-semibold text-neutral-400">
                  {agent.category}
                </div>
                <h3 className="font-semibold text-lg text-neutral-900 mt-0.5 group-hover:text-black">
                  {agent.name}
                </h3>
                <p className="text-xs text-neutral-600 mt-2 line-clamp-2">
                  {agent.mainResponsibility}
                </p>

                {/* Key Metrics Chips */}
                {agent.keyMetrics && agent.keyMetrics.length > 0 && (
                  <div className="mt-4 flex flex-wrap gap-1.5">
                    {agent.keyMetrics.slice(0, 2).map((m, idx) => (
                      <span
                        key={idx}
                        className="text-[11px] bg-neutral-50 border border-neutral-200/60 text-neutral-700 px-2 py-0.5 rounded-lg"
                      >
                        {m}
                      </span>
                    ))}
                  </div>
                )}
              </div>

              {/* Card Footer Actions */}
              <div className="mt-5 pt-4 border-t border-neutral-100 flex items-center justify-between gap-2">
                <div className="text-[11px] text-neutral-400">
                  {agent.lastRunAt ? new Date(agent.lastRunAt).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }) : "Not run yet"}
                </div>
                <button
                  onClick={(e) => handleRunAgent(agent.id, e)}
                  disabled={isRunning}
                  className="bg-black hover:bg-neutral-800 disabled:opacity-60 text-white px-3.5 py-1.5 rounded-full text-xs font-semibold flex items-center gap-1.5 transition"
                >
                  {isRunning ? (
                    <>
                      <svg className="animate-spin h-3 w-3 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                        <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                        <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8v8H4z"></path>
                      </svg>
                      <span>Running</span>
                    </>
                  ) : (
                    <>
                      <span>▶ Run</span>
                    </>
                  )}
                </button>
              </div>
            </div>
          );
        })}
      </div>

      {filteredAgents.length === 0 && (
        <div className="text-center py-20 text-neutral-500">
          No agents found matching your query.
        </div>
      )}

      {/* Agent Deep-Dive Modal */}
      {inspectModalOpen && (
        <div className="fixed inset-0 z-50 bg-black/70 backdrop-blur-sm flex items-center justify-center p-4">
          <div className="bg-white rounded-3xl max-w-2xl w-full max-h-[90vh] overflow-y-auto p-6 md:p-8 shadow-2xl relative">
            <button
              onClick={() => setInspectModalOpen(false)}
              className="absolute top-6 right-6 text-neutral-400 hover:text-black text-xl w-8 h-8 rounded-full bg-neutral-100 flex items-center justify-center"
            >
              ✕
            </button>

            {loadingInspect || !selectedAgent ? (
              <div className="py-20 text-center text-neutral-500">Loading Agent Analysis...</div>
            ) : (
              <div>
                <div className="flex items-center gap-3">
                  <span className="text-3xl">{selectedAgent.icon}</span>
                  <div>
                    <div className="text-xs font-mono text-neutral-400">
                      AGENT #{selectedAgent.id} · {selectedAgent.category}
                    </div>
                    <h2 className="font-display text-2xl md:text-3xl text-neutral-900">
                      {selectedAgent.name}
                    </h2>
                  </div>
                </div>

                <p className="text-sm text-neutral-600 mt-3">{selectedAgent.mainResponsibility}</p>

                {/* Live Status & Run Action */}
                <div className="flex items-center justify-between gap-4 mt-6 p-4 rounded-2xl bg-neutral-50 border border-neutral-100">
                  <div>
                    <div className="text-xs text-neutral-400">Status</div>
                    <div className="font-semibold text-sm capitalize text-neutral-800">
                      {selectedAgent.status}
                    </div>
                  </div>
                  <div>
                    <div className="text-xs text-neutral-400">Last Executed</div>
                    <div className="text-sm text-neutral-800">
                      {selectedAgent.lastRunAt
                        ? new Date(selectedAgent.lastRunAt).toLocaleString()
                        : "Not executed yet"}
                    </div>
                  </div>
                  <button
                    onClick={() => handleRunAgent(selectedAgent.id)}
                    disabled={runningAgentId === selectedAgent.id}
                    className="bg-black hover:bg-neutral-800 disabled:opacity-60 text-white px-4 py-2 rounded-full text-xs font-semibold"
                  >
                    {runningAgentId === selectedAgent.id ? "Analyzing..." : "Trigger Run"}
                  </button>
                </div>

                {/* Analysis Summary */}
                {selectedAgent.lastResult?.summary && (
                  <div className="mt-6">
                    <h4 className="text-xs uppercase tracking-wider font-semibold text-neutral-500 mb-2">
                      Analysis Finding
                    </h4>
                    <div className="p-4 rounded-2xl bg-neutral-100 text-neutral-800 text-sm leading-relaxed">
                      {selectedAgent.lastResult.summary}
                    </div>
                  </div>
                )}

                {/* Key Metrics */}
                {selectedAgent.lastResult?.keyMetrics && selectedAgent.lastResult.keyMetrics.length > 0 && (
                  <div className="mt-6">
                    <h4 className="text-xs uppercase tracking-wider font-semibold text-neutral-500 mb-2">
                      Key Telemetry & Metrics
                    </h4>
                    <div className="grid grid-cols-2 gap-2">
                      {selectedAgent.lastResult.keyMetrics.map((k, idx) => (
                        <div key={idx} className="p-3 bg-neutral-50 border rounded-xl text-xs text-neutral-800 font-medium">
                          {k}
                        </div>
                      ))}
                    </div>
                  </div>
                )}

                {/* Recommended Actions */}
                {selectedAgent.lastResult?.recommendedActions &&
                  selectedAgent.lastResult.recommendedActions.length > 0 && (
                    <div className="mt-6">
                      <h4 className="text-xs uppercase tracking-wider font-semibold text-neutral-500 mb-2">
                        Recommended Actions
                      </h4>
                      <div className="space-y-3">
                        {selectedAgent.lastResult.recommendedActions.map((act, idx) => (
                          <div key={idx} className="border rounded-2xl p-4 bg-white shadow-sm">
                            <div className="flex items-center justify-between">
                              <span className="font-semibold text-sm text-neutral-900">{act.action}</span>
                              <span className="text-[10px] font-bold px-2 py-0.5 rounded-full bg-amber-100 text-amber-800 uppercase">
                                {act.priority}
                              </span>
                            </div>
                            <div className="text-xs text-neutral-600 mt-1">{act.rationale}</div>
                            <div className="text-xs text-emerald-700 font-medium mt-2">
                              Impact: {act.expectedImpact}
                            </div>
                          </div>
                        ))}
                      </div>
                    </div>
                  )}

                {/* Alerts */}
                {selectedAgent.lastResult?.alerts && selectedAgent.lastResult.alerts.length > 0 && (
                  <div className="mt-6">
                    <h4 className="text-xs uppercase tracking-wider font-semibold text-neutral-500 mb-2">
                      System Alerts
                    </h4>
                    <div className="space-y-2">
                      {selectedAgent.lastResult.alerts.map((al, idx) => (
                        <div
                          key={idx}
                          className={`p-3 rounded-xl text-xs border ${
                            al.level === "Critical"
                              ? "bg-red-50 border-red-200 text-red-800"
                              : al.level === "Warning"
                              ? "bg-amber-50 border-amber-200 text-amber-800"
                              : "bg-blue-50 border-blue-200 text-blue-800"
                          }`}
                        >
                          <div className="font-bold">{al.title}</div>
                          <div className="mt-0.5">{al.message}</div>
                        </div>
                      ))}
                    </div>
                  </div>
                )}
              </div>
            )}
          </div>
        </div>
      )}
    </main>
  );
}
