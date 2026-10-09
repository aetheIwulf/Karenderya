
import { useEffect, useMemo, useState } from "react";
import { getMenuItems, createOrder } from "./services/api";
import {
  ShoppingBasket,
  Plus,
  Minus,
  Trash2,
  Utensils,
  RefreshCw,
} from "lucide-react";


function normalizeMenu(data) {
  const items = Array.isArray(data)
    ? data
    : data?.items ?? data?.menuItems ?? data?.$values ?? [];

  if (!Array.isArray(items)) {
    throw new Error("Menu API did not return an array.");
  }

  return items.map((item) => ({
    id: item.id ?? item.Id,
    name: item.name ?? item.Name,
    price: Number(item.price ?? item.Price ?? 0),
    stock: Number(
      item.availableQuantity ??
      item.AvailableQuantity ??
      item.availableOrderQty ??
      item.AvailableOrderQty ??
      item.stock ??
      item.Stock ??
      0
    ),
  }));
}

export default function App() {
  const [menu, setMenu] = useState([]);
  const [cart, setCart] = useState([]);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState("");
  const [notice, setNotice] = useState("");

  async function loadMenu() {
    setLoading(true);
    setError("");

    try {
      const data = await getMenuItems();
      setMenu(normalizeMenu(data));
    } catch (err) {
      setError(
        `${err.message}. Check the API URL, endpoint, and HTTPS certificate.`
      );
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    loadMenu();
  }, []);

  const total = useMemo(
    () => cart.reduce((sum, item) => sum + item.price * item.quantity, 0),
    [cart]
  );

  function addToCart(item) {
    setNotice("");
    setError("");

    if (item.stock <= 0) {
      setError(`${item.name} is out of stock.`);
      return;
    }

    setCart((current) => {
      const existing = current.find((x) => x.id === item.id);

      if (existing) {
        if (existing.quantity >= item.stock) {
          setError(`Only ${item.stock} serving(s) of ${item.name} available.`);
          return current;
        }

        return current.map((x) =>
          x.id === item.id
            ? { ...x, quantity: x.quantity + 1 }
            : x
        );
      }

      return [...current, { ...item, quantity: 1 }];
    });
  }

  function changeQuantity(id, amount) {
    setCart((current) =>
      current
        .map((item) => {
          if (item.id !== id) return item;

          const menuItem = menu.find((x) => x.id === id);
          const quantity = item.quantity + amount;

          if (quantity > (menuItem?.stock ?? 0)) return item;

          return { ...item, quantity };
        })
        .filter((item) => item.quantity > 0)
    );
  }

  
  async function handleCheckout(event) {
    event.preventDefault();
    setError("");
    setNotice("");

    if (cart.length === 0) {
      setError("Please add at least one item to your order.");
      return;
    }

    // Match this payload to your backend's CreateOrder DTO.
    const order = {
      items: cart.map((item) => ({
        menuItemId: item.id,
        quantity: item.quantity,
      })),
    };

    setSubmitting(true);

    try {
      const result = await createOrder(order);

      const orderNumber =
        result?.orderNumber ??
        result?.OrderNumber ??
        result?.order?.orderNumber ??
        result?.order?.OrderNumber;

      setNotice(
        orderNumber != null
          ? `Order #${orderNumber} created successfully!`
          : "Order submitted successfully!"
      );

      setCart([]);
      await loadMenu();
    } catch (err) {
      setError(`Could not submit order: ${err.message}`);
    } finally {
      setSubmitting(false);
    }
  }

  return (
    <div className="app-shell">
      <header className="topbar">
        <a className="brand" href="/">
          <span className="brand-icon">
            <Utensils size={23} />
          </span>
          <span>
            <strong>Karenderya</strong>
            <small>Fresh lutong-bahay</small>
          </span>
        </a>

        <div className="header-cart">
          <ShoppingBasket size={20} />
          <span>{cart.reduce((sum, x) => sum + x.quantity, 0)} items</span>
        </div>
      </header>

      <main className="layout">
        <section className="menu-section">
          <div className="hero">
            <span className="eyebrow">SARAP NG LUTONG-BAHAY</span>
            <h1>What's on the menu?</h1>
            <p>Choose your favorites and build your order.</p>
          </div>

          <div className="section-heading">
            <div>
              <h2>Today's menu</h2>
              <p>Select food to add it to your order.</p>
            </div>
            <button
              className="refresh-button"
              onClick={loadMenu}
              disabled={loading}
              title="Refresh menu"
            >
              <RefreshCw size={17} />
              Refresh
            </button>
          </div>

          {loading && <p className="state-message">Loading menu...</p>}

          {!loading && error && menu.length === 0 && (
            <div className="error-panel">
              <p>{error}</p>
              <button className="secondary-button" onClick={loadMenu}>
                Try again
              </button>
            </div>
          )}

          {!loading && !error && menu.length === 0 && (
            <p className="state-message">
              No menu items found. Check your API response.
            </p>
          )}

          <div className="menu-grid">
            {menu.map((item) => (
              <article className="menu-card" key={item.id}>
                <div className="food-icon">
                  <Utensils size={26} />
                </div>

                <div className="food-info">
                  <h3>{item.name || "Unnamed item"}</h3>
                  <strong className="price">
                    ₱{item.price.toFixed(2)}
                  </strong>
                  <p className={item.stock > 0 ? "in-stock" : "out-stock"}>
                    {item.stock > 0
                      ? `${item.stock.toLocaleString()} available`
                      : "Sold out"}
                  </p>
                </div>

                <button
                  className="add-button"
                  onClick={() => addToCart(item)}
                  disabled={
                    item.stock <= 0 ||
                    cart.some(
                      (x) => x.id === item.id && x.quantity >= item.stock
                    )
                  }
                  aria-label={`Add ${item.name}`}
                >
                  <Plus size={19} />
                </button>
              </article>
            ))}
          </div>
        </section>

        <aside className="order-panel">
          <div className="order-title">
            <div className="order-title-icon">
              <ShoppingBasket size={21} />
            </div>
            <div>
              <h2>Your order</h2>
              <p>Review before checkout</p>
            </div>
          </div>

          {cart.length === 0 ? (
            <div className="empty-cart">
              <ShoppingBasket size={36} />
              <strong>Your cart is empty</strong>
              <p>Add some delicious food from the menu.</p>
            </div>
          ) : (
            <div className="cart-items">
              {cart.map((item) => (
                <div className="cart-item" key={item.id}>
                  <div className="cart-item-info">
                    <strong>{item.name}</strong>
                    <span>
                      ₱{(item.price * item.quantity).toFixed(2)}
                    </span>
                  </div>

                  <div className="quantity-controls">
                    <button
                      onClick={() => changeQuantity(item.id, -1)}
                      aria-label={`Decrease ${item.name}`}
                    >
                      {item.quantity === 1 ? (
                        <Trash2 size={14} />
                      ) : (
                        <Minus size={14} />
                      )}
                    </button>
                    <span>{item.quantity}</span>
                    <button
                      onClick={() => changeQuantity(item.id, 1)}
                      disabled={
                        item.quantity >=
                        (menu.find((x) => x.id === item.id)?.stock ?? 0)
                      }
                      aria-label={`Increase ${item.name}`}
                    >
                      <Plus size={14} />
                    </button>
                  </div>
                </div>
              ))}
            </div>
          )}

          <div className="total-row">
            <span>Total</span>
            <strong>₱{total.toFixed(2)}</strong>
          </div>

          {error && menu.length > 0 && (
            <div className="feedback error-feedback" role="alert">
              {error}
            </div>
          )}

          {notice && (
            <div className="feedback success-feedback" role="status">
              {notice}
            </div>
          )}

          <form className="checkout-form" onSubmit={handleCheckout}>
            <button
              className="checkout-button"
              type="submit"
              disabled={submitting || cart.length === 0}
            >
              {submitting
                ? "Processing order..."
                : `Place order · ₱${total.toFixed(2)}`}
            </button>
          </form>

          <p className="checkout-note">
            Final prices and stock availability must be validated by the
            backend.
          </p>
        </aside>
      </main>
    </div>
  );
}