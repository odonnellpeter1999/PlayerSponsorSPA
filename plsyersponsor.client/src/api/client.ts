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
    const err = new Error((data && (data as any).message) || `HTTP ${res.status}`)
    ;(err as any).status = res.status
    ;(err as any).data = data
    throw err
  }

  return data as T
}
