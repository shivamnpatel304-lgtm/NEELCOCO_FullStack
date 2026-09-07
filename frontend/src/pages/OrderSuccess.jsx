import { Link, useParams } from "react-router-dom";

export default function OrderSuccess() {
  const { id } = useParams();
  return (
    <main className="max-w-3xl mx-auto px-5 py-24 text-center">
      <div className="w-20 h-20 rounded-full bg-black text-white mx-auto grid place-items-center text-3xl">✓</div>
      <h1 className="font-display text-5xl mt-7">Thank you!</h1>
      <p className="text-gray-600 mt-4">Your NEELCOCO order #{id} has been received.</p>
      <Link to="/products" className="inline-block mt-8 bg-black text-white rounded-full px-7 py-3">Continue shopping</Link>
    </main>
  );
}
