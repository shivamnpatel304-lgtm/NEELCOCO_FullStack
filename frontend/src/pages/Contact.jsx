import { useState } from "react";
import api from "../services/api";

export default function Contact() {
  const [form, setForm] = useState({ name:"", email:"", phone:"", message:"" });
  const [status, setStatus] = useState("");

  const submit = async e => {
    e.preventDefault();
    try {
      await api.post("/contact", form);
      setStatus("Thanks! Our team will contact you.");
      setForm({ name:"", email:"", phone:"", message:"" });
    } catch {
      setStatus("Please try again.");
    }
  };

  return (
    <main className="max-w-5xl mx-auto px-5 py-14">
      <h1 className="font-display text-6xl">Let's talk.</h1>
      <p className="text-gray-500 mt-4 mb-10">For distribution, retail, product and general enquiries.</p>
      <form onSubmit={submit} className="max-w-2xl space-y-4">
        {["name","email","phone"].map(k => <input key={k} required value={form[k]}
          onChange={e => setForm({...form,[k]:e.target.value})}
          placeholder={k[0].toUpperCase()+k.slice(1)} className="w-full border rounded-xl px-4 py-3" />)}
        <textarea required rows="6" value={form.message} onChange={e => setForm({...form,message:e.target.value})}
          placeholder="Your message" className="w-full border rounded-xl px-4 py-3" />
        <button className="bg-black text-white rounded-full px-8 py-3">Send enquiry</button>
        {status && <p className="text-gray-700">{status}</p>}
      </form>
    </main>
  );
}
