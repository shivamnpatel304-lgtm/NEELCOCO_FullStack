import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api";
import { useCart } from "../context";

export default function Checkout() {
  const { cart, total, clear } = useCart();
  const navigate = useNavigate();
  const [form, setForm] = useState({ customerName:"", email:"", phone:"", address:"", city:"" });
  const [message, setMessage] = useState("");

  const submit = async e => {
    e.preventDefault();
    setMessage("Submitting order...");
    try {
      const r = await api.post("/orders", {
        ...form,
        items: cart.map(x => ({ productId: x.id, quantity: x.quantity }))
      });
      clear();
      navigate(`/order-success/${r.data.id}`);
    } catch (err) {
      setMessage(err.response?.data?.message || "Could not place order.");
    }
  };

  if (!cart.length) return <div className="p-20 text-center">Your cart is empty.</div>;

  return (
    <main className="max-w-5xl mx-auto px-5 py-14">
      <div className="grid md:grid-cols-2 gap-10">
        <form onSubmit={submit} className="space-y-4">
          <h1 className="font-display text-5xl mb-8">Checkout</h1>
          {Object.entries(form).map(([key, value]) => (
            <input key={key} required value={value}
              onChange={e => setForm({...form, [key]: e.target.value})}
              placeholder={key.replace(/([A-Z])/g, " $1")}
              className="w-full border rounded-xl px-4 py-3" />
          ))}
          <button className="w-full bg-black text-white rounded-full py-4 font-semibold">Place order · ₹{total}</button>
          {message && <p className="text-red-600">{message}</p>}
        </form>
        <div className="bg-gray-100 rounded-3xl p-7 h-fit">
          <h2 className="font-semibold text-xl">Order summary</h2>
          {cart.map(x => <div className="flex justify-between py-3 border-b" key={x.id}><span>{x.name} × {x.quantity}</span><span>₹{x.price*x.quantity}</span></div>)}
          <div className="flex justify-between font-bold text-xl pt-5"><span>Total</span><span>₹{total}</span></div>
        </div>
      </div>
    </main>
  );
}
