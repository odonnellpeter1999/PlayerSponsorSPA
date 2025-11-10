export interface CreateClubRequest {
    adminAccountDetails: AccountDetails
    clubDetails: ClubDetails
}

export interface CreateClubResponse {
    clubId: string
}

export interface AccountDetails {
    email: string
    password: string
    confirmPassword: string
    phoneNumber?: string
    username?: string
}

export interface ClubDetails {
    name:string
    interacEmail: string
    description?: string
}