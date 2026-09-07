import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import api from "../services/api";
import ProductCard from "../components/ProductCard";

export default function Home() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    api.get("/products").then(r => setProducts(r.data)).catch(console.error);
  }, []);

  return (
    <div>
      <section className="bg-black text-white">
        <div className="max-w-7xl mx-auto px-5 py-20 md:py-28 grid md:grid-cols-2 gap-10 items-center">
          <div>
            <p className="uppercase tracking-[.35em] text-gray-400 text-sm">Since the first spoonful</p>
            <h1 className="font-display text-6xl md:text-8xl mt-5 leading-none">The real<br/>taste of milk.</h1>
            <p className="text-gray-300 max-w-xl mt-7 text-lg">
              NEELCOCO brings creamy, joyful dairy treats to every family moment.
            </p>
            <Link to="/products" className="inline-block mt-9 bg-white text-black px-7 py-3 rounded-full font-semibold">
              Explore products
            </Link>
          </div>
          <div className="flex justify-center">
            <div className="w-[min(78vw,470px)] aspect-square rounded-full bg-white p-3 shadow-2xl">
              <img src="/neelcoco-logo.png" alt="NEELCOCO logo" className="w-full h-full rounded-full object-cover" />
            </div>
          </div>
        </div>
      </section>

      <section className="max-w-7xl mx-auto px-5 py-20">
        <div className="flex justify-between items-end mb-10">
          <div>
            <p className="text-xs tracking-[.3em] uppercase text-gray-500">Customer favourites</p>
            <h2 className="font-display text-4xl mt-2">Made for cravings.</h2>
          </div>
          <Link to="/products" className="underline">View all</Link>
        </div>
        <div className="grid sm:grid-cols-2 lg:grid-cols-4 gap-6">
          {products.filter(p => p.isFeatured).slice(0,4).map(p => <ProductCard key={p.id} product={p} />)}
        </div>
      </section>

      <section className="bg-gray-100">
        <div className="max-w-7xl mx-auto px-5 py-20 grid md:grid-cols-3 gap-8">
          {[
            ["01", "Real ingredients", "Thoughtful recipes designed around familiar, delicious ingredients."],
            ["02", "Made with care", "Consistent quality from manufacturing to your freezer."],
            ["03", "Made to share", "Products that turn ordinary breaks into memorable moments."]
          ].map(([n,t,d]) => (
            <div key={n} className="bg-white rounded-3xl p-8">
              <div className="text-sm text-gray-400">{n}</div>
              <h3 className="text-2xl font-semibold mt-5">{t}</h3>
              <p className="text-gray-500 mt-3">{d}</p>
            </div>
          ))}
        </div>
      </section>
    </div>
  );
}
