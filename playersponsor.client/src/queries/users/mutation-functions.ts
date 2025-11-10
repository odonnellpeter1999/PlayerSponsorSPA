import { apiFetch } from '../../api/client'
import type { CreateClubRequest, CreateClubResponse } from './types'

export function createUser(user: Partial<CreateClubRequest>): Promise<CreateClubResponse> {
  
  return apiFetch<CreateClubResponse>('/api/club/register', { method: 'POST', body: JSON.stringify(user) })
}