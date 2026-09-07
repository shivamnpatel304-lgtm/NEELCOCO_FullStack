import { useEffect, useState } from "react";
import api from "../services/api";

export default function Admin() {
  const [products,setProducts] = useState([]);
  const [orders,setOrders] = useState([]);

  useEffect(() => {
    Promise.all([api.get("/admin/products"), api.get("/orders")])
      .then(([p,o]) => { setProducts(p.data); setOrders(o.data); })
      .catch(() => {});
  }, []);

  const logout = () => {
    localStorage.removeItem("neelcoco_token");
    location.href = "/login";
  };

  return (
    <main className="max-w-7xl mx-auto px-5 py-12">
      <div className="flex justify-between items-center mb-10">
        <div><p className="text-xs tracking-[.3em] uppercase text-gray-500">Management</p><h1 className="font-display text-5xl">NEELCOCO Admin</h1></div>
        <button onClick={logout} className="border rounded-full px-5 py-2">Logout</button>
      </div>
      <div className="grid md:grid-cols-4 gap-5 mb-10">
        <div className="border rounded-3xl p-6"><p className="text-gray-500">Products</p><b className="text-4xl">{products.length}</b></div>
        <div className="border rounded-3xl p-6"><p className="text-gray-500">Orders</p><b className="text-4xl">{orders.length}</b></div>
        <div className="border rounded-3xl p-6"><p className="text-gray-500">Revenue</p><b className="text-4xl">₹{orders.reduce((s,o)=>s+o.totalAmount,0)}</b></div>
        <a href="/agents" className="bg-black text-white rounded-3xl p-6 flex flex-col justify-between hover:bg-neutral-800 transition">
          <div>
            <p className="text-neutral-400 text-xs uppercase tracking-wider">AI Operations</p>
            <b className="text-3xl">56 Agents</b>
          </div>
          <div className="text-xs text-emerald-400 font-semibold mt-3 flex items-center gap-1">
            <span>Open Command Center</span> →
          </div>
        </a>
      </div>
      <div className="border rounded-3xl overflow-hidden">
        <div className="p-6 font-semibold">Recent orders</div>
        <div className="overflow-x-auto">
          <table className="w-full text-sm">
            <thead className="bg-gray-100"><tr><th className="p-4 text-left">ID</th><th className="p-4 text-left">Customer</th><th className="p-4 text-left">Total</th><th className="p-4 text-left">Status</th></tr></thead>
            <tbody>{orders.slice(0,20).map(o=><tr key={o.id} className="border-t"><td className="p-4">#{o.id}</td><td className="p-4">{o.customerName}</td><td className="p-4">₹{o.totalAmount}</td><td className="p-4">{o.status}</td></tr>)}</tbody>
          </table>
        </div>
      </div>
    </main>
  );
}
