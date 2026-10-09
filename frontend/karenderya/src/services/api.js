
const API_BASE_URL = (
  import.meta.env.VITE_API_BASE_URL || "https://localhost:7069"
).replace(/\/+$/, "");

async function request(endpoint, options = {}) {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    ...options,
    headers: {
      ...(options.body
        ? { "Content-Type": "application/json" }
        : {}),
      ...options.headers,
    },
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(
      message || `Request failed with status ${response.status}`
    );
  }

  if (response.status === 204) return null;

  const text = await response.text();
  return text ? JSON.parse(text) : null;
}

export function getMenuItems() {
  return request("/api/MenuItem");
}

export function createOrder(order) {
  return request("/api/Order", {
    method: "POST",
    body: JSON.stringify(order),
  });
}