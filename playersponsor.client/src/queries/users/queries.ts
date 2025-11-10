// import { useQuery } from '@tanstack/react-query'
// import { apiFetch } from '../../api/client'
// import { usersKeys } from '../keys'
// import type { User } from './types'

// export function useUsers(filters?: { page?: number }) {
//   const qs = filters ? `?${new URLSearchParams(filters as any)}` : ''
//   return useQuery(
//     usersKeys.list(filters),
//     () => apiFetch<User[]>(`/users${qs}`),
//     { keepPreviousData: true },
//   )
// }

// export function useUser(id?: number) {
//   return useQuery(
//     usersKeys.detail(id ?? 0),
//     () => apiFetch<User>(`/users/${id}`),
//     { enabled: !!id },
//   )
// }
