import { Link, useNavigate } from "react-router-dom";
import { useCart } from "../context";

export default function Cart() {
  const { cart, remove, change, total } = useCart();
  const navigate = useNavigate();

  if (!cart.length) return (
    <main className="max-w-4xl mx-auto px-5 py-24 text-center">
      <h1 className="font-display text-5xl">Your cart is empty.</h1>
      <Link to="/products" className="inline-block mt-8 bg-black text-white rounded-full px-7 py-3">Shop now</Link>
    </main>
  );

  return (
    <main className="max-w-5xl mx-auto px-5 py-14">
      <h1 className="font-display text-5xl mb-10">Your cart</h1>
      <div className="space-y-4">
        {cart.map(item => (
          <div key={item.id} className="flex gap-4 items-center bg-white border rounded-2xl p-4">
            <img src={item.imageUrl} className="w-24 h-24 rounded-xl object-cover" />
            <div className="flex-1">
              <h3 className="font-semibold">{item.name}</h3>
              <p className="text-gray-500">₹{item.price}</p>
            </div>
            <input type="number" min="1" value={item.quantity}
              onChange={e => change(item.id, Number(e.target.value))}
              className="w-16 border rounded-lg p-2" />
            <div className="font-semibold w-20 text-right">₹{item.price * item.quantity}</div>
            <button onClick={() => remove(item.id)} className="text-red-600">×</button>
          </div>
        ))}
      </div>
      <div className="flex justify-end mt-10">
        <div className="w-full sm:w-80 border rounded-3xl p-6">
          <div className="flex justify-between text-lg"><span>Total</span><strong>₹{total}</strong></div>
          <button onClick={() => navigate("/checkout")} className="w-full mt-5 bg-black text-white rounded-full py-3">Checkout</button>
        </div>
      </div>
    </main>
  );
}
