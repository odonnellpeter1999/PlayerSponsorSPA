export interface ApiErrorResponse extends Error {
  title: string; // A short, human-readable summary of the problem
  status: number; // HTTP status code
  errors?: Record<string, string[]>; // Validation errors, of type https://tools.ietf.org/html/rfc9110#section-15.5.1
}

// lightweight API fetch wrapper
export async function apiFetch<T>(endpoint: string, init?: RequestInit): Promise<T> {
  const base = (import.meta.env.VITE_API_BASE_URL as string) ?? ''
  const res = await fetch(`${base}${endpoint}`, {
    credentials: 'include',
    headers: { 'Content-Type': 'application/json', ...(init?.headers ?? {}) },
    ...init,
  })

  const text = await res.text()
  const data = text ? JSON.parse(text) : null

  if (!res.ok) {
    const error: ApiErrorResponse = {
      title: data?.title || `HTTP ${res.status}`,
      status: res.status,
      errors: data?.errors,
      name: "",
      message: ""
    }
    throw error
  }

  return data as T
}
