import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api";

export default function Login() {
  const [email,setEmail] = useState("admin@neelcoco.com");
  const [password,setPassword] = useState("Admin@123");
  const [error,setError] = useState("");
  const navigate = useNavigate();

  const submit = async e => {
    e.preventDefault();
    try {
      const r = await api.post("/auth/login", { email, password });
      localStorage.setItem("neelcoco_token", r.data.token);
      navigate("/admin");
    } catch {
      setError("Invalid credentials");
    }
  };

  return (
    <main className="min-h-[70vh] grid place-items-center px-5">
      <form onSubmit={submit} className="w-full max-w-md border rounded-3xl p-8 shadow-soft">
        <img src="/neelcoco-logo.png" className="w-20 h-20 rounded-full mx-auto object-cover" />
        <h1 className="font-display text-4xl text-center mt-5">Admin login</h1>
        <input className="w-full border rounded-xl p-3 mt-8" value={email} onChange={e=>setEmail(e.target.value)} />
        <input className="w-full border rounded-xl p-3 mt-3" type="password" value={password} onChange={e=>setPassword(e.target.value)} />
        <button className="w-full mt-5 bg-black text-white rounded-full py-3">Login</button>
        {error && <p className="text-red-600 text-center mt-3">{error}</p>}
      </form>
    </main>
  );
}
