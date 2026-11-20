import { useEffect, useState } from "react";
import api from "../services/api";
import ProductCard from "../components/ProductCard";
import { FALLBACK_PRODUCTS, FALLBACK_CATEGORIES } from "../data/fallbackProducts";

export default function Products() {
  const [products, setProducts] = useState(FALLBACK_PRODUCTS);
  const [categories, setCategories] = useState(FALLBACK_CATEGORIES);
  const [search, setSearch] = useState("");
  const [categoryId, setCategoryId] = useState("");

  const load = () => {
    api.get("/products", { params: { search, categoryId: categoryId || undefined } })
      .then(r => {
        if (r.data && Array.isArray(r.data)) {
          setProducts(r.data);
        }
      })
      .catch(() => {
        let filtered = FALLBACK_PRODUCTS;
        if (search) {
          filtered = filtered.filter(p => p.name.toLowerCase().includes(search.toLowerCase()));
        }
        if (categoryId) {
          filtered = filtered.filter(p => p.categoryId === Number(categoryId));
        }
        setProducts(filtered);
      });
  };

  useEffect(() => {
    api.get("/products/categories")
      .then(r => {
        if (r.data && Array.isArray(r.data) && r.data.length > 0) {
          setCategories(r.data);
        }
      })
      .catch(() => {});
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
