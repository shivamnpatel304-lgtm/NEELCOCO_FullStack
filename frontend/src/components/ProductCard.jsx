import { Link } from "react-router-dom";
import { useCart } from "../context";

export default function ProductCard({ product }) {
  const { add } = useCart();

  return (
    <article className="bg-white rounded-3xl overflow-hidden shadow-soft border border-gray-100 group">
      <Link to={`/products/${product.id}`}>
        <div className="aspect-[4/3] overflow-hidden bg-gray-100">
          <img src={product.imageUrl} alt={product.name}
               className="w-full h-full object-cover group-hover:scale-105 transition duration-500" />
        </div>
      </Link>
      <div className="p-5">
        <p className="text-xs uppercase tracking-widest text-gray-500">{product.category?.name}</p>
        <h3 className="text-xl font-semibold mt-1">{product.name}</h3>
        <p className="text-gray-500 text-sm mt-2 line-clamp-2">{product.description}</p>
        <div className="flex items-center justify-between mt-5">
          <div>
            <span className="font-bold text-xl">₹{product.price}</span>
            <span className="ml-2 text-sm text-gray-400 line-through">₹{product.mrp}</span>
          </div>
          <button onClick={() => add(product)}
                  className="bg-black text-white rounded-full px-4 py-2 text-sm hover:bg-gray-800">
            Add
          </button>
        </div>
      </div>
    </article>
  );
}
