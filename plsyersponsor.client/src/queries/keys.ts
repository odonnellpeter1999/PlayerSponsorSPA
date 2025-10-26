// Centralised, typed cache keys for react-query
export const usersKeys = {
  all: ['users'] as const,
  lists: () => [...usersKeys.all, 'list'] as const,
  list: (filters?: Record<string, any>) => [...usersKeys.lists(), filters ?? {}] as const,
  detail: (id: number) => [...usersKeys.all, 'detail', id] as const,
}
