import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import api from "../services/api";
import { useCart } from "../context";

export default function ProductDetails() {
  const { id } = useParams();
  const { add } = useCart();
  const [product, setProduct] = useState(null);

  useEffect(() => { api.get(`/products/${id}`).then(r => setProduct(r.data)); }, [id]);

  if (!product) return <div className="p-20 text-center">Loading...</div>;

  return (
    <main className="max-w-6xl mx-auto px-5 py-14">
      <Link to="/products" className="text-sm underline">← Back to products</Link>
      <div className="grid md:grid-cols-2 gap-12 mt-8 items-center">
        <img src={product.imageUrl} alt={product.name} className="rounded-3xl w-full aspect-square object-cover" />
        <div>
          <p className="text-xs uppercase tracking-widest text-gray-500">{product.category?.name}</p>
          <h1 className="font-display text-6xl mt-3">{product.name}</h1>
          <p className="text-gray-600 mt-6 text-lg">{product.description}</p>
          <div className="mt-8">
            <span className="text-3xl font-bold">₹{product.price}</span>
            <span className="ml-3 line-through text-gray-400">₹{product.mrp}</span>
            <span className="ml-3 text-gray-500">{product.unit}</span>
          </div>
          <button onClick={() => add(product)}
            className="mt-8 bg-black text-white rounded-full px-8 py-4 font-semibold">
            Add to cart
          </button>
        </div>
      </div>
    </main>
  );
}
