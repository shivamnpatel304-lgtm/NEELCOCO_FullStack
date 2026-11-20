import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import api from "../services/api";
import { FALLBACK_PRODUCTS } from "../data/fallbackProducts";

export default function ProductDetails() {
  const { id } = useParams();
  const [product, setProduct] = useState(() =>
    FALLBACK_PRODUCTS.find(p => p.id === Number(id)) || null
  );

  useEffect(() => {
    api.get(`/products/${id}`)
      .then(r => {
        if (r.data) setProduct(r.data);
      })
      .catch(() => {
        const found = FALLBACK_PRODUCTS.find(p => p.id === Number(id));
        if (found) setProduct(found);
      });
  }, [id]);

  if (!product) return <div className="p-20 text-center">Loading...</div>;

  return (
    <main className="max-w-6xl mx-auto px-5 py-14">
      <Link to="/products" className="text-sm underline">← Back to products</Link>
      <div className="grid md:grid-cols-2 gap-12 mt-8 items-center">
        <div className="flex justify-center">
          <img src={product.imageUrl} alt={product.name} className="rounded-3xl w-full max-w-md aspect-[9/16] object-cover shadow-soft" />
        </div>
        <div>
          <p className="text-xs uppercase tracking-widest text-gray-500">{product.category?.name || "Ice Pops"}</p>
          <h1 className="font-display text-6xl mt-3">{product.name}</h1>
          <p className="text-gray-600 mt-6 text-lg">{product.description}</p>
          {product.unit && (
            <div className="mt-6">
              <span className="text-xs uppercase tracking-wider text-gray-400 font-semibold">Packaging Unit: </span>
              <span className="text-sm text-gray-700 font-medium">{product.unit}</span>
            </div>
          )}
        </div>
      </div>
    </main>
  );
}
