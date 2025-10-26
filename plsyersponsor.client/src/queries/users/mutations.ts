import { useMutation, useQueryClient } from '@tanstack/react-query'
import { apiFetch } from '../../api/client'
import { usersKeys } from '../keys'
import type { CreateClubRequest, CreateClubResponse } from './types'

export function useCreateUser() {
  const qc = useQueryClient()

  return useMutation({
    mutationFn:(user: Partial<CreateClubRequest>): Promise<CreateClubResponse> => apiFetch<CreateClubResponse>('/api/club/register', { method: 'POST', body: JSON.stringify(user) }),
    onSuccess: () => qc.invalidateQueries({ queryKey: usersKeys.all})
  })
}
