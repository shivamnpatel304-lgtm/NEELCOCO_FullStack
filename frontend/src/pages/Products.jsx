import { useEffect, useState } from "react";
import api from "../services/api";
import ProductCard from "../components/ProductCard";

export default function Products() {
  const [products, setProducts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [search, setSearch] = useState("");
  const [categoryId, setCategoryId] = useState("");

  const load = () => {
    api.get("/products", { params: { search, categoryId: categoryId || undefined } })
      .then(r => setProducts(r.data)).catch(console.error);
  };

  useEffect(() => {
    api.get("/products/categories").then(r => setCategories(r.data));
  }, []);
  useEffect(() => { load(); }, [search, categoryId]);

  return (
    <main className="max-w-7xl mx-auto px-5 py-14">
      <div className="mb-10">
        <p className="uppercase tracking-[.3em] text-xs text-gray-500">NEELCOCO catalogue</p>
        <h1 className="font-display text-5xl mt-2">Something delicious.</h1>
      </div>
      <div className="flex flex-col md:flex-row gap-3 mb-8">
        <input value={search} onChange={e => setSearch(e.target.value)}
          placeholder="Search products..."
          className="flex-1 border rounded-full px-5 py-3 outline-none focus:ring-2 focus:ring-black" />
        <select value={categoryId} onChange={e => setCategoryId(e.target.value)}
          className="border rounded-full px-5 py-3 bg-white">
          <option value="">All categories</option>
          {categories.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
        </select>
      </div>
      <div className="grid sm:grid-cols-2 lg:grid-cols-4 gap-6">
        {products.map(p => <ProductCard key={p.id} product={p} />)}
      </div>
      {!products.length && <p className="text-gray-500 py-20 text-center">No products found.</p>}
    </main>
  );
}
