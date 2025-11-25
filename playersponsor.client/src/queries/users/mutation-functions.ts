import { apiFetch } from '../../api/client'
import type { CreateClubRequest, CreateClubResponse, ClubSignInRequest, ClubSignInResponse } from './types'

export function createUser(user: Partial<CreateClubRequest>): Promise<CreateClubResponse> {
  
  return apiFetch<CreateClubResponse>('/api/club/register', { method: 'POST', body: JSON.stringify(user) })
}

export function signInClub(credentials: ClubSignInRequest): Promise<ClubSignInResponse> {
  return apiFetch<ClubSignInResponse>('/login?useCookies=true', {
    method: 'POST', body: JSON.stringify(credentials),
  });
}