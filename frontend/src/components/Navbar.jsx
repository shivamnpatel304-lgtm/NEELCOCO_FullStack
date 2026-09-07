import { Link, NavLink } from "react-router-dom";
import { useCart } from "../context";

export default function Navbar() {
  const { count } = useCart();
  const active = ({ isActive }) => isActive ? "text-white" : "text-gray-300 hover:text-white";

  return (
    <header className="sticky top-0 z-50 bg-black/95 text-white backdrop-blur border-b border-white/10">
      <div className="max-w-7xl mx-auto px-5 h-20 flex items-center justify-between">
        <Link to="/" className="flex items-center gap-3">
          <img src="/neelcoco-logo.png" className="w-12 h-12 rounded-full object-cover" alt="NEELCOCO" />
          <div className="hidden sm:block">
            <div className="font-display text-xl tracking-wide">NEELCOCO</div>
            <div className="text-[10px] tracking-[.22em] text-gray-400">THE REAL TASTE OF MILK</div>
          </div>
        </Link>
        <nav className="flex items-center gap-5 text-sm">
          <NavLink className={active} to="/">Home</NavLink>
          <NavLink className={active} to="/products">Products</NavLink>
          <NavLink className={active} to="/about">About</NavLink>
          <NavLink className={active} to="/contact">Contact</NavLink>
          <NavLink className={active} to="/agents">AI Agents</NavLink>
          <Link to="/cart" className="relative bg-white text-black rounded-full px-4 py-2 font-semibold">
            Cart
            {count > 0 && <span className="ml-2 bg-black text-white rounded-full px-2 text-xs">{count}</span>}
          </Link>
        </nav>
      </div>
    </header>
  );
}
