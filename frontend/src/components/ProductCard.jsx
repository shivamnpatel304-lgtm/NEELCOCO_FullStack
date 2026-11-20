import { Link } from "react-router-dom";

export default function ProductCard({ product }) {
  return (
    <article className="bg-white rounded-3xl overflow-hidden shadow-soft border border-gray-100 group flex flex-col">
      <Link to={`/products/${product.id}`} className="block">
        <div className="aspect-[9/16] overflow-hidden bg-gray-100">
          <img src={product.imageUrl} alt={product.name}
               className="w-full h-full object-cover group-hover:scale-105 transition duration-500" />
        </div>
      </Link>
      <div className="p-5 flex flex-col flex-1 justify-between">
        <div>
          <p className="text-xs uppercase tracking-widest text-gray-500">{product.category?.name || "Ice Pops"}</p>
          <h3 className="text-xl font-semibold mt-1">
            <Link to={`/products/${product.id}`} className="hover:text-gray-600 transition">
              {product.name}
            </Link>
          </h3>
          <p className="text-gray-500 text-sm mt-2 line-clamp-2">{product.description}</p>
        </div>
        <div className="mt-5 pt-3 border-t border-gray-100">
          <Link
            to={`/products/${product.id}`}
            className="inline-flex items-center text-sm font-semibold text-black hover:text-gray-600 transition"
          >
            View details →
          </Link>
        </div>
      </div>
    </article>
  );
}
