import { Routes, Route } from "react-router-dom";
import { CartProvider } from "./context";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";
import Products from "./pages/Products";
import ProductDetails from "./pages/ProductDetails";
import Cart from "./pages/Cart";
import Checkout from "./pages/Checkout";
import OrderSuccess from "./pages/OrderSuccess";
import About from "./pages/About";
import Contact from "./pages/Contact";
import Login from "./pages/Login";
import Admin from "./pages/Admin";
import Agents from "./pages/Agents";

export default function App() {
  return (
    <CartProvider>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/products" element={<Products />} />
        <Route path="/products/:id" element={<ProductDetails />} />
        <Route path="/cart" element={<Cart />} />
        <Route path="/checkout" element={<Checkout />} />
        <Route path="/order-success/:id" element={<OrderSuccess />} />
        <Route path="/about" element={<About />} />
        <Route path="/contact" element={<Contact />} />
        <Route path="/login" element={<Login />} />
        <Route path="/admin" element={<Admin />} />
        <Route path="/agents" element={<Agents />} />
      </Routes>
      <footer className="bg-black text-white mt-20">
        <div className="max-w-7xl mx-auto px-5 py-12 flex flex-col md:flex-row justify-between gap-8">
          <div><div className="font-display text-2xl">NEELCOCO</div><p className="text-gray-400 mt-2">The real taste of milk.</p></div>
          <div className="text-gray-400 text-sm">© {new Date().getFullYear()} NEELCOCO. All rights reserved.</div>
        </div>
      </footer>
    </CartProvider>
  );
}
