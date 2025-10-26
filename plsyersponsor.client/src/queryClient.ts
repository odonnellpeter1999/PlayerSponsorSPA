import { QueryClient } from '@tanstack/react-query'

// Central QueryClient instance. Keep this file small so tests can create
// isolated clients by using a factory if needed.
export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      refetchOnWindowFocus: false,
      staleTime: 1000 * 60, // 1 minute
    },
    mutations: {
      retry: 0,
    },
  },
})
