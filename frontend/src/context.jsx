import { createContext, useContext, useEffect, useMemo, useState } from "react";

const CartContext = createContext(null);

export function CartProvider({ children }) {
  const [cart, setCart] = useState(() => JSON.parse(localStorage.getItem("neelcoco_cart") || "[]"));

  useEffect(() => {
    localStorage.setItem("neelcoco_cart", JSON.stringify(cart));
  }, [cart]);

  const add = (product) => setCart(current => {
    const found = current.find(x => x.id === product.id);
    if (found) return current.map(x => x.id === product.id ? { ...x, quantity: x.quantity + 1 } : x);
    return [...current, { ...product, quantity: 1 }];
  });

  const remove = (id) => setCart(current => current.filter(x => x.id !== id));
  const change = (id, quantity) => setCart(current =>
    current.map(x => x.id === id ? { ...x, quantity: Math.max(1, quantity) } : x)
  );
  const clear = () => setCart([]);

  const value = useMemo(() => ({
    cart, add, remove, change, clear,
    count: cart.reduce((s, x) => s + x.quantity, 0),
    total: cart.reduce((s, x) => s + x.price * x.quantity, 0)
  }), [cart]);

  return <CartContext.Provider value={value}>{children}</CartContext.Provider>;
}

export const useCart = () => useContext(CartContext);
